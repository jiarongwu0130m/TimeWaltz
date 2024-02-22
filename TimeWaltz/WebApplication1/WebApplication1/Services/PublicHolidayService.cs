using WebApplication1.Models.Entity;

namespace WebApplication1.Services
{
    public class PublicHolidayService
    {
        private TimeWaltzContext _timeWaltzContext;
        public PublicHolidayService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }

        internal int CreatePublicHoliday(PublicHoliday entity)
        {
            _timeWaltzContext.PublicHolidays.Add(entity);
            _timeWaltzContext.SaveChanges();
            return entity.Id;
        }
    }
}
