using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.Entity;
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
        public IActionResult CreateVacationType()
        {
            var model = new VacationTypeViewModel
            {
                CycleSelectItems = Enum.GetValues(typeof(CycleEnum)).Cast<CycleEnum>().Select(c => new SelectListItem
                {
                    Text = c.ToString(),
                    Value = ((int)c).ToString()
                }).ToList(),
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateVacationType(VacationTypeViewModel model)
        {
            if(ModelState.IsValid)
            {
                return View(model);
            }
            var entity = ViewModelToEntity.ConvertToEntity(model);
            _vacationTypeService.CreateVacationType(entity);
            return RedirectToAction();
        }
    }
}
