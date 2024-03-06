using WebApplication1.Models.BasicSettingViewModels;
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

        public int EditSpecialHoliday(SpecialHolidayViewModel model)
        {
            var entity = GetSpecialHolidayOrNull();
            entity.HowToGive = model.HowToGive;
            entity.GiveDay = model.GiveDay;

            _timeWaltzContext.SaveChanges();
            return model.Id;
        }

        

        public SpecialHoliday GetSpecialHoliday()
        {
            var entity =  _timeWaltzContext.SpecialHoliday.FirstOrDefault();
            if (entity == null)
            {
                throw new Exception("請先去資料庫新建一筆資料!");
            }
            return entity;
        }

        public SpecialHoliday? GetSpecialHolidayOrNull()
        {
            return _timeWaltzContext.SpecialHoliday.FirstOrDefault();
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
