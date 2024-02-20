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
        public IActionResult ShiftSchedule(ShiftSchedulesViewModel model)
        {
            //TODO: 將model接到的sta和end拿來當Where條件做出查詢功能
            
            return View();
        }


        public IActionResult Clock()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Clock(ClockViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "請檢查表單中的錯誤。";
                return View(model);
            }
            var clockData = new Clock
            {
                EmployeesId = model.EmployeesId,
                Date = model.Date,
                Status = ClockStatusEnum.上班卡,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
            };

            //clockData.Status = ClockStatusEnum.上班卡 ;
            //clockData.Date = model.Date;
            //clockData.Latitude = model.Latitude;
            //clockData.Longitude = model.Longitude;

            _clockService.ClockData(clockData);
            ViewBag.SuccessMessage = "打卡成功！";
            return View();

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
