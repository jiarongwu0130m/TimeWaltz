using WebApplication1.Models;
using WebApplication1.Models.ApplicationFormViewModels;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Models.Entity;
using WebApplication1.Models.Enums;

namespace WebApplication1.Helpers
{
    public class ViewModelConverter
    {
        public static Employee ToEntity(PersonalDataCreateDto model)
        {
            var entity = new Employee
            {
                ShiftScheduleId = model.ShiftScheduleId,
                DepartmentId = model.DepartmentId,
                Name = model.Name,
                HireDate = model.HireDate,
                Email = model.Email,
                Gender = (GenderEnum)model.Gender,
                EmployeesNo = model.EmployeesNo,
            };
            return entity;
        }
        public static SpecialGrade ToEntity(SpecialGradeCreateViewModel model)
        {
            var entity = new SpecialGrade
            {
                ServiceLength = model.ServiceLength,
                Days = model.Days,
            };
            return entity;
        }

        public static ShiftSchedule ToEntity(ShiftSchedulesCreateViewModel model)
        {
            var entity = new ShiftSchedule
            {
                Id = model.Id,
                ShiftsName = model.ShiftName,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                BreakTime = (int)Math.Round(model.BreakTimeHours * 60.0),
                MaxAdditionalClockIn = model.MaxAdditionalClockIn,
            };
            return entity;
        }

        public static VacationDetail ToEntity(VacationCreateDto model)
        {

            var entity = new VacationDetail
            {
                VacationType = model.VacationType,
                Gender = model.Gender,
                NumberOfDays = model.NumberOfDays,
                Cycle = model.Cycle,
                MinVacationHours = model.MinVacationHours
            };
            return entity;
        }
       
        public static PublicHoliday ToEntity(PublicHolidayCreateViewModel model)
        {
            var entity = new PublicHoliday
            {
                HolidayName = model.HolidayName,
                Date = model.Date
            };
            return entity;
        }

        public static Department ToEntity(DepartmentCreateViewModel model)
        {
            var entity = new Department
            {                
                DepartmentName = model.DepartmentName,
                EmployeesId = model.EmployeesId,
            };
            return entity;
        }

        public static AdditionalClockIn ToEntity(CompRequestCreateViewModel model)
        {
            var entity = new AdditionalClockIn
            {
                EmployeesId = model.EmployeesId,
                AdditionalTime = model.AdditionalTime,
                Status = model.Status,
                Reason = model.Reason,
                ApprovalEmployeeId = model.ApprovalEmployeeId,
            };
            return entity;
        }


    }
}
