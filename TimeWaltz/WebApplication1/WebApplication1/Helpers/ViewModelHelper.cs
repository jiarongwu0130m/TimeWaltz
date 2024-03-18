using WebApplication1.Models.BasicSettingViewModels;
﻿using WebApplication1.Models;
using WebApplication1.Models.ApplicationFormViewModels;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Models.Entity;
using WebApplication1.Models.SettingViewModels;

namespace WebApplication1.Helpers
{
    public class ViewModelHelper
    {
        public static Employee ToEntity(PersonalDataCreateViewModel model)
        {
            var entity = new Employee
            {
                ShiftScheduleId = model.ShiftScheduleId,
                DepartmentId = model.DepartmentId,
                Name = model.Name,
                HireDate = model.HireDate,
                Email = model.Email,
                Gender = model.Gender,
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

        public static OvertimeApplication ToEntity(OvertimeRequestDto dto)
        {
            var entity = new OvertimeApplication
            {
                EmployeesId = 1,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Status = dto.Status,
                Reason = dto.Reason,
                ApprovalEmployeeId = 2,
            };
            return entity;
        }

        public static User ToEntity(UserViewModel model)
        {

            var entity = new User
            {
                Account = model.Account,
                //Password = model.Password,
                EmployeesId = model.EmployeesID,
                DepartmentId = (int)model.DepartmentID,
                Stop = model.Stop==1 ? true : false,
                PasswordDate = DateTime.Now
            };
            return entity;
        }

        public static Billboard ToEntity(BillboardCreat dto)
        {
            var entity = new Billboard
            {
                EmployeesId = 1,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Title = dto.Title,
                Content = dto.Content,
            };
            return entity;
        }
        public static Billboard ToEntity(BillboardEditDto dto)
        {
            var entity = new Billboard
            {
                EmployeesId = dto.EmployeesID,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Title = dto.Title,
                Content = dto.Content,
            };
            return entity;
        }
    }
}
