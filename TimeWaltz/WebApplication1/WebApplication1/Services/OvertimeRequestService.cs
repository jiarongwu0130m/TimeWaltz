using Repository.Models;
namespace WebApplication1.Services
{
    public class OvertimeRequestService
    {
        private readonly TimeWaltzContext _timeWaltzDb;
        private readonly ApprovalService _approvalService;

        public OvertimeRequestService(TimeWaltzContext timeWaltzDb,ApprovalService approvalService)
        {
            _timeWaltzDb = timeWaltzDb;
            _approvalService = approvalService;
        }

        public OvertimeApplication CreateOvertimeRequest(OvertimeApplication entity)
        {
            _timeWaltzDb.OvertimeApplications.Add(entity);
            _timeWaltzDb.SaveChanges();

            _approvalService.NewApproval_加班單(entity.Id);
            return entity;
        }

        public List<OvertimeApplication> GetOvertimeList()
        {
            return _timeWaltzDb.OvertimeApplications.ToList();
        }

        public OvertimeApplication? GetOvertimeRequestTypeOrNull(int id)
        {
            return _timeWaltzDb.OvertimeApplications.FirstOrDefault(x => x.Id == id);

        }


        public Employee? GetNameOrNull(int id)
        {
            return _timeWaltzDb.Employees.FirstOrDefault(x => x.Id == id);

        }
    }
}
