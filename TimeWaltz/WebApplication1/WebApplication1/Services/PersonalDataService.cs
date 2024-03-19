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
            if(entity == null)
            {
                throw new ArgumentNullException("新增錯誤");
            }
            _timeWaltzContext.Employees.Add(entity);
            _timeWaltzContext.SaveChanges();

            
        }

        public void DeletePersonalData(Employee entity)
        {
            _timeWaltzContext.Employees.Remove(entity);
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

        public List<Employee> GetPersonalDataList()
        {
            var entities =  _timeWaltzContext.Employees.ToList();

            foreach (var entity in entities)
            {
                //TODO: 在這種假如資料正常情況下(有串好)不可能是null的時候要寫log或直接丟錯(因為也許空空的到前面也不能用)
                var department = _timeWaltzContext.Departments.FirstOrDefault(d => d.Id == entity.DepartmentId);
                if(department != null)
                {
                    //entity.DepartmentName = department.DepartmentName;//todo
                }
                var shiftSchedule = _timeWaltzContext.ShiftSchedules.FirstOrDefault(s => s.Id == entity.ShiftScheduleId);
                if(shiftSchedule != null)
                {
                    //entity.ShiftsName = shiftSchedule.ShiftsName;//todo
                }
            }
            
            return entities;
        }

        public Employee? GetPersonalDataOrNull(int id)
        {
            return _timeWaltzContext.Employees.Where(e => e.Id == id).FirstOrDefault();
        }

        

        public void EditPersonalData(PersonalDataEditViewModel model)
        {
            var entity = _timeWaltzContext.Employees.FirstOrDefault(e=>e.Id == model.Id);
            entity.DepartmentId = model.DepartmentId;
            entity.ShiftScheduleId = model.ShiftScheduleId;
            entity.Name = model.Name;
            entity.Email = model.Email;
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
