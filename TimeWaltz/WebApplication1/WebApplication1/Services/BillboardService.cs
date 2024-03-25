using Microsoft.EntityFrameworkCore;
using Repository.Models;
using WebApplication1.Models.SettingViewModels;

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

        public bool EditBillboard(BillboardEditDto dto)
        {
            var entity = _timeWaltzDb.Billboards.FirstOrDefault(x => x.Id == dto.Id);

            entity.Title = dto.Title;
            entity.StartTime = dto.StartTime;
            entity.EndTime = dto.EndTime;
            entity.Content = dto.Content;

            _timeWaltzDb.SaveChanges();
            return true;
        }


        public List<Billboard> GetBillboardList()
        {
            return _timeWaltzDb.Billboards.Include(x => x.Employees).AsNoTracking().ToList();
        }

        public Billboard? GetBillboardTypeOrNull(int id)
        {
            return _timeWaltzDb.Billboards.FirstOrDefault(x => x.Id == id);
        }

    }
}
