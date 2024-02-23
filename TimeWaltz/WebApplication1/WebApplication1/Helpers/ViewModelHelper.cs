﻿using WebApplication1.Models;
using WebApplication1.Models.Entity;

namespace WebApplication1.Helpers
{
    public class ViewModelHelper
    {
        public static VacationDetail ToEntity(CreateVacationTypeViewModel model)
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
        public static VacationDetail ToEntity(EditVacationTypeViewModel model)
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
        public static PublicHoliday ToEntity(PublicHolidayViewModel model)
        {
            var entity = new PublicHoliday
            {
                Id = model.Id,
                HolidayName = model.HolidayName,
                Date = model.Date
            };
            return entity;
        }


    }
}
