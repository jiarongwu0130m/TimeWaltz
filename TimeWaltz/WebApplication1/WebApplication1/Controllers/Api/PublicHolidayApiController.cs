using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PublicHolidayApiController : ControllerBase
    {
        private readonly PublicHolidayService _publicHolidayService;

        public PublicHolidayApiController(PublicHolidayService publicHolidayService)
        {
            _publicHolidayService = publicHolidayService;
        }
        [HttpPost]
        public ActionResult Create(PublicHolidayCreateDto dto)
        {
            try
            {
                var entity = new PublicHoliday
                {
                    Date = dto.Date,
                    HolidayName = dto.HolidayName,
                };
                _publicHolidayService.CreatePublicHoliday(entity);
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }

        }
        [HttpGet]
        [Route("{Id}")]
        public PublicHolidayEditDto GetEditData(int Id)
        {
            var entity = _publicHolidayService.GetPublicHolidayOrNull(Id);
            var model = new PublicHolidayEditDto
            {
                Date = entity.Date,
                HolidayName = entity.HolidayName,
                Id = Id
            };
            return model;

        }
        [HttpPost]
        public ActionResult Edit(PublicHolidayEditDto dto)
        {
            try
            {
                _publicHolidayService.EditPublicHoliday(dto);
                return Ok(new { status = true });
            }
            catch (Exception ex) { 
                return Ok(new { status = false });
            }

        }
        [HttpGet]
        public List<PublicHolidayDto> GetListData()
        {
            var entities = _publicHolidayService.GetPublicHolidayList();
            var models = new List<PublicHolidayDto>();
            foreach (var entity in entities)
            {
                models.Add(new PublicHolidayDto
                {
                    Id = entity.Id,
                    Date = entity.Date.ToString("yyyy-MM-dd"),
                    HolidayName = entity.HolidayName,
                });
            }
            return models;
        }
        [HttpPost]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var entity = _publicHolidayService.GetPublicHolidayOrNull(id);
                _publicHolidayService.DeleteVacationType(entity);
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }

        }
    }
}
