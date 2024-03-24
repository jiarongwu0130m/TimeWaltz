using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters;

namespace WebApplication1.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(AuthenticationSchemes = "EmployeeAuthScheme")]
    [TimeWaltzMobileAuthorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("clock", "event");
        }
    }
}
