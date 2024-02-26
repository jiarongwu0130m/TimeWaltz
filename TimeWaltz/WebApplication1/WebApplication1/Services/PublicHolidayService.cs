
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
        public int EditPublicHoliday(PublicHolidayViewModel model)
        {
            var entity = _timeWaltzContext.PublicHolidays.FirstOrDefault(x => x.Id == model.Id);

            entity.HolidayName = model.HolidayName;
            entity.Date = model.Date;
            _timeWaltzContext.SaveChanges();
            return entity.Id;
        }
        public int CreatePublicHoliday(PublicHoliday entity)
        {
            _timeWaltzContext.PublicHolidays.Add(entity);
            _timeWaltzContext.SaveChanges();
            return entity.Id;
        }

<<<<<<< HEAD
        public PublicHoliday GetPublicHolidayOrNull(int id)
        {
            return  _timeWaltzContext.PublicHolidays.FirstOrDefault(p => p.Id == id);
        }

=======
>>>>>>> 437d1afd73d4a94354d193e3e423e375de688b79
        public List<PublicHoliday> GetPublicHolidayList()
        {
            return _timeWaltzContext.PublicHolidays.ToList();
        }
<<<<<<< HEAD
        /// <summary>
        /// 找到查詢到的國定假日List
        /// </summary>
        /// <param name="selectedModel"></param>
        /// <returns></returns>
        public List<PublicHoliday> GetSelectedPublicHolidayList(PublicHolidayViewModel selectedModel)
        {
            var holidayName = selectedModel.HolidayName;
            var entities = _timeWaltzContext.PublicHolidays
                .Where(v => v.HolidayName == holidayName).ToList();
            return entities;

        }

        public PublicHoliday GetVacationTypeOrNull(int Id)
        {
            return _timeWaltzContext.PublicHolidays.FirstOrDefault(p => p.Id == Id);
        }

        public int DeleteVacationType(PublicHoliday entity)
        {
            if(entity != null)
            {
                _timeWaltzContext.Remove(entity);
                _timeWaltzContext.SaveChanges();                
            }
            return entity.Id;

=======

        public List<PublicHoliday> GetSelectedPublicHolidayList(EditPublicHolidayViewModel selectedModel)
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
>>>>>>> 437d1afd73d4a94354d193e3e423e375de688b79
        }
    }
}
