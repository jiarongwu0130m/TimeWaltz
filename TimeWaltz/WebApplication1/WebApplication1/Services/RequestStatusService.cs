using Repository.Enums;
using Repository.Models;


namespace WebApplication1.Services
{
    public class RequestStatusService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public RequestStatusService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }



        public void NewRequestStatus(LeaveRequest entity)
        {
             _timeWaltzContext.Add(new RequestStatus
            {
                 TableType = TableTypeEnum.請假單,
                 TableId = entity.Id,
                 Status = (int)RequestStatusEnum.簽核中,
             });
            _timeWaltzContext.SaveChanges();
        }
    }
}
