
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Models.Entity;
using WebApplication1.Models.Enums;
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
        //public object GetAttendanceData(int userId)
        //{
        //    int currentYear = DateTime.Now.Year;
        //    int currentMonth = DateTime.Now.Month;

        //    var shift = _timeWaltzContext.Shifts.Where(x => x.EmployeesId == userId 
        //    && x.ShiftsDate.Date.Year == currentYear 
        //    && x.ShiftsDate.Month == currentMonth).Join(_timeWaltzContext.ShiftSchedules,x=>x.ShiftScheduleId, y=>y.Id, (x, y)=>new {x, y}).ToList();

        //    var allClock = _timeWaltzContext.Clocks.Where(x => x.EmployeesId == userId &&
        //    EF.Functions.DateDiffDay(x.Date, DateTime.Now) == 0).AsEnumerable();

        //    for(var i = 1; i < shift.Count; i++)
        //    {
        //        var fullDate = new DateTime(currentYear, currentMonth , i);
        //        var currentshift = shift.FirstOrDefault(xy => xy.x.ShiftsDate == fullDate);
        //        if(currentshift != null)
        //        {
        //            var currentClockIn = allClock.Where(x => x.Status == ClockStatusEnum.上班打卡).MinBy(x => x.Date).Date;
        //            var currentClockOut = allClock.Where(x => x.Status == ClockStatusEnum.下班打卡).MaxBy(x => x.Date).Date;
        //            if(currentClockIn == null && currentClockOut == null)
        //            {
                        
        //            } 
        //        }

        //    }
        //}
    }
}
