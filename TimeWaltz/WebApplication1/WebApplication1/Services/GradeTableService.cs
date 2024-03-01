using WebApplication1.Models;
using WebApplication1.Models.Entity;

namespace WebApplication1.Services
{
    public class GradeTableService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public GradeTableService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }
        public int CreateGradeTable(GradeTable entity)
        {            
            _timeWaltzContext.GradeTable.Add(entity);
            _timeWaltzContext.SaveChanges();

            return entity.Id;
        }

        public int DeleteGradeTable(GradeTable? entity)
        {
            _timeWaltzContext.Remove(entity);
            _timeWaltzContext.SaveChanges();
            return entity.Id;
        }

        public int EditGradeTable(EditGradeTableViewModel model)
        {
            var entity = _timeWaltzContext.GradeTable.FirstOrDefault(x => x.Id == model.Id);
            entity.ServiceLength = model.ServiceLength;
            entity.Days = model.Days;

            _timeWaltzContext.SaveChanges();
            return entity.Id;
        }

        

        public List<GradeTable> GetGradeTableList()
        {
            return _timeWaltzContext.GradeTable.ToList();
        }

        public GradeTable? GetGradeTableOrNull(int Id)
        {
            return _timeWaltzContext.GradeTable.FirstOrDefault(g => g.Id == Id);
        }
    }
}
