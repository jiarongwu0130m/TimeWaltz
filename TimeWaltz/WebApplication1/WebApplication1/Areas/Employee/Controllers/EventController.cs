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

            return View();
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
