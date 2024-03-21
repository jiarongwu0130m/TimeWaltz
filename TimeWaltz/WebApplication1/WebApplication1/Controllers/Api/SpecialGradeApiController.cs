
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
    }
}
