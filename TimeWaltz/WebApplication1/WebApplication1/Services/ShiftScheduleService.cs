using WebApplication1.Models.Entity;

namespace WebApplication1.Services
{
    public class ShiftScheduleService
    {
        private readonly TimeWaltzContext _timeWaltzContext;
        public ShiftScheduleService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }

        public List<ShiftSchedule> GetPersonalShiftScheduleList(int Id)
        {
            return _timeWaltzContext.ShiftSchedules.Where(s => s.Id == Id).ToList();            
        }
    }
}
