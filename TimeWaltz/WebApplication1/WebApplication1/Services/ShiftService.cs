using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Models.Entity;

namespace WebApplication1.Services
{
    public class ShiftService
    {
        private readonly TimeWaltzContext _db;
        public ShiftService(TimeWaltzContext timeWaltzContext)
        {
            _db = timeWaltzContext;
        }

        public bool GenerateAllEmployeeShift(DateTime startDate, int GenDays)
        {
            try
            {
                var endDate = startDate.AddDays(GenDays);
                var employees = _db.Employees.Where(x => x.ShiftScheduleId != null).ToList();
                for (var date = startDate; date < endDate; date = date.AddDays(1))
                {
                    if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                    {
                        continue;
                    }
                    foreach (var employee in employees)
                    {
                        var todayShifts = _db.Shifts.FirstOrDefault(x => x.EmployeesId == employee.Id && x.ShiftScheduleId == employee.ShiftScheduleId && x.ShiftsDate == date);
                        if (todayShifts == null)
                        {
                            _db.Shifts.Add(new Shift
                            {
                                ShiftScheduleId = employee.ShiftScheduleId.Value,
                                ShiftsDate = date,
                                EmployeesId = employee.Id
                            });
                        }
                    }
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Shift> GetAllShiftData()
        {
            return _db.Shifts
                .Join(_db.Employees, s => s.ShiftScheduleId, e => e.ShiftScheduleId, (s, e) => new { s, e })
                .Join(_db.ShiftSchedules, se => se.s.ShiftScheduleId, d => d.Id, (se, d) => new Shift
                {
                    StartTime = se.s.ShiftsDate.ToString("yyyy-MM-dd") + " " + d.StartTime.ToString("HH:mm"),
                    EndTime = se.s.ShiftsDate.ToString("yyyy-MM-dd") + " " + d.EndTime.ToString("HH:mm"),
                    Id = se.s.Id,
                    Title = se.e.Name + "(" + d.ShiftsName + ")",
                }).ToList();
        }
    }
}
