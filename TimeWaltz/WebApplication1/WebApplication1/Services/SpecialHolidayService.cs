
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

        public int EditSpecialHoliday(EditSpecialHolidayViewModel model)
        {
            var entity = GetSpecialHolidayOrNull(model.Id);
            entity.HowToGive = model.HowToGive;
            entity.GiveDay = model.GiveDay;

            _timeWaltzContext.SaveChanges();
            return model.Id;
        }

        

        public List<SpecialHoliday> GetSpecialHolidayList()
        {
            return _timeWaltzContext.SpecialHoliday.ToList();
        }

        public SpecialHoliday? GetSpecialHolidayOrNull(int id)
        {
            return _timeWaltzContext.SpecialHoliday.FirstOrDefault(x => x.Id == id);
        }

        public int DeleteSpecialHoliday(SpecialHoliday entity)
        {
            if(entity != null)
            {
                _timeWaltzContext.SpecialHoliday.Remove(entity);
                _timeWaltzContext.SaveChanges() ;
                return entity.Id;
            }
            return -1;
        }
    }
}
