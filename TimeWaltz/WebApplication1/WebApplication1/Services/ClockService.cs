using WebApplication1.Models;
using WebApplication1.Models.Entity;

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
    }
}
