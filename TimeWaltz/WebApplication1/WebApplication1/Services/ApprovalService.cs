using WebApplication1.Models.Entity;

namespace WebApplication1.Services
{
    public class ApprovalService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public ApprovalService(TimeWaltzContext timeWaltzContext)
        {
            this._timeWaltzContext = timeWaltzContext;
        }

        public void NewApproval(LeaveRequest entity)
        {
            _timeWaltzContext.Approvals.Add(new Approval
            {
                TableId = entity.Id,
                TableType = (Models.Enums.TableTypeEnum)1,
            });
            _timeWaltzContext.SaveChanges();

        }
    }
}
