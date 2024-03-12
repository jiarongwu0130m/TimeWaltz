using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class ApplicationFormController : Controller
    {
        private readonly CompRequestService _compRequestService;

        public ApplicationFormController(CompRequestService compRequestService)
        {
            _compRequestService = compRequestService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateLeaveRequest()
        {
            return RedirectToAction("LeaveRecord", "PersonalRecord");
        }
        [HttpGet]
        public IActionResult CompRequest()
        {
            var entities = _compRequestService.GetCompRequest();
            var model = EntityHelper.ToViewModel(entities);
            return View(model);
        }
        [HttpGet]
        public IActionResult CompRequestCreate()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CompRequestDetail(int id)
        {
            return View();
        }
    }
}
