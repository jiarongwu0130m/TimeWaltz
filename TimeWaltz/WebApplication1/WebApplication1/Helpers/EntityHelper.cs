using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Models.Entity;
using WebApplication1.Models.Enums;

namespace WebApplication1.Helpers
{
    public class EntityHelper
    {
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

        public static ShiftSchedulesViewModel ToViewModel(ShiftSchedule entity)
        {
            var model = new ShiftSchedulesViewModel
            {
                Date = entity.StartTime.Date,
                DayOfWeek = entity.StartTime.DayOfWeek,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                BreakTime = entity.EndTime.CompareTo(entity.StartTime),

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
    }
}
