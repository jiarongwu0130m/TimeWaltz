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
            //entity.IsDelete = false;
            _timeWaltzContext.Employees.Add(entity);
            _timeWaltzContext.SaveChanges();


        }

        public void SoftDeletePersonalData(Employee entity)
        {
            //entity.IsDelete = true;
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
        public List<Employee> GetPersonalDataList()
        {
            
            //var entities = _timeWaltzContext.Employees
                //.Where(x=>x.IsDelete == false)
                //.Join(_timeWaltzContext.Departments, e => e.DepartmentId, d => d.Id, (e, d) => new Employee
                //{
                //    Id = e.Id,
                //    ShiftScheduleId = e.ShiftScheduleId,
                //    DepartmentId = e.DepartmentId,
                //    Name = e.Name,
                //    HireDate = e.HireDate,
                //    Email = e.Email,
                //    Gender = e.Gender,
                //    EmployeesNo = e.EmployeesNo,
                //    DepartmentName = d.DepartmentName,
                //    ShiftsName = "",
                //}).ToList();


            //foreach (var entity in entities)
            //{
            //    //TODO: 在這種假如資料正常情況下(有串好)不可能是null的時候要寫log或直接丟錯(因為也許空空的到前面也不能用)
            //    var department = _timeWaltzContext.Departments.FirstOrDefault(d => d.Id == entity.DepartmentId);
            //    if(department != null)
            //    {
            //        //entity.DepartmentName = department.DepartmentName;//todo
            //    }
            //    var shiftSchedule = _timeWaltzContext.ShiftSchedules.FirstOrDefault(s => s.Id == entity.ShiftScheduleId);
            //    if (shiftSchedule != null)
            //    {
            //        //entity.ShiftsName = shiftSchedule.ShiftsName;//todo
            //    }
            //    else
            //    {
            //        entity.ShiftsName = "此班別已被刪除，請重新設定或洽管理員";
            //    }

            //}

            //return entities;
            return new List<Employee>();
        }


        public Employee? GetPersonalDataOrNull(int id)
        {

            //return _timeWaltzContext.Employees.Where(x=>x.IsDelete == false).FirstOrDefault(x=>x.Id == id);
            return new Employee();
        }



        public void EditPersonalData(PersonalDataEditDto model)
        {
            //var entity = _timeWaltzContext.Employees.Where(x => x.IsDelete == false).FirstOrDefault(e => e.Id == model.Id);
            //entity.DepartmentId = model.DepartmentId;
            //entity.ShiftScheduleId = model.ShiftScheduleId;
            //entity.Name = model.Name;
            //entity.Email = model.Email;
            //_timeWaltzContext.SaveChanges();

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
