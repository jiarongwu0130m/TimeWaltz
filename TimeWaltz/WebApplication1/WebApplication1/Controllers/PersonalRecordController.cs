using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class PersonalRecordController : Controller
    {
        private readonly ShiftScheduleService _shiftScheduleService;
       

        public PersonalRecordController(ShiftScheduleService shiftScheduleService)
        {
            _shiftScheduleService = shiftScheduleService;            
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
        public IActionResult ShiftSchedule(int Id)
        {
            
            var entities = _shiftScheduleService.GetPersonalShiftScheduleList(Id);
            var models = EntityHelper.ToViewModel(entities).ToString();

            return View(models);
        }
        //[HttpPost]
        //public IActionResult ShiftSchedule()
        //{

        //}
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
