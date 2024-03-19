using Repository.Models;

namespace WebApplication1.Services
{
    public class DropDownBasicSettingService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public DropDownBasicSettingService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }

        public List<Department>? GetDropDownData()
        {
            var entity = _timeWaltzContext.Departments.ToList();
            if (entity != null)
            {
                return entity;
            }
            return null;
        }

        public List<Employee>? GetEmployeeDropDownData()
        {
            var  entity =_timeWaltzContext.Employees.ToList();
            if (entity != null)
            {
                return entity;
            }
            return null;
        }
    }
}
