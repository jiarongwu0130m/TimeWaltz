using Repository.Enums;
using Repository.Models;


namespace WebApplication1.Services
{
    public class ApprovalService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public ApprovalService(TimeWaltzContext timeWaltzContext)
        {
            this._timeWaltzContext = timeWaltzContext;
        }

        public void NewApproval(int empId)
        {
            _timeWaltzContext.Approvals.Add(new Approval
            {
                TableId = empId,
                TableType = (int)TableTypeEnum.請假單,
            });
            _timeWaltzContext.SaveChanges();

        }
    }
}
