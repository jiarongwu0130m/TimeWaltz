using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApplication1.Filters;
using WebApplication1.Helpers;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class BasicSettingController : Controller
    {
        private readonly PersonalDataService _personalDataService;
        private readonly GradeTableService _gradeTableService;
        private readonly SpecialHolidayService _specialHolidayService;
        private readonly ShiftScheduleService _shiftScheduleService;
        private readonly VacationTypeService _vacationTypeService;
        private readonly PublicHolidayService _publicHolidayService;
        private readonly FlextimeService _flextimeService;
        private readonly DepartmentService _departmentService;

        public BasicSettingController(PersonalDataService personalDataService, GradeTableService gradeTableService, SpecialHolidayService specialHolidayService, VacationTypeService vacationTypeService, PublicHolidayService publicHolidayService, ShiftScheduleService shiftScheduleService, FlextimeService flextimeService, DepartmentService departmentService)
        {

            _shiftScheduleService = shiftScheduleService;
            _personalDataService = personalDataService;
            _gradeTableService = gradeTableService;
            _specialHolidayService = specialHolidayService;
            _vacationTypeService = vacationTypeService;
            _publicHolidayService = publicHolidayService;
            _flextimeService = flextimeService;
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult SpecialVacation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SpecialVacationEdit(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult SpecialVacationCreate()
        {
            return View();
        }

        //[Authorize]
        [TimeWaltzAuthorize]
        [HttpGet]
        public IActionResult PersonalData()
        {
            return View();
        }

        public IActionResult PersonalDataDelete(int id)
        {
            var entity = _personalDataService.GetPersonalDataOrNull(id);
            if (entity == null)
            {
                return RedirectToAction("PersonalData");
            }
            _personalDataService.SoftDeletePersonalData(entity);
            return RedirectToAction("PersonalData");
        }

        [HttpGet]
        public IActionResult PersonalDataEdit(int id)
        {
            return View();
        }





        [HttpGet]
        public IActionResult SpecialGradeCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SpecialGradeCreate(SpecialGradeCreateViewModel model)
        {
            if (!ModelState.IsValid && model != null)
            {
                return View(model);
            }
            var entity = ViewModelHelper.ToEntity(model);
            _gradeTableService.CreateGradeTable(entity);
            return RedirectToAction("SpecialGrade");
        }

        [HttpGet]
        public IActionResult SpecialGrade()
        {
            var entities = _gradeTableService.GetGradeTableList();
            var models = EntityHelper.ToViewModel(entities);
            return View(models);
        }

        public IActionResult SpecialGradeDelete(int id)
        {
            var entity = _gradeTableService.GetGradeTableOrNull(id);
            if (entity != null)
            {
                _gradeTableService.DeleteGradeTable(entity);
                return RedirectToAction("SpecialGrade");
            }

            return RedirectToAction("SpecialGrade");
        }

        [HttpGet]
        public IActionResult EditGradeTable(int id)
        {
            var entity = _gradeTableService.GetGradeTableOrNull(id);
            if (entity == null)
            {
                return RedirectToAction("SpecialGrade");
            }
            var model = EntityHelper.ToEditViewModel(entity);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditGradeTable(SpecialGradeEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _gradeTableService.EditGradeTable(model);
            return RedirectToAction("SpecialGrade");
        }
        [HttpGet]
        public IActionResult SpecialHoliday()
        {
            var entity = _specialHolidayService.GetSpecialHoliday();
            var model = EntityHelper.ToViewModel(entity);
            ViewBag.HowToGiveDropDownList = DropDownHelper.GetHowToGiveDropDownList();

            return View(model);
        }
        [HttpPost]
        public IActionResult SpecialHoliday(SpecialHolidayViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.HowToGiveDropDownList = DropDownHelper.GetHowToGiveDropDownList();
                return View(model);
            }
            ViewBag.HowToGiveDropDownList = DropDownHelper.GetHowToGiveDropDownList();
            _specialHolidayService.EditSpecialHoliday(model);
            return View(model);
        }


        [HttpGet]
        public IActionResult PublicHolidayCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PublicHolidayCreate(PublicHolidayCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = ViewModelHelper.ToEntity(model);
            _publicHolidayService.CreatePublicHoliday(entity);
            return RedirectToAction("PublicHoliday");
        }

        public IActionResult PublicHoliday()
        {
            var entities = _publicHolidayService.GetPublicHolidayList();
            var models = EntityHelper.ToViewModel(entities);

            return View(models);
        }

        [HttpPost]
        public IActionResult PublicHoliday(PublicHolidayViewModel selectedModel)
        {
            var entities = _publicHolidayService.GetSelectedPublicHolidayList(selectedModel);
            if (entities != null)
            {
                var models = EntityHelper.ToViewModel(entities);
                return View(models);
            }
            return View(selectedModel);
        }
        [HttpGet]
        public IActionResult PublicHolidayEdit(int Id)
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult ShiftScheduleCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShiftScheduleCreate(ShiftSchedulesCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = ViewModelHelper.ToEntity(model);
            _shiftScheduleService.CreateShiftSchedule(entity);
            return RedirectToAction("ShiftSchedule");
        }
        public IActionResult ShiftSchedule()
        {
            var entities = _shiftScheduleService.GetShiftScheduleList();
            var models = EntityHelper.ToViewModel(entities);

            return View(models);
        }

        [HttpPost]
        public IActionResult ShiftSchedule(ShiftSchedulesViewModel selectedModel)
        {
            var entities = _shiftScheduleService.GetSelectedShiftScheduleList(selectedModel);
            if (entities != null)
            {
                var models = EntityHelper.ToViewModel(entities);
                return View(models);
            }
            return View(selectedModel);
        }
        public IActionResult ShiftScheduleDelete(int id)
        {
            var entity = _shiftScheduleService.GetShiftScheduleOrNull(id);
            if (entity == null)
            {
                return RedirectToAction("ShiftSchedule");
            }
            _shiftScheduleService.ClearAllFK(id);
            _shiftScheduleService.DeleteShiftSchedule(entity);
            return RedirectToAction("ShiftSchedule");
        }

        [HttpGet]
        public IActionResult ShiftScheduleEdit(int id)
        {
            var entity = _shiftScheduleService.GetShiftScheduleOrNull(id);
            if (entity == null)
            {
                return RedirectToAction("ShiftSchedule");
            }
            var model = EntityHelper.ToEditViewModel(entity);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShiftScheduleEdit(ShiftSchedulesEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _shiftScheduleService.EditShiftSchedule(model);
            return RedirectToAction("ShiftSchedule");
        }


        [HttpGet]
        public IActionResult VacationTypeCreate()
        {

            return View();
        }

        [HttpGet]
        public IActionResult VacationType()
        {
            return View();
        }
        public IActionResult VacationTypeDelete(int id)
        {
            var entity = _vacationTypeService.GetVacationTypeOrNull(id);
            _vacationTypeService.DeleteVacationType(entity);
            return RedirectToAction("VacationType");
        }

        [HttpGet]
        public IActionResult VacationTypeEdit(int id)
        {
            return View();
        }

        public IActionResult Flextime()
        {
            return View();
        }

        public IActionResult DepartmentCreate()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Department()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DepartmentEdit(int id)
        {
                return View();
        }
    }
}
