using WebApplication1.Models;
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

        public List<PublicHoliday> GetPublicHolidayList()
        {
            return _timeWaltzContext.PublicHolidays.ToList();
        }

        public List<PublicHoliday> GetSelectedPublicHolidayList(PublicHolidayViewModel selectedModel)
        {
            if(selectedModel.QueryHolidayName != null)
            {
                return _timeWaltzContext.PublicHolidays.Where(p => p.HolidayName.Contains(selectedModel.QueryHolidayName)).ToList();
            }
            return _timeWaltzContext.PublicHolidays.ToList();
        }

        public PublicHoliday? GetVacationTypeOrNull(int id)
        {
            return _timeWaltzContext.PublicHolidays.FirstOrDefault(p => p.Id == id);
        }

        public int EditPublicHoliday(EditPublicHolidayViewModel model)
        {
            var entity = _timeWaltzContext.PublicHolidays.FirstOrDefault(p=>p.Id == model.Id);

            entity.HolidayName = model.HolidayName;
            entity.Date = model.Date;
            _timeWaltzContext.SaveChanges();
            return entity.Id;
        }
        public PublicHoliday? GetPublicHolidayOrNull(int id)
        {
            return _timeWaltzContext.PublicHolidays.FirstOrDefault(x => x.Id == id);

        }

        public int DeleteVacationType(PublicHoliday? entity)
        {
            if(entity != null)
            {
                _timeWaltzContext.PublicHolidays.Remove(entity);
                _timeWaltzContext.SaveChanges();

                return entity.Id;
            }
            return -1;
        }
    }
}
