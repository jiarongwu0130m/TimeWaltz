using Repository.Models;
using WebApplication1.Models.BasicSettingViewModels;

namespace WebApplication1.Services
{
    public class PersonalDataService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public PersonalDataService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }
        public void CreatePersonalData(Employee entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("新增錯誤");
            }
            entity.User.Stop= false;
            _timeWaltzContext.Employees.Add(entity);
            _timeWaltzContext.SaveChanges();


        }

        public void SoftDeletePersonalData(Employee entity)
        {
            entity.User.Stop = true;
            _timeWaltzContext.SaveChanges();
        }

        public List<ShiftSchedule> GetShiftNameDropDownData()
        {
            var entities = _timeWaltzContext.ShiftSchedules.Select(s => new ShiftSchedule
            {
                Id = s.Id,
                ShiftsName = s.ShiftsName,
            }).ToList();

            return entities;

        }
        /// <summary>
        /// 給GetPersonalData用，Join部門以及班別
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<PersonalDataDto> GetPersonalDataList()
        {

            return _timeWaltzContext.Employees
                .Where(x => x.User.Stop == false)
                .Select(e => new PersonalDataDto
                {
                    Id = e.Id,
                    ShiftsName = e.ShiftSchedule.ShiftsName,
                    DepartmentName = e.Department.DepartmentName,
                    Name = e.Name,
                    HireDate = e.HireDate,
                    Email = e.Email,
                    Gender = e.Gender.ToString(),
                    EmployeesNo = e.EmployeesNo,
                }).ToList();
        }



        public Employee? GetPersonalDataOrNull(int id)
        {
            return _timeWaltzContext.Employees.Where(x=>x.User.Stop == false).FirstOrDefault(x=>x.Id == id);
        }



        public void EditPersonalData(PersonalDataEditModel model)
        {
            var entity = _timeWaltzContext.Employees
                .Where(x => x.User.Stop == false)
                .FirstOrDefault(e => e.Id == model.Id);
            entity.DepartmentId = model.DepartmentId;
            entity.ShiftScheduleId = model.ShiftScheduleId;
            entity.Name = model.Name;
            entity.Email = model.Email;
            entity.Gender = model.Gender;
            entity.EmployeesNo = model.EmployeesNo;
            entity.HireDate = model.HireDate;
            _timeWaltzContext.SaveChanges();
        }

        public List<Department> GetDepartmentDropDownData()
        {
            var entities = _timeWaltzContext.Departments.Select(d => new Department
            {
                Id = d.Id,
                DepartmentName = d.DepartmentName,
            }).ToList();
            return entities;
        }


    }
}
