using Repository.Models;
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
