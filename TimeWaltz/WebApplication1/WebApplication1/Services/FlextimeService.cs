using WebApplication1.Models;
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
        public FlextimeViewModel GetFlextimeViewModel()
        {
            var model = _timeWaltzDb.Flextimes.FirstOrDefault();

            var flextimeViewModel = new FlextimeViewModel
            {
                Id = model?.Id ?? 1,
                FlexibleTime = model?.FlexibleTime,
                MoveUp = model?.MoveUp ?? false,
            };

            return flextimeViewModel;
        }

        public void UpdateFlextime(FlextimeViewModel model)
        {
            if (model == null) return;

            var flextimeDb = _timeWaltzDb.Flextimes.FirstOrDefault();
            if (flextimeDb != null)
            {
                flextimeDb.FlexibleTime = model.FlexibleTime;
                flextimeDb.MoveUp = model.MoveUp;
                _timeWaltzDb.SaveChanges();
            }
        }


    }
}
