using WebApplication1.Models;
using WebApplication1.Models.Entity;

namespace WebApplication1.Helpers
{
    public class ViewModelHelper
    {
        public static Employee ToEntity(CreatePersonalDataViewModel model)
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
        public static GradeTable ToEntity(CreateGradeTableViewModel model)
        {
            var entity = new GradeTable
            {
                ServiceLength = model.ServiceLength,
                Days = model.Days,
            };
            return entity;
        }

        public static ShiftSchedule ToEntity(CreateShiftSchedulesViewModel model)
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
       
        public static PublicHoliday ToEntity(CreatePublicHolidayViewModel model)
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
