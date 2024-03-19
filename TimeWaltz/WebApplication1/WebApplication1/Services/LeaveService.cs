
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace WebApplication1.Services
{
    public class LeaveService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public LeaveService(TimeWaltzContext timeWaltzContext)
        {
            this._timeWaltzContext = timeWaltzContext;
        }
        public List<Employee> GetAgentDropDownData(int userId)
        {
            var employee = _timeWaltzContext.Employees.ToList();
            var agentEmployee = _timeWaltzContext.Employees.Include(x=>x.AgentEmployees).FirstOrDefault(x => x.Id == userId).AgentEmployees;
            return agentEmployee.ToList();
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
