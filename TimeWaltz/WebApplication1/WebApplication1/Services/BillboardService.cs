using WebApplication1.Models.Entity;

namespace WebApplication1.Services
{
    public class BillboardService
    {
        private readonly TimeWaltzContext _timeWaltzDb;

        public BillboardService(TimeWaltzContext timeWaltzDb)
        {
            _timeWaltzDb = timeWaltzDb;
        }
        public Billboard CreateBillboard(Billboard entity)
        {
            _timeWaltzDb.Billboards.Add(entity);
            _timeWaltzDb.SaveChanges();
            return entity;
        }

    }
}
