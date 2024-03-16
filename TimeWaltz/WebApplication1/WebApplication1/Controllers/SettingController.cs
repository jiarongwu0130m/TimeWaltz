using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class SettingController : Controller
    {
        public IActionResult BillBoard()
        {
            return View();
        }
    }
}
