using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public PublicHoliday GetPublicHolidayOrNull(int id)
        {
            return  _timeWaltzContext.PublicHolidays.FirstOrDefault(p => p.Id == id);
        }

        public List<PublicHoliday> GetPublicHolidayList()
        {
            return _timeWaltzContext.PublicHolidays.ToList();
        }
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

        }
    }
}
