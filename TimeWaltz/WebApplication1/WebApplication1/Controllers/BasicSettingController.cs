using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.Entity;
using WebApplication1.Models.Enums;
using WebApplication1.Models.ViewModel;
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
            var sDropDownData = _personalDataService.GetShiftNameDropDownData();
            var dDropDownData = _personalDataService.GetDepartmentDropDownData();

            var model = new PersonalDataCreateViewModel
            {
                GenderSelectItems = DropDownHelper.GetGenderDropDownList(),
                DepartmentNameSelectItem = DropDownHelper.GetDepartmentNameDropDownList(dDropDownData),
                ShiftNameSelectItems = DropDownHelper.GetShiftNameDropDownList(sDropDownData)
            };

            return View(model);
        }
           
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PersonalDataCreate(PersonalDataCreateViewModel model)
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
        public IActionResult PersonalDataList()
        {
            var entities = _personalDataService.GetPersonalDataList();            
            var models = EntityHelper.ToViewModel(entities);
            
            return View(models);
        }
      
        public IActionResult PersonalDataDelete(int id)
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
        public IActionResult PersonalDataEdit(int id)
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
        public IActionResult PersonalDataEdit(PersonalDataEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _personalDataService.EditPersonalData(model);
            return RedirectToAction("ListPersonalData");
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
            return RedirectToAction("ListGradeTable");
        }

        [HttpGet]
        public IActionResult SpecialGradeList()
        {
            var entities = _gradeTableService.GetGradeTableList();
            var models = EntityHelper.ToViewModel(entities);
            return View(models);
        }
        
        public IActionResult SpecialGradeDelete(int id)
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
        public IActionResult EditGradeTable(SpecialGradeEditViewModel model)
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
        public IActionResult PublicHolidayList(PublicHolidayViewModel selectedModel)
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
        public IActionResult PublicHolidayEdit(int Id)
        {
            var entity = _publicHolidayService.GetPublicHolidayOrNull(Id);
            var model = EntityHelper.ToEditViewModel(entity);
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
            return RedirectToAction("ListPublicHoliday");
        }

        public IActionResult PublicHolidayDelete(int id)
        {
            //TODO: 驗證是否為登入者有權限的資料
            var entity = _publicHolidayService.GetPublicHolidayOrNull(id);
            _publicHolidayService.DeleteVacationType(entity);
            return RedirectToAction("ListPublicHoliday");
        }
        [HttpGet]
        public IActionResult ShiftScheduleCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShiftScheduleCreate(CreateShiftSchedulesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = ViewModelHelper.ToEntity(model);
            _shiftScheduleService.CreateShiftSchedule(entity);
            return RedirectToAction("ListShiftSchedule");
        }
        public IActionResult ShiftScheduleList()
        {  
            var entities = _shiftScheduleService.GetShiftScheduleList();
            var models = EntityHelper.ToViewModel(entities);

            return View(models);
        }

        [HttpPost]
        public IActionResult ShiftScheduleList(ShiftSchedulesViewModel selectedModel)
        {
            var entities = _shiftScheduleService.GetSelectedShiftScheduleList(selectedModel);
            if(entities != null)
            {
                var models = EntityHelper.ToViewModel(entities);
                return View(models);
            }           
            return View(selectedModel);
        }
        public IActionResult ShiftScheduleDelete(int id)
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
        public IActionResult ShiftScheduleEdit(int id)
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
        public IActionResult ShiftScheduleEdit(EditShiftSchedulesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _shiftScheduleService.EditShiftSchedule(model);
            return RedirectToAction("ListShiftSchedule");
        }


        [HttpGet]
        public IActionResult VacationTypeCreate()
        {
            
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult VacationTypeCreate(VacationCreateViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var entity = ViewModelHelper.ToEntity(model);
        //    _vacationTypeService.CreateVacationType(entity);
        //    return RedirectToAction("ListVacationType");
        //}
        [HttpGet]
        public IActionResult VacationTypeList()
        {


            //var entities = _vacationTypeService.GetVacationDetailsList();
            //var model = EntityHelper.ToViewModel(entities);
            return View();
        }
        //[HttpPost]
        //public IActionResult VacationTypeList(VacationViewModel selectedModel)
        //{
        //    var entities = _vacationTypeService.GetSelectedVacationTypeList(selectedModel);
        //    if (entities != null)
        //    {
        //        var models = EntityHelper.ToViewModel(entities);
        //        return View(models);
        //    }
        //    else
        //    {
        //        return View(selectedModel);
        //    }
        //}


        

        public IActionResult VacationTypeDelete(int id)
        {
            var entity = _vacationTypeService.GetVacationTypeOrNull(id);
            _vacationTypeService.DeleteVacationType(entity);
            return RedirectToAction("ListVacationType");
        }

        [HttpGet]
        public IActionResult VacationTypeEdit(int id)
        {
            

            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult VacationTypeEdit(VacationEditViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    _vacationTypeService.EditVacationType(model);
        //    return RedirectToAction("ListVacationType");
        //}


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
           
            
            return RedirectToAction("ListPersonalData");

        }

        [HttpPost]
        public IActionResult DepartmentCreate(DepartmentCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = ViewModelHelper.ToEntity(model);
            _departmentService.CreateDepartment(entity);
            return RedirectToAction("Department");
            
        }
        [HttpGet]
        public IActionResult Department()
        {
            var entities = _departmentService.GetDepartment();
            var models = EntityHelper.ToViewModel(entities);
            return View(models);

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
        public IActionResult DepartmentEdit(int id)
        {
            var employeeNameDropDownData = _departmentService.GetEmployeeDropDownData();
            var entity = _departmentService.GetDepartmentOrNull(id);
            if (employeeNameDropDownData != null)
            {
                var model = EntityHelper.ToEditViewModel(entity);

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
