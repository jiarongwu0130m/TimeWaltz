using Humanizer;
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
    public class FlextimeApiController : ControllerBase
    {
        private readonly TimeWaltzContext _context;
        private readonly FlextimeService _flextimeService;

        public FlextimeApiController(TimeWaltzContext context, FlextimeService flextimeService)
        {
            _context = context;
            _flextimeService = flextimeService;
        }
        [HttpGet]
        public FlextimeViewModel getFlextime()
        {
            var FlextimeEntity = _flextimeService.GetFlextime();

            var model = new FlextimeViewModel
            {
                Id = FlextimeEntity.Id,
                FlexibleTime = FlextimeEntity.FlexibleTime,
                MoveUp = FlextimeEntity.MoveUp.Value,
            };

            return model;
        }

        [HttpPost]
        public bool saveFelxtime(FlextimeViewModel model)
        {
            try
            {
                _flextimeService.UpdateFlextime(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
