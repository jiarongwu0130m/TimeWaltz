﻿using Repository.Models;
using WebApplication1.Models.BasicSettingViewModels;

namespace WebApplication1.Services
{
    public class GradeTableService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public GradeTableService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }
        public int CreateGradeTable(SpecialGrade entity)
        {            
            _timeWaltzContext.SpecialGrades.Add(entity);
            _timeWaltzContext.SaveChanges();

            return entity.Id;
        }

        public int DeleteGradeTable(SpecialGrade? entity)
        {
            _timeWaltzContext.Remove(entity);
            _timeWaltzContext.SaveChanges();
            return entity.Id;
        }

        public int EditGradeTable(SpecialGradeEditViewModel model)
        {
            var entity = _timeWaltzContext.SpecialGrades.FirstOrDefault(x => x.Id == model.Id);
            entity.ServiceLength = model.ServiceLength;
            entity.Days = model.Days;

            _timeWaltzContext.SaveChanges();
            return entity.Id;
        }

        

        public List<SpecialGrade> GetGradeTableList()
        {
            return _timeWaltzContext.SpecialGrades.ToList();
        }

        public SpecialGrade? GetGradeTableOrNull(int Id)
        {
            return _timeWaltzContext.SpecialGrades.FirstOrDefault(g => g.Id == Id);
        }
    }
}
