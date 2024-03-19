﻿
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
        public object GetAttendanceData(int userId)
        {
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            var shift = _timeWaltzContext.Shifts.Where(x => x.EmployeesId == userId 
            && x.ShiftsDate.Date.Year == currentYear 
            && x.ShiftsDate.Month == currentMonth).ToList();

            var allClock = _timeWaltzContext.Clocks.Where(x => x.EmployeesId == userId &&
            EF.Functions.DateDiffDay(x.Date, DateTime.Now) == 0).AsEnumerable();

            var leaveRequest = _timeWaltzContext.LeaveRequests.Where(x=>x.EmployeesId == userId 
            && x.StartTime.Date.Year == currentYear
            && x.StartTime.Month == currentMonth).ToList();

            for(var i = 1; i < shift.Count; i++)
            {
                var fullDate = new DateTime(currentYear, currentMonth , i);
                var currentshift = shift.FirstOrDefault(x => x.ShiftsDate == fullDate);
                if(currentshift != null)
                {
                    var currentClockIn = allClock.Where(x => x.Status == ClockStatusEnum.上班打卡).MinBy(x => x.Date).Date;
                    var currentClockOut = allClock.Where(x => x.Status == ClockStatusEnum.下班打卡).MaxBy(x => x.Date).Date;
                    if(currentClockIn == null && currentClockOut == null)
                    {
                        
                    } 
                }

            }

            var attendanceList = new List<AttendanceDto>();
            attendanceList.Add(new AttendanceDto
            {
                ClockInTime = allClock.Where(x => x.Status == ClockStatusEnum.上班打卡).MinBy(x => x.Date).Date,
                ClockOutTime = allClock.Where(x => x.Status == ClockStatusEnum.下班打卡).MaxBy(x => x.Date).Date,
                ShiftDate = 
            });

            
            
                
            



            attendanceDto.Add(new AttendanceDto
            {

            });

        }
    }
}
