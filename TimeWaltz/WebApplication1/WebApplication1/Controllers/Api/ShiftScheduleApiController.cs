using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebApplication1.Helpers;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShiftScheduleApiController : ControllerBase
    {
        private readonly TimeWaltzContext _context;
        private readonly ShiftScheduleService _service;

        public ShiftScheduleApiController(TimeWaltzContext context,ShiftScheduleService service)
        {
            _context = context;
            _service = service;
        }
        public List<ShiftSchedulesViewModel> getShiftSchedule()
        {

            var entities = _service.GetShiftScheduleList();
            var models = EntityHelper.ToViewModel(entities);

            return models;
        }

    }
}
