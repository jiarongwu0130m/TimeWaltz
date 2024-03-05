﻿using System.Linq;
using WebApplication1.Models;
using WebApplication1.Models.Entity;

namespace WebApplication1.Services
{
    public class DepartmentService
    {
        private readonly TimeWaltzContext _timeWaltzDb;

        public DepartmentService(TimeWaltzContext timeWaltzDb)
        {
            _timeWaltzDb = timeWaltzDb;
        }

        public List<Department> GetDepartment()
        {
            
            var entities = _timeWaltzDb.Departments.ToList();
            foreach(var entity in entities)
            {
                var employee = _timeWaltzDb.Employees.FirstOrDefault(e=>e.Id == entity.EmployeesId);
                if (employee != null)
                {
                    entity.EmployeeName = employee.Name;
                }
            }
            return entities;                               
        }


        public void CreateDepartment(Department entity)
        {
            _timeWaltzDb.Departments.Add(entity);
            _timeWaltzDb.SaveChanges();

        }

        public Department? GetDepartmentOrNull(int id)
        {
            return _timeWaltzDb.Departments.FirstOrDefault(x => x.Id == id);

        }

        public List<Department> GetSelectedDepartment(DepartmentViewModel selectModel)
        {

            if (selectModel.QueryDepartment != null)
            {
                return _timeWaltzDb.Departments.Where(x=>x.DepartmentName.Contains(selectModel.QueryDepartment)).ToList();                    
            }
            else
            {
                return _timeWaltzDb.Departments.ToList();
            }
        }

        public int EditDepartment(DepartmentEditViewModel model)
        {
            var entity = _timeWaltzDb.Departments.FirstOrDefault(x => x.Id == model.Id);

            entity.DepartmentName = model.DepartmentName;
            entity.EmployeesId = model.EmployeesId;

            _timeWaltzDb.SaveChanges();
            return model.Id;
        }


        public int DeleteDepartment(Department entity)
        {
            if (entity != null)
            {
                _timeWaltzDb.Departments.Remove(entity);
                _timeWaltzDb.SaveChanges();
                return entity.Id;
            }
            return -1;
        }

        public List<Department>? GetDropDownData()
        {
            var entity =  _timeWaltzDb.Departments.ToList();
            if(entity != null)
            {
                return entity;
            }
            return null;
        }

        public List<Employee>? GetEmployeeDropDownData()
        {
            return _timeWaltzDb.Employees.ToList();
        }
    }
}
