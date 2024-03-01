using System.Linq;
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
            return _timeWaltzDb.Departments.ToList();
        }


        public Department CreateDepartment(Department entity)
        {
            _timeWaltzDb.Departments.Add(entity);
            _timeWaltzDb.SaveChanges();
            return entity;
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


    }
}
