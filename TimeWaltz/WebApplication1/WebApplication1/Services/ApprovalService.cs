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

        public void NewApproval(int Id)
        {
            _timeWaltzContext.Approvals.Add(new Approval
            {
                TableId = Id,
                TableType = (int)TableTypeEnum.請假單,
                Status = Repository.Enum.RequestStatusEnum.簽核中,
            });
            _timeWaltzContext.SaveChanges();

        }
        public void NewApproval_補打卡(int Id)
        {
            _timeWaltzContext.Approvals.Add(new Approval
            {
                TableId = Id,
                TableType = (int)TableTypeEnum.補卡單,
                Status = Repository.Enum.RequestStatusEnum.簽核中,
            });
            _timeWaltzContext.SaveChanges();
        }
        
        public void NewApproval_加班單(int Id)
        {
            _timeWaltzContext.Approvals.Add(new Approval
            {
                TableId = Id,
                TableType = (int)TableTypeEnum.加班單,
                Status = Repository.Enum.RequestStatusEnum.簽核中,
            });
            _timeWaltzContext.SaveChanges();
        }
    }
}
