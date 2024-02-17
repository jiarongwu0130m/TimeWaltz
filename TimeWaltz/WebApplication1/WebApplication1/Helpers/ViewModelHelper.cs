﻿using WebApplication1.Models;
using WebApplication1.Models.Entity;

namespace WebApplication1.Helpers
{
    public class ViewModelHelper
    {
        public static VacationDetail ToEntity(VacationTypeViewModel model)
        {            

            var entity = new VacationDetail
            {
                Id = model.Id,
                VacationType = model.VacationType,
                Gender = model.Gender,
                NumberOfDays = model.NumberOfDays,
                Cycle = model.Cycle,
                MinVacationHours = model.MinVacationHours
            };
            return entity;
        }

        
    }
}