
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebApplication1.Models.BasicSettingViewModels;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SpecialGradeApiController : ControllerBase
    {
        private readonly TimeWaltzContext _db;

        public SpecialGradeApiController(TimeWaltzContext db)
        {
            _db = db;
        }
        [HttpGet]
        public List<SpecialGradeDto> GetListData()
        {
            var entity =  _db.SpecialGrade.ToList();
            var models = new List<SpecialGradeDto>();
            foreach (var model in entity)
            {
                models.Add(new SpecialGradeDto
                {
                    Id = model.Id,
                    Days = model.Days,
                    ServiceLength = model.ServiceLength,
                });
            }
            return models;
        }

        [HttpPost]
        public ActionResult Create(SpecialGradeCreateDto dto)
        {
            try
            {
                var entity = new SpecialGrade
                {
                    Days = dto.Days,
                    ServiceLength = dto.ServiceLength,
                };
                _db.SpecialGrade.Add(entity);
                _db.SaveChanges();
                return Ok(new { status = true });
            }
            catch (Exception ex) 
            { 
                return Ok(new { status = false });
            }
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<SpecialGradeEditDto> GetEditData(int id)
        {
            try
            {
                var entity = _db.SpecialGrade.FirstOrDefault(x=>x.Id == id);
                var model = new SpecialGradeEditDto
                {
                    Id = entity.Id,
                    Days = entity.Days,
                    ServiceLength = entity.ServiceLength,
                };
                return model;
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }
        [HttpPost]
        public ActionResult Edit(SpecialGradeEditDto dto)
        {
            try
            {
                var entity = _db.SpecialGrade.FirstOrDefault(x=>x.Id == dto.Id);
                entity.ServiceLength = dto.ServiceLength;
                entity.Days = dto.Days;
                _db.SaveChanges();
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }
        [HttpPost]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var entity = _db.SpecialGrade.FirstOrDefault(x => x.Id == id);
                _db.SpecialGrade.Remove(entity);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
            
        }
    }
}
