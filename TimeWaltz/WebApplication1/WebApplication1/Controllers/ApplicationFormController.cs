using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models.ApplicationFormViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class ApplicationFormController : Controller
    {
        private readonly CompRequestService _compRequestService;
        private readonly OvertimeRequestService _overtimeRequestService;

        public ApplicationFormController(CompRequestService compRequestService,OvertimeRequestService overtimeRequestService)
        {
            _compRequestService = compRequestService;
            _overtimeRequestService = overtimeRequestService;
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
        public IActionResult OvertimeRequest()
        {
            return View();
        }
        public IActionResult OvertimeRequestCreate()
        {
            return View();
        }

        public IActionResult OvertimeRequestDetail(int id)
        {
            return View();
        }
    }
}
