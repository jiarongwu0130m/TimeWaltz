﻿using Microsoft.AspNetCore.Mvc;
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

        private readonly ShiftScheduleService _shiftScheduleService;
        private readonly VacationTypeService _vacationTypeService;      
        private readonly PublicHolidayService _publicHolidayService;
        private readonly FlextimeService _flextimeService;

        public BasicSettingController(VacationTypeService vacationTypeService, PublicHolidayService publicHolidayService, ShiftScheduleService shiftScheduleService, FlextimeService flextimeService)
        {
            _shiftScheduleService = shiftScheduleService;
            _vacationTypeService = vacationTypeService;
            _publicHolidayService = publicHolidayService;
            _flextimeService = flextimeService;
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
        public IActionResult CreatePublicHoliday(CreatePublicHolidayViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = ViewModelHelper.ToEntity(model);
            _publicHolidayService.CreatePublicHoliday(entity);
            return RedirectToAction("ListPublicHoliday");
        }

        public IActionResult ListPublicHoliday()
        {
            var entities = _publicHolidayService.GetPublicHolidayList();
            var models = EntityHelper.ToViewModel(entities);

            return View(models);
        }

        [HttpPost]
        public IActionResult ListPublicHoliday(PublicHolidayViewModel selectedModel)
        {
            var entities = _publicHolidayService.GetSelectedPublicHolidayList(selectedModel);
            if(entities != null)
            {
                var models = EntityHelper.ToViewModel(entities);
                return View(models);
            }
            return View(selectedModel);
        }
        [HttpGet]
        public IActionResult EditPublicHoliday(int Id)
        {
            var entity = _publicHolidayService.GetPublicHolidayOrNull(Id);
            var model = EntityHelper.ToViewModel(entity);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditPublicHoliday(EditPublicHolidayViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _publicHolidayService.EditPublicHoliday(model);
            return RedirectToAction("ListPublicHoliday");
        }

        public IActionResult DeletePublicHoliday(int id)
        {
            //TODO: 驗證是否為登入者有權限的資料
            var entity = _publicHolidayService.GetPublicHolidayOrNull(id);
            _publicHolidayService.DeleteVacationType(entity);
            return RedirectToAction("ListPublicHoliday");
        }

        public IActionResult ListShiftSchedule()
        {

            var Id = 1;
            var entities = _shiftScheduleService.GetPersonalShiftScheduleList(Id);
            var models = EntityHelper.ToViewModel(entities);

            return View(models);
        }

        [HttpPost]
        public IActionResult ListShiftSchedule(ShiftSchedulesViewModel selectedModel)
        {
            var entities = _shiftScheduleService.GetSelectedShiftScheduleList(selectedModel);
            if(entities != null)
            {
                var models = EntityHelper.ToViewModel(entities);
                return View(models);
            }           
            return View(selectedModel);
        }

        [HttpGet]
        public IActionResult CreateVacationType()
        {
            var model = new CreateVacationTypeViewModel
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
            var entities = _vacationTypeService.GetSelectedVacationTypeList(selectedModel);
            if (entities != null)
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
        public IActionResult CreateVacationType(CreateVacationTypeViewModel model)
        {
            if (ModelState.IsValid)
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
        public IActionResult EditVacationType(EditVacationTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _vacationTypeService.EditVacationType(model);
            return RedirectToAction("ListVacationType");
        }

        public IActionResult Flextime()
        {
            var model = _flextimeService.GetFlextimeViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Flextime(FlextimeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _flextimeService.UpdateFlextime(model);
            return View();
        }
    }
}
