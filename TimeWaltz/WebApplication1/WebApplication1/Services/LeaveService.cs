
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

        public List<VacationDetail> GetVacationDropDownData()
        {
            return _timeWaltzContext.VacationDetails.ToList();
        }

        internal string GetApprovalStatusOrDefault()
        {
            throw new NotImplementedException();
        }

        public string GetNameOrNull(int userId)
        {
            return _timeWaltzContext.Employees.FirstOrDefault(x => x.Id == userId).Name;
        }
    }
}
