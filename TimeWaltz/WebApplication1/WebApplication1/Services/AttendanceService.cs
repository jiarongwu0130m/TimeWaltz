using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using Repository.Models;
using WebApplication1.Models.PersonalRecordViewModels;


namespace WebApplication1.Services
{
    public class AttendanceService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public AttendanceService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }
        public AttendanceViewModel GetAttendanceData(int userId)
        {
            var _AttendanceViewModel = new AttendanceViewModel
            {
                
                
            };
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            var shift = _timeWaltzContext.Shifts.Where(x => x.EmployeesId == userId
            && x.ShiftsDate.Date.Year == currentYear
            && x.ShiftsDate.Month == currentMonth).Join(_timeWaltzContext.ShiftSchedules, x => x.ShiftScheduleId, y => y.Id, (x, y) => new { x, y }).ToList();

            var allClock = _timeWaltzContext.Clocks.Where(x => x.EmployeesId == userId &&
            EF.Functions.DateDiffDay(x.Date, DateTime.Now) == 0).AsEnumerable();

            for (var i = 1; i < shift.Count; i++)
            {
                var fullDate = new DateTime(currentYear, currentMonth, i);
                var currentshift = shift.FirstOrDefault(xy => xy.x.ShiftsDate == fullDate);
                if (currentshift != null)
                {
                    var currentClockIn = allClock.Where(x => x.Status == ClockStatusEnum.上班打卡).MinBy(x => x.Date).Date;
                    var currentClockOut = allClock.Where(x => x.Status == ClockStatusEnum.下班打卡).MaxBy(x => x.Date).Date;
                    //無打卡-請假or曠職
                    if (currentClockIn == null && currentClockOut == null)
                    {

                    }
                    //打卡異常
                    else if (currentClockIn == null || currentClockOut == null)
                    {

                    }   
                }
            }
            return _AttendanceViewModel;
        }

        // 將請假記錄添加到對應月份的函式
        static void AddLeaveRecord(Dictionary<string, List<Tuple<DateTime, DateTime>>> leaveRecords, string employeeId, DateTime startDate, DateTime endDate)
        {
            while (startDate <= endDate)
            {
                int year = startDate.Year;
                int month = startDate.Month;
                DateTime lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                DateTime currentMonthEndDate = DateTime.Compare(endDate, lastDayOfMonth) < 0 ? endDate : lastDayOfMonth;

                if (!leaveRecords.ContainsKey(employeeId))
                {
                    leaveRecords[employeeId] = new List<Tuple<DateTime, DateTime>>();
                }

                leaveRecords[employeeId].Add(Tuple.Create(startDate, currentMonthEndDate));

                startDate = currentMonthEndDate.AddDays(1);
            }
        }

    }
}
