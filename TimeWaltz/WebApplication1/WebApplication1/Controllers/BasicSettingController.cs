  using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.Enums;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class BasicSettingController : Controller
    {

        private readonly VacationTypeService _vacationTypeService;

        public BasicSettingController(VacationTypeService vacationTypeService)
        {
            _vacationTypeService = vacationTypeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreatePublicHoliday()
        {            
            return View();
        }
        [HttpPost]
        public IActionResult CreatePublicHoliday(PublicHolidayViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            var entity = ViewModelHelper.ToEntity(model);
            _vacationTypeService.CreateVacationType(entity);
            return RedirectToAction("ListVacationType");
        }
        [HttpGet]
        public IActionResult CreateVacationType()
        {
            var model = new VacationTypeViewModel
            {
                GenderSelectItems = Enum.GetValues(typeof(GenderEnum)).Cast<GenderEnum>().Select(c => new SelectListItem
                {
                    Text = c.ToString(),
                    Value = ((int)c).ToString()
                }).ToList(),
                CycleSelectItems = Enum.GetValues(typeof(CycleEnum)).Cast<CycleEnum>().Select(c => new SelectListItem
                {
                    Text = c.ToString(),
                    Value = ((int)c).ToString()
                }).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult ListVacationType()
        {
            
            
            var entities = _vacationTypeService.GetVacationDetailsList();
            var model = EntityHelper.ToViewModel(entities);
            return View(model);
        }
        [HttpPost]
        public IActionResult ListVacationType(VacationTypeViewModel selectedModel)
        {
            
            var entities = _vacationTypeService.GetSelectedShiftScheduleList(selectedModel);
            if(entities != null)
            {
                var models = EntityHelper.ToViewModel(entities);
                return View(models);
            }
            else
            {
                return View(selectedModel);
            }
        }
            

        [HttpPost]
        public IActionResult CreateVacationType(VacationTypeViewModel model)
        {
            if(ModelState.IsValid)
            {
                return View(model);
            }
            var entity = ViewModelHelper.ToEntity(model);
            _vacationTypeService.CreateVacationType(entity);
            return RedirectToAction("ListVacationType");
        }

        public IActionResult DeleteVacationType(int id)
        {
            var entity = _vacationTypeService.GetVacationTypeOrNull(id);
            _vacationTypeService.DeleteVacationType(entity);
            return RedirectToAction("ListVacationType");
        }

        [HttpGet]
        public IActionResult EditVacationType(int id)
        {            
            var entity = _vacationTypeService.GetVacationTypeOrNull(id);
            if (entity == null)
            {
                return RedirectToAction("ListVacationType");
            }
            var model = EntityHelper.ToViewModel(entity);

            return View(model);
        }

        [HttpPost]
        public IActionResult EditVacationType(VacationTypeViewModel model)
        {
            if(ModelState.IsValid)
            {
                return View(model);
            }
            _vacationTypeService.EditVacationType(model);
            return RedirectToAction("ListVacationType");
        }
    }
}
