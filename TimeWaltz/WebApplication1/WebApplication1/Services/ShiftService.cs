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
                for (var date = startDate; date < endDate; date.AddDays(1))
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



    }
}
