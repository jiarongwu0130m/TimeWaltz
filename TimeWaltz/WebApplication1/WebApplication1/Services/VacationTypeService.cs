using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Models;
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
        public VacationDetail? GetVacationTypeOrNull(int id)
        {
            return _timeWaltzContext.VacationDetails.FirstOrDefault(x => x.Id == id);            
           
        }

        public int EditVacationType(VacationTypeViewModel model)
        {
            var entity = _timeWaltzContext.VacationDetails.FirstOrDefault(x => x.Id == model.Id);

            entity.VacationType = model.VacationType;
            entity.Gender = model.Gender;
            entity.NumberOfDays = model.NumberOfDays;
            entity.Cycle = model.Cycle;
            entity.MinVacationHours = model.MinVacationHours;

            _timeWaltzContext.SaveChanges();
            return model.Id;
        }
    }
}
