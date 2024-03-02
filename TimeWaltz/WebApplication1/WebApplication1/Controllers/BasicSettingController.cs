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
        private readonly PersonalDataService _personalDataService;
        private readonly GradeTableService _gradeTableService;
        private readonly SpecialHolidayService _specialHolidayService;
        private readonly ShiftScheduleService _shiftScheduleService;
        private readonly VacationTypeService _vacationTypeService;      
        private readonly PublicHolidayService _publicHolidayService;
        private readonly FlextimeService _flextimeService;
        private readonly DepartmentService _departmentService;

        public BasicSettingController(PersonalDataService personalDataService, GradeTableService gradeTableService, SpecialHolidayService specialHolidayService,VacationTypeService vacationTypeService, PublicHolidayService publicHolidayService, ShiftScheduleService shiftScheduleService, FlextimeService flextimeService, DepartmentService departmentService) { 
        
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
        public IActionResult CreatePersonalData()
        {
            var sDropDownData = _personalDataService.GetShiftNameDropDownData();
            var dDropDownData = _personalDataService.GetDepartmentDropDownData();

            var model = new CreatePersonalDataViewModel
            {
                GenderSelectItems = DropDownHelper.GetGenderDropDownList(),
                DepartmentNameSelectItem = DropDownHelper.GetDepartmentNameDropDownList(dDropDownData),
                ShiftNameSelectItems = DropDownHelper.GetShiftNameDropDownList(sDropDownData) 
            };

            return View(model);
        }
           
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePersonalData(CreatePersonalDataViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = ViewModelHelper.ToEntity(model);
            _personalDataService.CreatePersonalData(entity);
            return RedirectToAction("ListPersonalData");
        }
        [HttpGet]
        public IActionResult ListPersonalData()
        {
            var entities = _personalDataService.GetPersonalDataList();            
            var models = EntityHelper.ToViewModel(entities);
            
            return View(models);
        }
      
        public IActionResult DeletePersonalData(int id)
        {
            var entity = _personalDataService.GetPersonalDataOrNull(id);
            if(entity == null)
            {
                return RedirectToAction("ListPersonalData");
            }
            _personalDataService.DeletePersonalData(entity);
            return RedirectToAction("ListPersonalData");
        }

        [HttpGet]
        public IActionResult EditPersonalData(int id)
        {
            var sDropDownData = _personalDataService.GetShiftNameDropDownData();
            var dDropDownData = _personalDataService.GetDepartmentDropDownData();

            
            var entity = _personalDataService.GetPersonalDataOrNull(id);
            if (entity == null)
            {
                return RedirectToAction("ListPersonalData");
            }
            var model = EntityHelper.ToEditViewModel(entity);           
            model.DepartmentNameSelectItem = DropDownHelper.GetDepartmentNameDropDownList(dDropDownData);
            model.ShiftNameSelectItems = DropDownHelper.GetShiftNameDropDownList(sDropDownData);
            

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPersonalData(EditPersonalDataViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _personalDataService.EditPersonalData(model);
            return RedirectToAction("ListPersonalData");
        }

       

        [HttpGet]
        public IActionResult CreateGradeTable()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateGradeTable(CreateGradeTableViewModel model)
        {
            if (!ModelState.IsValid && model != null)
            {
                return View(model);
            }
            var entity = ViewModelHelper.ToEntity(model);
            _gradeTableService.CreateGradeTable(entity);
            return RedirectToAction("ListGradeTable");
        }

        [HttpGet]
        public IActionResult ListGradeTable()
        {
            var entities = _gradeTableService.GetGradeTableList();
            var models = EntityHelper.ToViewModel(entities);
            return View(models);
        }
        
        public IActionResult DeleteGradeTable(int id)
        {
            var entity = _gradeTableService.GetGradeTableOrNull(id);
            if(entity != null)
            {
                _gradeTableService.DeleteGradeTable(entity);
                return RedirectToAction("ListGradeTable");
            }
            
            return RedirectToAction("ListGradeTable");
        }

        [HttpGet]        
        public IActionResult EditGradeTable(int id)
        {
            var entity = _gradeTableService.GetGradeTableOrNull(id);
            if (entity == null)
            {
                return RedirectToAction("ListGradeTable");
            }
            var model = EntityHelper.ToEditViewModel(entity);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditGradeTable(EditGradeTableViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _gradeTableService.EditGradeTable(model);
            return RedirectToAction("ListGradeTable");
        }
        [HttpGet]
        public IActionResult SpecialHoliday()
        {
            //TODO:等人事基本資料完成後，讓使用者在選擇了周年制的情況下disable約定給假時間
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
        public IActionResult CreatePublicHoliday()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            var model = EntityHelper.ToEditViewModel(entity);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [HttpGet]
        public IActionResult CreateShiftSchedule()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateShiftSchedule(CreateShiftSchedulesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = ViewModelHelper.ToEntity(model);
            _shiftScheduleService.CreateShiftSchedule(entity);
            return RedirectToAction("ListShiftSchedule");
        }
        public IActionResult ListShiftSchedule()
        {  
            var entities = _shiftScheduleService.GetShiftScheduleList();
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
        public IActionResult DeleteShiftSchedule(int id)
        {
            var entity = _shiftScheduleService.GetShiftScheduleOrNull(id);
            if(entity == null)
            {
                return RedirectToAction("ListShiftSchedule");
            }
            _shiftScheduleService.DeleteShiftSchedule(entity);
            return RedirectToAction("ListShiftSchedule");
        }

        [HttpGet]
        public IActionResult EditShiftSchedule(int id)
        {
            var entity = _shiftScheduleService.GetShiftScheduleOrNull(id);
            if (entity == null)
            {
                return RedirectToAction("ListShiftSchedule");
            }
            var model = EntityHelper.ToEditViewModel(entity);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditShiftSchedule(EditShiftSchedulesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _shiftScheduleService.EditShiftSchedule(model);
            return RedirectToAction("ListShiftSchedule");
        }


        [HttpGet]
        public IActionResult CreateVacationType()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateVacationType(CreateVacationTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = ViewModelHelper.ToEntity(model);
            _vacationTypeService.CreateVacationType(entity);
            return RedirectToAction("ListVacationType");
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
            var model = EntityHelper.ToEditViewModel(entity);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            var FlextimeEntity = _flextimeService.GetFlextime();
            var Flextimemodel = EntityHelper.ToViewModel(FlextimeEntity);
            return View(Flextimemodel);
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

        
        public IActionResult CreateDepartment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateDepartment(DepartmentCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = ViewModelHelper.ToEntity(model);
            _departmentService.CreateDepartment(entity);
            return RedirectToAction("Department");
            
        }

        public IActionResult Department()
        {
            var entities = _departmentService.GetDepartment();
            var model = EntityHelper.ToViewModel(entities);
            return View(model);

        }

        [HttpPost]
        public IActionResult Department(DepartmentViewModel selectedModel)
        {
            var entities = _departmentService.GetSelectedDepartment(selectedModel);
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


        [HttpGet]
        public IActionResult EditDepartment(int id)
        {
            var entity = _departmentService.GetDepartmentOrNull(id);
            if (entity == null)
            {
                return RedirectToAction("Department");
            }
            var model = EntityHelper.ToViewModel(entity);

            return View(model);
        }

        [HttpPost]
        public IActionResult EditDepartment(DepartmentEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _departmentService.EditDepartment(model);
            return RedirectToAction("Department");
        }


        public IActionResult DeleteDepartment(int id)
        {
            var entity = _departmentService.GetDepartmentOrNull(id);
            _departmentService.DeleteDepartment(entity);
            return RedirectToAction("Department");
        }


    }
}
