using WebApplication1.Models.Entity;

namespace WebApplication1.Services
{
    public class SpecialHolidayDaysService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public SpecialHolidayDaysService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }
        public void CountSpecialHolidayDaysAnniversary()
        {
            var emp = _timeWaltzContext.Employees.ToList();
            foreach (var e in emp)
            {
                var monthsPassed = GetDayPassed(e.HireDate);
                var serviceDays = GetServiceDays(monthsPassed);
                var thisYear = new DateTime(DateTime.Now.Year + e.HireDate.Month + e.HireDate.Day);

                var temp1 = _timeWaltzContext.SpecialHolidayDays.FirstOrDefault(x => x.EmployeeId == e.Id);
                //TODO: 撈出該員工所有請過的特休假，用來扣除
                var alreadyLeave = (int)_timeWaltzContext.LeaveRequests.Where(x=>x.EmployeesId == e.Id && x.StartTime > thisYear).Select(x=>x.LeaveMinutes).Sum();
                int renewServiceDays = serviceDays.Days - alreadyLeave;
                if (temp1 == null)
                {
                    _timeWaltzContext.SpecialHolidayDays.Add(new SpecialHolidayDays
                    {
                        EmployeeId = e.Id,
                        AvailableDays = renewServiceDays,
                    });
                }
                else
                {
                    temp1.AvailableDays = renewServiceDays;
                }
                _timeWaltzContext.SaveChanges();
            }
        }

        public SpecialGrade GetServiceDays(int monthsPassed)
        {
            var specialGrades = _timeWaltzContext.SpecialGrade.FirstOrDefault(x => monthsPassed <= x.ServiceLength);
            if (specialGrades == null) throw new Exception("資料庫錯誤");
            return specialGrades;
        }

        public int GetDayPassed(DateTime date)
        {
            DateTime today = DateTime.Today;
            int monthsPassed = ((today.Year - date.Year) * 12) + today.Month - date.Month;
            return monthsPassed;
        }
    }
}
