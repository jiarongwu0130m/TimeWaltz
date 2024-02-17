using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ApplicationFormController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateLeaveRequest()
        {
            return RedirectToAction("LeaveRecord", "PersonalRecord");
        }
    }
}
