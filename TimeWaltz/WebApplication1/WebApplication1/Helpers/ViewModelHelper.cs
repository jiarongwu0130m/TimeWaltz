using WebApplication1.Models;
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
        //public static ShiftSchedule ToEntity(ShiftSchedulesViewModel model)
        //{
        //    var entity = new ShiftSchedule
        //    {

        //    };
        //}


        public static User ToEntity(AccountViewModel model)
        {

            var entity = new User
            {
                Account = model.Account,
                //Password = model.Password,
                EmployeesId = model.EmployeesID.Value,
                DepartmentId = model.DepartmentID.Value,
                Stop = model.Stop,
                PasswordDate = DateTime.Now
            };
            return entity;
        }
    }
}
