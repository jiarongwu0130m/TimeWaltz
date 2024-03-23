using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters;

namespace WebApplication1.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(AuthenticationSchemes = "EmployeeAuthScheme")]
    [TimeWaltzMobileAuthorize]
    public class QueryController : Controller
    {
        public IActionResult Overtime()
        {
            return View();
        }

        public IActionResult Complement()
        {
            return View();
        }
        public IActionResult ComplementDetail()
        {
            return View();
        }

        public IActionResult ShiftSchedule()
        {
            return View();
        }

        public IActionResult Leave()
        {
            return View();
        }
        public IActionResult LeaveDetail()
        {
            return View();
        }
        public IActionResult Attendance()
        {
            return View();
        }
        public IActionResult Approval()
        {
            return View();
        }

        public IActionResult BillBoard()
        {
            return View();
        }
    }
}
