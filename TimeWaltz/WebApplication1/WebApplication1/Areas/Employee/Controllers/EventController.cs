using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Enums;
using Repository.Models;
using WebApplication1.Filters;
using WebApplication1.Models.PersonalRecordViewModels;

namespace WebApplication1.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(AuthenticationSchemes = "EmployeeAuthScheme")]
    [TimeWaltzMobileAuthorize]
    public class EventController : Controller
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public EventController(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }
        /// <summary>
        /// 打卡
        /// </summary>
        /// <returns></returns>
        public IActionResult Clock()
        {
            var a = User;
            var toDay = DateTime.Now.Date;
            var clockRecord = _timeWaltzContext.Clocks.FirstOrDefault(res => res.Date.Date == toDay);
            ClockViewModel clockViewModel = null;

            if (clockRecord != null)
            {
                clockViewModel = new ClockViewModel
                {
                    EmployeesId = clockRecord.EmployeesId,
                    StartClockInDate = clockRecord.Status == ClockStatusEnum.上班打卡 ? clockRecord.Date : null,
                    StartClockInLongitude = clockRecord.Status == ClockStatusEnum.上班打卡 ? clockRecord.Longitude : null,
                    StartClockInLatitude = clockRecord.Status == ClockStatusEnum.上班打卡 ? clockRecord.Latitude : null,
                    EndClockInDate = clockRecord.Status == ClockStatusEnum.下班打卡 ? clockRecord.Date : null,
                    EndClockInLongitude = clockRecord.Status == ClockStatusEnum.下班打卡 ? clockRecord.Longitude : null,
                    EndClockInLatitude = clockRecord.Status == ClockStatusEnum.下班打卡 ? clockRecord.Latitude : null,
                };

                var shift = _timeWaltzContext.Shifts.FirstOrDefault(workShift => workShift.ShiftsDate.Date == toDay);
                var shiftSchedule = _timeWaltzContext.ShiftSchedules.FirstOrDefault(ShiftSchedule => ShiftSchedule.StartTime.Date == toDay);

                if (shift != null && shiftSchedule != null)
                {
                    TimeSpan startTimeOfDay = shift.ShiftSchedule.StartTime.TimeOfDay;
                    TimeSpan endTimeOfDay = shift.ShiftSchedule.EndTime.TimeOfDay;

                    clockViewModel.ShiftScheduleStartTime = startTimeOfDay;
                    clockViewModel.ShiftScheduleEndTime = endTimeOfDay;
                }
            }

            return View(clockViewModel ?? new ClockViewModel());
        }

        /// <summary>
        /// 請假申請
        /// </summary>
        /// <returns></returns>
        public IActionResult LeaveRequest()
        {
            return View();
        }


        /// <summary>
        /// 加班申請
        /// </summary>
        /// <returns></returns>
        public IActionResult OverTimeRequest()
        {
            return View();
        }

        /// <summary>
        /// 補打卡申請
        /// </summary>
        /// <returns></returns>
        public IActionResult CompRequest()
        {
            return View();
        }
    }
}
