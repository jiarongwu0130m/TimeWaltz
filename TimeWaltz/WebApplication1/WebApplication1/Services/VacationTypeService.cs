using WebApplication1.Models.Entity;

namespace WebApplication1.Services
{
    public class VacationTypeService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public VacationTypeService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }
        public int CreateVacationType(VacationDetail vacationDetail)
        {
            _timeWaltzContext.VacationDetails.Add(vacationDetail);
            _timeWaltzContext.SaveChanges();
            return vacationDetail.Id;
        }
    }
}
