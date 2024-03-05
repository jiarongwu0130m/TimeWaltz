using WebApplication1.Models.Dto;
using WebApplication1.Models.Entity;
using WebApplication1.Models.ViewModel;

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

        public void EditAllAgentEmployee(AgentEmployeeDto dto)
        {
            var emp = _timeWaltzContext.AgentEmployees.Where(a => a.EmployeesId == 1);
            if(emp != null)
            {
                _timeWaltzContext.AgentEmployees.RemoveRange(emp);  
                if (!string.IsNullOrEmpty(dto.Agent1))
                {
                    _timeWaltzContext.AgentEmployees.Add(new AgentEmployee
                    {
                        EmployeesId = 1,
                        AgentEmployeesId = int.Parse(dto.Agent1),
                    });
                }
                if (!string.IsNullOrEmpty(dto.Agent2))
                {
                    _timeWaltzContext.AgentEmployees.Add(new AgentEmployee
                    {
                        EmployeesId = 1,
                        AgentEmployeesId = int.Parse(dto.Agent2),
                    });
                }
                if (!string.IsNullOrEmpty(dto.Agent3))
                {
                    _timeWaltzContext.AgentEmployees.Add(new AgentEmployee
                    {
                        EmployeesId = 1,
                        AgentEmployeesId = int.Parse(dto.Agent3),
                    });
                }
                _timeWaltzContext.SaveChanges();
            }
            
        }

        public List<int> GetAgentemployee(int UserId)
        {
            return _timeWaltzContext.AgentEmployees.Where(a=> a.EmployeesId == UserId).Select(x=>x.AgentEmployeesId).ToList();
        }
    }
}
