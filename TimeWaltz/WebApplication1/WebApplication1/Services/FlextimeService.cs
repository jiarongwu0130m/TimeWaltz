using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Models.Entity;

namespace WebApplication1.Services
{
    public class FlextimeService
    {
        private readonly TimeWaltzContext _timeWaltzDb;
        public FlextimeService(TimeWaltzContext timeWaltzDb)
        {
            _timeWaltzDb = timeWaltzDb;
        }        

        public Flextime GetFlextime()
        {
            return _timeWaltzDb.Flextimes.FirstOrDefault();
        }

        public FlextimeViewModel UpdateFlextime(FlextimeViewModel model)
        {
            var flextimeDb = _timeWaltzDb.Flextimes.FirstOrDefault(x=>x.Id == model.Id);
            flextimeDb.FlexibleTime = model.FlexibleTime;
                flextimeDb.MoveUp = model.MoveUp;
                _timeWaltzDb.SaveChanges();
            return model;
        }


    }
}
