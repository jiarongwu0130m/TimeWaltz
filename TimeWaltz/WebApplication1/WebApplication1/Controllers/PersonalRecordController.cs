using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;
using WebApplication1.Models;
using WebApplication1.Models.Entity;
using WebApplication1.Models.Enums;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class PersonalRecordController : Controller
    {
        private readonly TimeWaltzContext _timeWaltzContext;
        private readonly ClockService _clockService;

        public PersonalRecordController(TimeWaltzContext timeWaltzContext, ClockService clockService)
        {

            _timeWaltzContext = timeWaltzContext;
            _clockService = clockService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Leave()
        {
            var status = _timeWaltzContext.RequestStatuses.Where(x => x.TableType == TableTypeEnum.請假單);
            var data = _timeWaltzContext.LeaveRequests.Join(status,
                l => l.Id,
                s => s.TableId,
                (l, s) => new LeaveViewModel
                {
                    StartTime = l.StartTime,
                    EndTime = l.EndTime,
                    LeaveHour = l.LeaveHours,
                    VacationName = l.VacationDetails.VacationType.ToString(),
                    ApprovalStatus = s.Status.ToString()
                }).ToString();
            return Json(data);

        }
        public IActionResult ShiftSchedule()
        {
            //var data = _timeWaltzContext.ShiftSchedules.Select(s => new ShiftSchedulesViewModel
            //{
            //    Date = s.StartTime.Date,
            //    DayOfWeek = s.StartTime.DayOfWeek,
            //    StartTime = s.StartTime.TimeOfDay,
            //    EndTime = s.EndTime.TimeOfDay,
            //    BreakTime = s.BreakTime / 60.00
            //});
            return View();
        }
        [HttpGet]
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
                ViewBag.ErrorMessage = "資料錯誤。";
                return View(model);
            }

            var clockData = new Clock
            {
                EmployeesId = model.EmployeesId,
                Date = model.Date,
                Status = model.Status,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
            };

            if (model.Status == ClockStatusEnum.上班打卡)
            {
                _clockService.ClockData(clockData);
                ViewBag.SuccessMessage = "上班打卡成功！";
                return View();
            }
            else if (model.Status == ClockStatusEnum.下班打卡)
            {
                _clockService.ClockData(clockData);
                ViewBag.SuccessMessage = "下班打卡成功！";
            }
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
