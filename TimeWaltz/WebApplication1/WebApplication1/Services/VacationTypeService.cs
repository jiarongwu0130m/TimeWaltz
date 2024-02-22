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
        public int CreateVacationType(VacationDetail entity)
        {
            _timeWaltzContext.VacationDetails.Add(entity);
            _timeWaltzContext.SaveChanges();
            return entity.Id;
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

        public List<VacationDetail> GetVacationDetailsList()
        {
            return _timeWaltzContext.VacationDetails.ToList();            
        }
        
        public int DeleteVacationType(VacationDetail entity)
        {
            if(entity !=  null)
            {
                _timeWaltzContext.VacationDetails.Remove(entity);
                _timeWaltzContext.SaveChanges();
            }
            return entity.Id;
        }

        internal List<VacationDetail> GetSelectedShiftScheduleList(VacationTypeViewModel selectedModel)
        {            
            var typeName = selectedModel.VacationType;
            if(typeName != null)
            {
                var entities = _timeWaltzContext.VacationDetails
                .Where(v => v.VacationType == typeName).ToList();
                return entities;
            }
            else
            {
                return null;
            }
            
        }
    }
}
