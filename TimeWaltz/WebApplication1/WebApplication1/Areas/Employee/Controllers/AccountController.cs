using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
