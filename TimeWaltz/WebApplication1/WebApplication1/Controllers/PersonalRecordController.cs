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

        public IActionResult LeaveRecord()
        {
            var status = _timeWaltzContext.RequestStatuses.Where(x => x.TableType == TableTypeEnum.Leave);
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
            public IActionResult Clock()
            {
                return View();
            }
            public IActionResult Attendance()
            {
                return View();
            }
            public IActionResult Leave()
            {
                return View();
            }
            public IActionResult Overtime()
            {
                return View();
            }
            public IActionResult ShiftSchedule()
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
}
