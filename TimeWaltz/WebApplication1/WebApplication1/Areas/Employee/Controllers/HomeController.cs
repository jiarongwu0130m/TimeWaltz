using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
