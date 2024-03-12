using WebApplication1.Models.Entity;

namespace WebApplication1.Services
{
    public class OvertimeRequestService
    {
        private readonly TimeWaltzContext _timeWaltzDb;

        public OvertimeRequestService(TimeWaltzContext timeWaltzDb)
        {
            _timeWaltzDb = timeWaltzDb;
        }

        public OvertimeApplication CreateOvertimeRequest(OvertimeApplication entity)
        {
            _timeWaltzDb.OvertimeApplications.Add(entity);
            _timeWaltzDb.SaveChanges();
            return entity;
        }
    }
}
