using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class EventController : Controller
    {
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
