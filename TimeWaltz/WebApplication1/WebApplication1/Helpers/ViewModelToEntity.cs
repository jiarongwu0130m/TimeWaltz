using WebApplication1.Models;
using WebApplication1.Models.Entity;

namespace WebApplication1.Helpers
{
    public class ViewModelToEntity
    {
        private readonly TimeWaltzContext _context;
        public ViewModelToEntity(TimeWaltzContext context)
        {
            _context = context;
        }
        public static VacationDetail ConvertToEntity(VacationTypeViewModel model)
        {
            
            var entity = new VacationDetail
            {
                VacationType = model.VacationType,
                Gender = model.Gender,
                NumberOfDays = model.NumberOfDays,
                Cycle = model.Cycle,
                MinVacationHours = model.MinVacationHours
            };
            return entity;
        }
    }
}
