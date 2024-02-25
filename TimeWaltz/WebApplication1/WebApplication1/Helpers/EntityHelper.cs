using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Models.Entity;
using WebApplication1.Models.Enums;

namespace WebApplication1.Helpers
{
    public class EntityHelper
    {
        public static EditVacationTypeViewModel ToViewModel(VacationDetail entity)
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

       public static List<EditVacationTypeViewModel> ToViewModel(List<VacationDetail> entities)
        {
            var models = new List<EditVacationTypeViewModel>();     //先準備一個空的
            
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
