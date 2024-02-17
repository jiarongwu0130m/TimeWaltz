using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Entity;
using WebApplication1.Models.Enums;

namespace WebApplication1.Controllers
{
    public class PersonalRecordController : Controller
    {
        private readonly TimeWaltzContext _timeWaltzContext;
        public PersonalRecordController(TimeWaltzContext timeWaltzContext)
        {

            _timeWaltzContext = timeWaltzContext;

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
        public IActionResult Clock()
        {
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
