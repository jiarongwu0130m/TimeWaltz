using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.Entity;
using WebApplication1.Models.Enums;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class PersonalRecordController : Controller
    {
        private readonly ShiftScheduleService _shiftScheduleService;
                private readonly ClockService _clockService;


        public PersonalRecordController(ShiftScheduleService shiftScheduleService, ClockService clockService)
        {
            _shiftScheduleService = shiftScheduleService;
            _clockService = clockService;
        }

       
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Leave()
        //{
        //    var status = _timeWaltzContext.RequestStatuses.Where(x => x.TableType == TableTypeEnum.請假單);
        //    var data = _timeWaltzContext.LeaveRequests.Join(status,
        //        l => l.Id,
        //        s => s.TableId,
        //        (l, s) => new LeaveViewModel
        //        {
        //            StartTime = l.StartTime,
        //            EndTime = l.EndTime,
        //            LeaveHour = l.LeaveHours,
        //            VacationName = l.VacationDetails.VacationType.ToString(),
        //            ApprovalStatus = s.Status.ToString()
        //        }).ToString();
        //    return Json(data);

        //}
        [HttpGet]
        public IActionResult ShiftSchedule()
        {
            
            var Id = 1;
            var entities = _shiftScheduleService.GetPersonalShiftScheduleList(Id);
            var models = EntityHelper.ToViewModel(entities);

            return View(models);
        }

        [HttpPost]
        public IActionResult ShiftSchedule(ShiftSchedulesViewModel selectedModel)
        {
            var entities = _shiftScheduleService.GetSelectedShiftScheduleList(selectedModel);
            var models = EntityHelper.ToViewModel(entities);
            return View(models);
        }


        public IActionResult Clock()
        {
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Clock(ClockViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "資料錯誤。";
                return View(model);
            }

            var clockData = new Clock
            {
                EmployeesId = model.EmployeesId,
                //Date = model.Date,
                Status = model.Status,
                //Longitude = model.Longitude,
                //Latitude = model.Latitude,
            };

            if (model.Status == ClockStatusEnum.上班打卡)
            {
                #region
                clockData.Date = (DateTime)model.StartClockInDate;
                clockData.Longitude = (decimal)model.StartClockInLongitude;
                clockData.Latitude = (decimal)model.StartClockInLatitude;
                #endregion

                _clockService.ClockData(clockData);
                ViewBag.SuccessMessage = "上班打卡成功！";
                //return View();
            }
            else if (model.Status == ClockStatusEnum.下班打卡)
            {
                #region
                clockData.Date = (DateTime)model.EndClockInDate;
                clockData.Longitude = (decimal)model.EndClockInLongitude;
                clockData.Latitude = (decimal)model.EndClockInLatitude;
                #endregion

                _clockService.ClockData(clockData);
                ViewBag.SuccessMessage = "下班打卡成功！";
            }
            return RedirectToAction("Clock");
        }

        public IActionResult Attendance()
        {
            return View();
        }

        public IActionResult Overtime()
        {
            return View();
        }

        public IActionResult Approval()
        {
            return View();
        }
        public IActionResult AgentEmployeeSetting()
        {
            return View();
        }
    }
}
