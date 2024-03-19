using Repository.Models;
using WebApplication1.Models.BasicSettingViewModels;


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
            _timeWaltzContext.SpecialHolidays.Add(entity);
            _timeWaltzContext.SaveChanges();
            return entity.Id;
        }

        public int EditSpecialHoliday(SpecialHolidayViewModel model)
        {
            var entity = GetSpecialHolidayOrNull();
            //entity.HowToGive = model.HowToGive;
            //entity.GiveDay = model.GiveDay;//todo

            _timeWaltzContext.SaveChanges();
            return model.Id;
        }

        

        public SpecialHoliday GetSpecialHoliday()
        {
            var entity =  _timeWaltzContext.SpecialHolidays.FirstOrDefault();
            if (entity == null)
            {
                throw new Exception("請先去資料庫新建一筆資料!");
            }
            return entity;
        }

        public SpecialHoliday? GetSpecialHolidayOrNull()
        {
            return _timeWaltzContext.SpecialHolidays.FirstOrDefault();
        }

        public int DeleteSpecialHoliday(SpecialHoliday entity)
        {
            if(entity != null)
            {
                _timeWaltzContext.SpecialHolidays.Remove(entity);
                _timeWaltzContext.SaveChanges() ;
                return entity.Id;
            }
            return -1;
        }
    }
}
