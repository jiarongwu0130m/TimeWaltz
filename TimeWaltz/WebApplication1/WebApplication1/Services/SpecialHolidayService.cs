
using WebApplication1.Models;
using WebApplication1.Models.Entity;

namespace WebApplication1.Services
{
    public class SpecialHolidayService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public SpecialHolidayService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }


        public int CreatePublicHoliday(SpecialHoliday entity)
        {
            _timeWaltzContext.SpecialHoliday.Add(entity);
            _timeWaltzContext.SaveChanges();
            return entity.Id;
        }

        internal void EditSpecialHoliday(EditSpecialHolidayViewModel model)
        {
            throw new NotImplementedException();
        }

        internal PublicHoliday GetSelectedSpecialHolidayList(SpecialHolidayViewModel selectedModel)
        {
            throw new NotImplementedException();
        }

        internal PublicHoliday GetSpecialHolidayList()
        {
            throw new NotImplementedException();
        }

        internal PublicHoliday GetSpecialHolidayOrNull(int id)
        {
            throw new NotImplementedException();
        }
    }
}
