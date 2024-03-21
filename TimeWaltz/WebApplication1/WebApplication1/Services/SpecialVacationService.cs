using System.Collections.Generic;
using Repository.Models;
using WebApplication1.Models.BasicSettingViewModels;

namespace WebApplication1.Services
{
    public class SpecialVacationService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public SpecialVacationService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }

        public void Cerate(SpecialVacation entity)
        {
            _timeWaltzContext.SpecialVacations.Add(entity);
            _timeWaltzContext.SaveChanges();
        }

        public SpecialVacation GetEditDataOrNull(int id)
        {
            return _timeWaltzContext.SpecialVacations.FirstOrDefault(x => x.Id == id);
        }

        public void Edit(SpecialVacationEditDto model)
        {
            var entity = GetEditDataOrNull(model.Id);
            if (entity == null) throw new ArgumentException("資料庫錯誤");
            entity.SpecialVacationName = model.SpecialVacationName;
            entity.Date = model.Date;
            _timeWaltzContext.SaveChanges();
        }

        public List<SpecialVacation> GetListData()
        {
            return _timeWaltzContext.SpecialVacations.ToList();
        }

        public void Delete(SpecialVacation entity)
        {
            _timeWaltzContext.Remove(entity);
            _timeWaltzContext.SaveChanges();
        }
    }
}
