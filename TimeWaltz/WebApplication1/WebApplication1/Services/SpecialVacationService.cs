using WebApplication1.Models.Entity;

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
    }
}
