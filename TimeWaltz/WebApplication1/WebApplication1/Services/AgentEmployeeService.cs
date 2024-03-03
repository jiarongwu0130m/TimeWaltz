using WebApplication1.Models;
using WebApplication1.Models.Entity;

namespace WebApplication1.Services
{
    public class AgentEmployeeService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public AgentEmployeeService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }

        public List<Employee> GetAgentDropDownData(int UserId)
        {
            var User = _timeWaltzContext.Employees.FirstOrDefault(e=>e.Id == UserId);
            if(User != null)
            {
                return _timeWaltzContext.Employees.Where(e => e.DepartmentId == User.DepartmentId).ToList();                
            }
            throw new Exception("程式錯誤");                       
        }

        public void EditAgentEmployee(AgentEmploeeViewModel model)
        {
            var entity = _timeWaltzContext.AgentEmployees.FirstOrDefault(a => a.Id == model.Id);
            if(entity == null)
            {
                throw new Exception("資料庫錯誤");
            }
            entity.AgentEmployeesId = model.AgentEmployeesId;
        }
    }
}
