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
        public ActionResult Create(PublicHolidayDto dto)
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
    }
}
