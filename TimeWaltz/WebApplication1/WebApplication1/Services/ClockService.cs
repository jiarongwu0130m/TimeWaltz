using Repository.Models;


namespace WebApplication1.Services
{
    public class ClockService
    {
        private readonly TimeWaltzContext _timeWaltzDb;

        public ClockService(TimeWaltzContext timeWaltzDb) 
        {
            _timeWaltzDb = timeWaltzDb;
        }
        public int ClockData(Clock clockDb)
        {
            _timeWaltzDb.Clocks.Add(clockDb);
            _timeWaltzDb.SaveChanges();
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
