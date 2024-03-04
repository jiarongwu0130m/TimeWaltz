
using WebApplication1.Models.Entity;

namespace WebApplication1.Services
{
    public class LeaveService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public LeaveService(TimeWaltzContext timeWaltzContext)
        {
            this._timeWaltzContext = timeWaltzContext;
        }
        public List<AgentEmployee> GetAgentDropDownData(int UserId)
        {
            var employee = _timeWaltzContext.Employees.ToList();
            var agentEmployee = _timeWaltzContext.AgentEmployees
                .Where(a => a.EmployeesId == UserId).ToList();
            foreach(var a in agentEmployee)
            {
                employee.Where(e => e.Id == a.AgentEmployeesId).Select(e => new AgentEmployee
                {                    
                    EmployeesId = e.Id,
                    AgentEmployeeName = e.Name,
                });
            }
            return agentEmployee;
        }

        public List<VacationDetail> GetVacationDropDownData()
        {
            return _timeWaltzContext.VacationDetails.ToList();
        }

        internal string GetApprovalStatusOrDefault()
        {
            throw new NotImplementedException();
        }
    }
}
