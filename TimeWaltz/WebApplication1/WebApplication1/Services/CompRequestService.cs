using Repository.Models;
using WebApplication1.Models.ApplicationFormViewModels;

namespace WebApplication1.Services
{
    public class CompRequestService
    {
        private readonly TimeWaltzContext _timeWaltzDb;

        public CompRequestService(TimeWaltzContext timeWaltzDb)
        {
            _timeWaltzDb = timeWaltzDb;
        }        

        public AdditionalClockIn CreateCompRequest(AdditionalClockIn entity)
        {
            _timeWaltzDb.AdditionalClockIns.Add(entity);
            _timeWaltzDb.SaveChanges();
            return entity;
        }

        public List<AdditionalClockIn> GetCompRequest()
        {
            return _timeWaltzDb.AdditionalClockIns.ToList();
        }

        public List<AdditionalClockIn> GetSelectedCompRequest(CompRequestViewModel selectModel)
        {
            if (selectModel.QueryCompRequest != null)
            {
                return _timeWaltzDb.AdditionalClockIns.Where(x => x.AdditionalTime >= selectModel.QueryCompRequest).ToList();
            }
            else
            {
                return _timeWaltzDb.AdditionalClockIns.ToList();
            }
        }
        public AdditionalClockIn? GetCompRequestTypeOrNull(int id)
        {
            return _timeWaltzDb.AdditionalClockIns.FirstOrDefault(x => x.Id == id);
        }
    }
}
