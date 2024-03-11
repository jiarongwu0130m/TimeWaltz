﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebApplication1.Models;
using WebApplication1.Models.ApplicationFormViewModels;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Models.Entity;
using WebApplication1.Models.Enums;

namespace WebApplication1.Helpers
{
    public class EntityContverter
    {
        private readonly TimeWaltzContext _dbContext;

        public EntityContverter(TimeWaltzContext timeWaltzContext)
        {
            _dbContext = timeWaltzContext;
        }


        public static List<PersonalDataDto> ToPersonalListDto(List<Employee> entities)
        {
            var models = new List<PersonalDataDto>();
            foreach (var entity in entities)
            {
                models.Add(ToPersonalDto(entity));
            }

            return models;
        }

        public static PersonalDataDto ToPersonalDto(Employee entity)
        {


            var model = new PersonalDataDto
            {
                Id = entity.Id,
                ShiftsName = entity.ShiftsName,
                DepartmentName = entity.DepartmentName,
                Name = entity.Name,
                HireDate = entity.HireDate,
                Email = entity.Email,
                Gender = entity.Gender,
                EmployeesNo = entity.EmployeesNo,

            };
            return model;
        }
        public static PersonalDataEditViewModel ToEditViewModel(Employee entity)
        {

            var model = new PersonalDataEditViewModel
            {
                Id = entity.Id,
                ShiftScheduleId = entity.ShiftScheduleId,
                DepartmentId = entity.DepartmentId,
                DepartmentName = entity.DepartmentName,
                Name = entity.Name,
                Email = entity.Email,

            };
            return model;
        }
        public static SpecialGradeEditViewModel ToEditViewModel(SpecialGrade entity)
        {
            var model = new SpecialGradeEditViewModel
            {
                Id = entity.Id,
                ServiceLength = entity.ServiceLength,
                Days = entity.Days,
            };
            return model;
        }
        public static List<SpecialGradeViewModel> ToViewModel(List<SpecialGrade> entities)
        {
            var models = new List<SpecialGradeViewModel>();
            foreach (var entity in entities)
            {
                models.Add(ToViewModel(entity));
            }
            return models;
        }
        public static SpecialGradeViewModel ToViewModel(SpecialGrade entity)
        {
            var model = new SpecialGradeViewModel
            {
                Id = entity.Id,
                ServiceLength = entity.ServiceLength,
                Days = entity.Days,
            };
            return model;
        }
        public static SpecialHolidayViewModel ToViewModel(SpecialHoliday entity)
        {
            var model = new SpecialHolidayViewModel
            {
                Id = entity.Id,
                HowToGive = entity.HowToGive,
                GiveDay = entity.GiveDay,
            };
            return model;
        }

        public static List<ShiftSchedulesViewModel> ToViewModel(List<ShiftSchedule> entities)
        {
            var models = new List<ShiftSchedulesViewModel>();
            foreach (var entity in entities)
            {
                models.Add(ToViewModel(entity));
            }

            return models;
        }
        public static ShiftSchedulesViewModel ToViewModel(ShiftSchedule entity)
        {
            var model = new ShiftSchedulesViewModel
            {
                Id = entity.Id,
                Date = entity.StartTime.Date,
                DayOfWeek = entity.StartTime.DayOfWeek,
                ShiftName = entity.ShiftsName,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                BreakTimeHours = entity.BreakTime / 60.0,
                MaxAdditionalClockIn = entity.MaxAdditionalClockIn,
            };
            return model;
        }
        public static ShiftSchedulesEditViewModel ToEditViewModel(ShiftSchedule entity)
        {
            var model = new ShiftSchedulesEditViewModel
            {
                Id = entity.Id,
                ShiftName = entity.ShiftsName,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                BreakTimeHours = entity.BreakTime / 60.0,
                MaxAdditionalClockIn = entity.MaxAdditionalClockIn,
            };
            return model;
        }
        public static PublicHolidayEditViewModel ToEditViewModel(PublicHoliday entity)
        {
            var model = new PublicHolidayEditViewModel
            {
                Id = entity.Id,
                HolidayName = entity.HolidayName,
                Date = entity.Date,
            };
            return model;
        }

        public static FlextimeViewModel ToViewModel(Flextime entity)
        {
            var model = new FlextimeViewModel
            {
                Id = entity.Id,
                FlexibleTime = entity.FlexibleTime,
                MoveUp = entity.MoveUp,
            };
            return model;
        }

