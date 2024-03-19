using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Models.Entity;

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
                 TableType = (Models.Enums.TableTypeEnum)1,
                 TableId = entity.Id,
                 Status = (Models.Enums.RequestStatusEnum)1,
             });
            _timeWaltzContext.SaveChanges();
        }
    }
}
