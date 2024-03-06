
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Models.Entity;

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
        public object GetEmployeeAndShiftData()
        {
            throw new NotImplementedException();
        }
        public List<Employee> GetPersonalDataList()
        {
            var department = _timeWaltzContext.Departments
                .Select(d=>new Department { Id = d.Id, DepartmentName = d.DepartmentName})
                .ToList();
            var entities = _timeWaltzContext.Employees.ToList()
                .Join(department, e=>e.DepartmentId, d=> d.Id, (e, d)=> new Employee
                {
                    Id = e.Id,
                    ShiftScheduleId = e.ShiftScheduleId,
                    DepartmentId = e.DepartmentId,
                    Name = e.Name,
                    HireDate = e.HireDate,
                    Email = e.Email,
                    Gender = e.Gender,
                    EmployeesNo = e.EmployeesNo,
                    DepartmentName = d.DepartmentName,
                    ShiftsName = "",
                }).ToList();
            
            
            foreach( var entity in entities)
            {
                var shiftSchedule = _timeWaltzContext.ShiftSchedules.FirstOrDefault(s => s.Id == entity.ShiftScheduleId);
                if (shiftSchedule != null)
                {
                    entity.ShiftsName = shiftSchedule.ShiftsName;
                }
                else
                {
                    throw new Exception("資料庫錯誤");
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
