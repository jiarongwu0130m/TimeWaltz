using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using WebApplication1.Helpers;
using WebApplication1.Models.ApplicationFormViewModels;
using WebApplication1.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdditionalClockInApiController : ControllerBase
    {
        private readonly TimeWaltzContext _timeWaltzDb;
        private readonly CompRequestService _compRequestService;

        public AdditionalClockInApiController(TimeWaltzContext timeWaltzDb,CompRequestService compRequestService)
        {
            _timeWaltzDb = timeWaltzDb;
            _compRequestService = compRequestService;
        }

        // GET: api/AdditionalClockInsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionalClockIn>>> GetAdditionalClockIns()
        {
            if (_timeWaltzDb.AdditionalClockIns == null)
            {
                return NotFound();
            }
            return await _timeWaltzDb.AdditionalClockIns.ToListAsync();
        }

        public CompRequestCreateViewModel DropDownList()
        {
            var model = new CompRequestCreateViewModel
            {
                ClockStatusSelectItems = DropDownHelper.GetClockStatusDropDownList(),
            };
            return model;
        }
        [HttpPost]
        public ActionResult CompRequestCreate(CompRequestCreateViewModel model)
        {
            try
            {
                var entity = ViewModelHelper.ToEntity(model);
                _compRequestService.CreateCompRequest(entity);
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });

            }
        }

        [HttpGet]
        [Route("{id}")]
        public CompRequestViewModel GetCompRequestData(int id)
        {
            try
            {
                var entity = _compRequestService.GetCompRequestTypeOrNull(id);
                var model = EntityHelper.ToViewModel(entity);
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
