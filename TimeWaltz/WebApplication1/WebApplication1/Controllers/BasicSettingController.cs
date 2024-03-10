using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.BasicSettingViewModels;
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
        public IActionResult PersonalDataCreate()
        {
            return View();
        }
           
       
        [HttpGet]
        public IActionResult PersonalData()
        {
            return View();
        }
      
        public IActionResult PersonalDataDelete(int id)
        {
            var entity = _personalDataService.GetPersonalDataOrNull(id);
            if(entity == null)
            {
                return RedirectToAction("PersonalData");
            }
            _personalDataService.DeletePersonalData(entity);
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
            var entity = ViewModelConverter.ToEntity(model);
            _gradeTableService.CreateGradeTable(entity);
            return RedirectToAction("SpecialGrade");
        }

        [HttpGet]
        public IActionResult SpecialGrade()
        {
            var entities = _gradeTableService.GetGradeTableList();
            var models = EntityContverter.ToViewModel(entities);
            return View(models);
        }
        
        public IActionResult SpecialGradeDelete(int id)
        {
            var entity = _gradeTableService.GetGradeTableOrNull(id);
            if(entity != null)
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
            var model = EntityContverter.ToEditViewModel(entity);

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
            //TODO:等人事基本資料完成後，讓使用者在選擇了周年制的情況下disable約定給假時間
            var entity = _specialHolidayService.GetSpecialHoliday();            
            var model = EntityContverter.ToViewModel(entity);
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
        public IActionResult CreatePublicHoliday(PublicHolidayCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = ViewModelConverter.ToEntity(model);
            _publicHolidayService.CreatePublicHoliday(entity);
            return RedirectToAction("PublicHoliday");
        }

        public IActionResult PublicHoliday()
        {
            var entities = _publicHolidayService.GetPublicHolidayList();
            var models = EntityContverter.ToViewModel(entities);

            return View(models);
        }

        [HttpPost]
        public IActionResult PublicHoliday(PublicHolidayViewModel selectedModel)
        {
            var entities = _publicHolidayService.GetSelectedPublicHolidayList(selectedModel);
            if(entities != null)
            {
                var models = EntityContverter.ToViewModel(entities);
                return View(models);
            }
            return View(selectedModel);
        }
        [HttpGet]
        public IActionResult PublicHolidayEdit(int Id)
        {
            var entity = _publicHolidayService.GetPublicHolidayOrNull(Id);
            var model = EntityContverter.ToEditViewModel(entity);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PublicHolidayEdit(PublicHolidayEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _publicHolidayService.EditPublicHoliday(model);
            return RedirectToAction("PublicHoliday");
        }

        public IActionResult PublicHolidayDelete(int id)
        {
            //TODO: 驗證是否為登入者有權限的資料
            var entity = _publicHolidayService.GetPublicHolidayOrNull(id);
            _publicHolidayService.DeleteVacationType(entity);
            return RedirectToAction("PublicHoliday");
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
            var entity = ViewModelConverter.ToEntity(model);
            _shiftScheduleService.CreateShiftSchedule(entity);
            return RedirectToAction("ShiftSchedule");
        }
        public IActionResult ShiftSchedule()
        {  
            var entities = _shiftScheduleService.GetShiftScheduleList();
            var models = EntityContverter.ToViewModel(entities);

            return View(models);
        }

        [HttpPost]
        public IActionResult ShiftSchedule(ShiftSchedulesViewModel selectedModel)
        {
            var entities = _shiftScheduleService.GetSelectedShiftScheduleList(selectedModel);
            if(entities != null)
            {
                var models = EntityContverter.ToViewModel(entities);
                return View(models);
            }           
            return View(selectedModel);
        }
        public IActionResult ShiftScheduleDelete(int id)
        {
            var entity = _shiftScheduleService.GetShiftScheduleOrNull(id);
            if(entity == null)
            {
                return RedirectToAction("ShiftSchedule");
            }
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
            var model = EntityContverter.ToEditViewModel(entity);

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
            var FlextimeEntity = _flextimeService.GetFlextime();
            var Flextimemodel = EntityContverter.ToViewModel(FlextimeEntity);
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

        
        public IActionResult DepartmentCreate()
        {           
            var employeeNameDropDownData = _departmentService.GetEmployeeDropDownData();
            

            if (employeeNameDropDownData != null) 
            { 
                var model = new DepartmentCreateViewModel
                {                    
                    EmployeeNameSelectList = DropDownHelper
                       .GetEmployeeNameDropDownList(employeeNameDropDownData),
                };
                return View(model);
            }
           
            
            return RedirectToAction("PersonalData");

        }

        [HttpPost]
        public IActionResult DepartmentCreate(DepartmentCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = ViewModelConverter.ToEntity(model);
            _departmentService.CreateDepartment(entity);
            return RedirectToAction("Department");
            
        }
        [HttpGet]
        public IActionResult Department()
        {
            var entities = _departmentService.GetDepartment();
            var models = EntityContverter.ToViewModel(entities);
            return View(models);

        }

        [HttpPost]
        public IActionResult Department(DepartmentViewModel selectedModel)
        {
            var entities = _departmentService.GetSelectedDepartment(selectedModel);
            if (entities != null)
            {
                var models = EntityContverter.ToViewModel(entities);
                return View(models);
            }
            else
            {
                return View(selectedModel);
            }

        }


        [HttpGet]
        public IActionResult DepartmentEdit(int id)
        {
            var employeeNameDropDownData = _departmentService.GetEmployeeDropDownData();
            var entity = _departmentService.GetDepartmentOrNull(id);
            if (employeeNameDropDownData != null)
            {
                var model = EntityContverter.ToEditViewModel(entity);

                model.EmployeeNameSelectList = DropDownHelper
                       .GetEmployeeNameDropDownList(employeeNameDropDownData);

                return View(model);
            }

            return RedirectToAction("PersonalData");
        }

        [HttpPost]
        public IActionResult DepartmentEdit(DepartmentEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _departmentService.EditDepartment(model);
            return RedirectToAction("Department");
        }


        public IActionResult DepartmentDelete(int id)
        {
            var entity = _departmentService.GetDepartmentOrNull(id);
            _departmentService.DeleteDepartment(entity);
            return RedirectToAction("Department");
        }


    }
}
