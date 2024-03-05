using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebApplication1.Models;
using WebApplication1.Models.Entity;
using WebApplication1.Models.Enums;

namespace WebApplication1.Helpers
{
    public class EntityHelper
    {
        private readonly TimeWaltzContext _dbContext;

        public EntityHelper(TimeWaltzContext timeWaltzContext)
        {
            _dbContext = timeWaltzContext;
        }
        

        public static List<PersonalDataViewModel> ToViewModel(List<Employee> entities)
        {
            var models = new List<PersonalDataViewModel>();
            foreach(var entity in entities)
            {
                models.Add(ToViewModel(entity));
            }

            return models;
        }

        public static PersonalDataViewModel ToViewModel(Employee entity)
        {
            

            var model = new PersonalDataViewModel
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
        public static EditPersonalDataViewModel ToEditViewModel(Employee entity)
        {
            
            var model = new EditPersonalDataViewModel
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
        public static EditGradeTableViewModel ToEditViewModel(GradeTable entity)
        {
            var model = new EditGradeTableViewModel
            {
                Id = entity.Id,
                ServiceLength = entity.ServiceLength,
                Days = entity.Days,
            };
            return model;
        }
        public static List<GradeTableViewModel> ToViewModel(List<GradeTable> entities)
        {
            var models = new List<GradeTableViewModel>();
            foreach(var entity in entities)
            {
                models.Add(ToViewModel(entity));
            }
            return models;
        }
        public static GradeTableViewModel ToViewModel(GradeTable entity)
        {
            var model = new GradeTableViewModel
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
        public static EditShiftSchedulesViewModel ToEditViewModel(ShiftSchedule entity)
        {
            var model = new EditShiftSchedulesViewModel
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
        public static EditPublicHolidayViewModel ToEditViewModel(PublicHoliday entity)
        {
            var model = new EditPublicHolidayViewModel
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
            foreach(var entity in entities)
            {
                models.Add(ToViewModel(entity));
            }
            return models;
        }
        public static VacationTypeViewModel ToViewModel(VacationDetail entity)
        {
            var model = new VacationTypeViewModel
            {
                Id = entity.Id,
                VacationType = entity.VacationType,
                Gender = entity.Gender,           
                Cycle = entity.Cycle,
                NumberOfDays = entity.NumberOfDays,
                MinVacationHours = entity.MinVacationHours,
                //
                GenderSelectItems = Enum.GetValues(typeof(GenderEnum)).Cast<GenderEnum>().Select(c => new SelectListItem
                {
                    Text = c.ToString(),
                    Value = c.ToString()
                }).ToList(),
                CycleSelectItems = Enum.GetValues(typeof(CycleEnum)).Cast<CycleEnum>().Select(c => new SelectListItem
                {
                    Text = c.ToString(),
                    Value = ((int)c).ToString()
                }).ToList()
            };
            return model;
        }
        public static EditVacationTypeViewModel ToEditViewModel(VacationDetail entity)
        {
            var model = new EditVacationTypeViewModel
            {
                Id = entity.Id,
                VacationType = entity.VacationType,
                Gender = entity.Gender,
                Cycle = entity.Cycle,
                NumberOfDays = entity.NumberOfDays,
                MinVacationHours = entity.MinVacationHours,
                //
                GenderSelectItems = Enum.GetValues(typeof(GenderEnum)).Cast<GenderEnum>().Select(c => new SelectListItem
                {
                    Text = c.ToString(),
                    Value = c.ToString()
                }).ToList(),
                CycleSelectItems = Enum.GetValues(typeof(CycleEnum)).Cast<CycleEnum>().Select(c => new SelectListItem
                {
                    Text = c.ToString(),
                    Value = ((int)c).ToString()
                }).ToList()
            };
            return model;
        }
        

        public static List<VacationTypeViewModel> ToViewModel(List<VacationDetail> entities)
        {
            var models = new List<VacationTypeViewModel>();     //先準備一個空的
            
            foreach (var entity in entities)                    //跑迴圈
            {
                models.Add(ToViewModel(entity));
            }
            
            //var items = entities.Select(o => ToViewModel(o)).ToList();      //LINQ寫法...

            return models;
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
                EmployeeName = entity. EmployeeName,
            };
            return model;
        }
        public static DepartmentEditViewModel ToEditViewModel(Department entity)
        {
            var model = new DepartmentEditViewModel
            {
                models.Add(ToViewModel(entity));
            }
            
            return models;
       }


        public static AccountViewModel ToViewModel(User entity)
        {
            var model = new AccountViewModel
            {
                Id = entity.Id,
                Account = entity.Account,
                EmployeesID = entity.EmployeesId,
                DepartmentID = entity.DepartmentId,
                Stop = entity.Stop,
            };
            return model;
        }
        public static List<AccountViewModel> ToViewModel(List<User> entities)
        {
            var models = new List<AccountViewModel>();
            foreach (var entity in entities)
            {
                models.Add(ToViewModel(entity));
            }

            return models;
        }
                Id = entity.Id,
                DepartmentName = entity.DepartmentName,
                EmployeesId = entity.EmployeesId,
            };
            return model;
        }
    }
}
