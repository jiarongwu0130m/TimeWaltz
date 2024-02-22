using WebApplication1.Models;
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

        public List<ShiftSchedule> GetSelectedShiftScheduleList(ShiftSchedulesViewModel model)
        {
            //var dt = 資料庫裡的所有日期
            var dt1 = model.StartTime.Date;
            var dt2 = model.EndTime.Date;
           
            if(dt1 != dt2)
            {
                var entity = _timeWaltzContext.ShiftSchedules
                    .Where(s => s.StartTime.Date.CompareTo(dt1) >= 0 && s.StartTime.Date.CompareTo(dt2) <= 0);
                if(entity != null)
                {
                    return entity.ToList();
                }
                else
                {
                    throw new Exception("您所選擇的日期沒有資料");

                }

            }
            else
            {
                var entity = _timeWaltzContext.ShiftSchedules
                    .Where(s => s.StartTime.Date.CompareTo(dt1) == 0);
                if (entity != null)
                {
                    return entity.ToList();
                }
                else
                {
                    throw new Exception("您所選擇的日期沒有資料");
                }
            }

            
        }
       
    }
}
