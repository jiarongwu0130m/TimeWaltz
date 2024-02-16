using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Models.Entity;
using WebApplication1.Models.Enums;
using WebApplication1.Services;

namespace WebApplication1.Helpers
{
    public class EntityHelper
    {
        public static VacationTypeViewModel ToViewModel(VacationDetail entity)
        {
            var model = new VacationTypeViewModel
            {
                Id = entity.Id,
                VacationType = entity.VacationType,
                CycleSelectItems = Enum.GetValues(typeof(CycleEnum)).Cast<CycleEnum>().Select(c => new SelectListItem
                {
                    Text = c.ToString(),
                    Value = ((int)c).ToString()
                }).ToList(),
                NumberOfDays = entity.NumberOfDays,
                MinVacationHours = entity.MinVacationHours
            };
            return model;
        }
    }
}