        public static PublicHolidayViewModel ToViewModel(PublicHoliday entity)
        {
            var model = new PublicHolidayViewModel
            {
                Id = entity.Id,
                HolidayName = entity.HolidayName,
                Date = entity.Date,
            };
            return model;
        }
        public static List<PublicHolidayViewModel> ToViewModel(List<PublicHoliday> entities)
        {
            var models = new List<PublicHolidayViewModel>();
            foreach (var entity in entities)
            {
                models.Add(ToViewModel(entity));
            }
            return models;
        }

        public static PersonalDataEditDto ToEditDto(Employee entity)
        {
            var model = new PersonalDataEditDto
            {
                Id = entity.Id,
                ShiftScheduleId = entity.ShiftScheduleId,
                DepartmentId = entity.DepartmentId,
                Name = entity.Name,
                Email = entity.Email,

            };
            return model;
        }

        /// <summary>
        /// 給編輯頁面用的ToEditDto，傳入一個entity，回傳一個Dto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static VacationEditDto ToEditDto(VacationDetail entity)
        {
            var model = new VacationEditDto
            {
                Id = entity.Id,
                VacationType = entity.VacationType,
                Gender = entity.Gender,
                Cycle = entity.Cycle,
                NumberOfDays = entity.NumberOfDays,
                MinVacationHours = entity.MinVacationHours,


            };
            return model;
        }
        /// <summary>
        /// 給編輯畫面用的將ListEntity轉成ListEditDto
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<VacationEditDto> ToEditDto(List<VacationDetail> entities)
        {
            var models = new List<VacationEditDto>();     //先準備一個空的

            foreach (var entity in entities)                    //跑迴圈
            {
                models.Add(ToEditDto(entity));
            }

            //var items = entities.Select(o => ToViewModel(o)).ToList();      //LINQ寫法...

            return models;
        }

        /// <summary>
        /// 給查詢畫面用的entity轉Dto(List)
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<VacationDto> ToDto(List<VacationDetail> entities)
        {
            var models = new List<VacationDto>();     //先準備一個空的

            foreach (var entity in entities)                    //跑迴圈
            {
                models.Add(ToDto(entity));
            }

            //var items = entities.Select(o => ToViewModel(o)).ToList();      //LINQ寫法...

            return models;
        }
        /// <summary>
        /// 給查詢畫面用的ToDto用的，傳入VacationDetails假別entityModel後回傳VacationDto
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static VacationDto ToDto(VacationDetail entity)
        {
            var model = new VacationDto
            {
                Id = entity.Id,
                VacationType = entity.VacationType,
                Gender = entity.Gender.ToString(),
                Cycle = entity.Cycle.ToString(),
                NumberOfDays = entity.NumberOfDays,
                MinVacationHours = entity.MinVacationHours,

            };
            return model;
        }



        public static List<DepartmentViewModel> ToViewModel(List<Department> entities)
        {
            var models = new List<DepartmentViewModel>();
            foreach (var entity in entities)
            {
                models.Add(ToViewModel(entity));
            }

            return models;
        }

        public static DepartmentViewModel ToViewModel(Department entity)
        {
            var model = new DepartmentViewModel
            {
                Id = entity.Id,
                DepartmentName = entity.DepartmentName,
                EmployeesId = entity.EmployeesId,
                EmployeeName = entity.EmployeeName,
            };
            return model;
        }
        public static DepartmentEditViewModel ToEditViewModel(Department entity)
        {
            var model = new DepartmentEditViewModel
            {
                Id = entity.Id,
                DepartmentName = entity.DepartmentName,
                EmployeesId = entity.EmployeesId,
            };
            return model;
        }
        public static List<CompRequestViewModel> ToViewModel(List<AdditionalClockIn> entities)
        {
            var models = new List<CompRequestViewModel>();
            foreach (var entity in entities)
            {
                models.Add(ToViewModel(entity));
            }

            return models;
        }

        public static CompRequestViewModel ToViewModel(AdditionalClockIn entity)
        {
            var model = new CompRequestViewModel
            {
                Id = entity.Id,
                EmployeesId = entity.EmployeesId,
                AdditionalTime = entity.AdditionalTime,
                Status = entity.Status,
                Reason = entity.Reason,
                ApprovalEmployeeId = entity.ApprovalEmployeeId,
            };
            return model;
        }
    }
}