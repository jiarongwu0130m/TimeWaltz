using WebApplication1.Models.BasicSettingViewModels;
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

        public void CreateShiftSchedule(ShiftSchedule entity)
        {
            _timeWaltzContext.ShiftSchedules.Add(entity);
            _timeWaltzContext.SaveChanges();          
        }

        public void DeleteShiftSchedule(ShiftSchedule entity)
        {
            _timeWaltzContext.Remove(entity);
            _timeWaltzContext.SaveChanges(true);
        }

        public ShiftSchedule? GetShiftScheduleOrNull(int id)
        {
            return _timeWaltzContext.ShiftSchedules.FirstOrDefault(s => s.Id == id);
        }

        public void EditShiftSchedule(ShiftSchedulesEditViewModel model)
        {
            var entity = GetShiftScheduleOrNull(model.Id);

            entity.ShiftsName = model.ShiftName;
            entity.StartTime = model.StartTime;
            entity.EndTime = model.EndTime;
            entity.BreakTime = (int)Math.Round(model.BreakTimeHours * 60.0);
            entity.MaxAdditionalClockIn = model.MaxAdditionalClockIn;

            _timeWaltzContext.SaveChanges();
        }

        public List<ShiftSchedule> GetShiftScheduleList()
        {
            return _timeWaltzContext.ShiftSchedules.Select(s=>new ShiftSchedule
            {
                Id = s.Id,
                ShiftsName = s.ShiftsName,
                StartTime = s.StartTime, 
                EndTime = s.EndTime,
                BreakTime = s.BreakTime,
                MaxAdditionalClockIn = s.MaxAdditionalClockIn,
            }).ToList();
        }

        public void ClearAllFK(int id)
        {
            var emp = _timeWaltzContext.Employees.Where(x=>x.ShiftScheduleId == id).ToList();
            foreach(var e in emp)
            {
                e.ShiftScheduleId = null;
            }
            var shi = _timeWaltzContext.Shifts.Where(x => x.ShiftScheduleId == id).ToList();
            _timeWaltzContext.Shifts.RemoveRange(shi);
        }
    }
}
