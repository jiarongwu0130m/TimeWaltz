using Repository.Models;


namespace WebApplication1.Services
{
    public class ClockService
    {
        private readonly TimeWaltzContext _timeWaltzDb;
        private readonly ApprovalService _approvalService;

        public ClockService(TimeWaltzContext timeWaltzDb, ApprovalService approvalService) 
        {
            _timeWaltzDb = timeWaltzDb;
            _approvalService = approvalService;
        }
        public int ClockData(Clock clockDb)
        {
            _timeWaltzDb.Clocks.Add(clockDb);
            _timeWaltzDb.SaveChanges();
            _approvalService.NewApproval_補打卡(clockDb.Id);

            return clockDb.EmployeesId;
        }

        //public  GetToDayClockRecord(DateTime toDay,ClockViewModel model)
        //{
        //    var clockRecord = _timeWaltzDb.Clocks.FirstOrDefault(clockRes => clockRes.Date.Date == toDay.Date);
            
        //    if(clockRecord != null)
        //    {
        //        return clockRecord;  
        //    }
        //    return null;
        //}
    }
}
