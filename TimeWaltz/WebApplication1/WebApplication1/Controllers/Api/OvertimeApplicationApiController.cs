using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models.ApplicationFormViewModels;
using WebApplication1.Models.Entity;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class OvertimeApplicationApiController : ControllerBase
    {
        private readonly TimeWaltzContext _timeWaltzDb;
        private readonly OvertimeRequestService _overtimeRequestService;

        public OvertimeApplicationApiController(TimeWaltzContext timeWaltzDb,OvertimeRequestService overtimeRequestService)
        {
            _timeWaltzDb = timeWaltzDb;
            _overtimeRequestService = overtimeRequestService;
        }

        [HttpPost]
        public ActionResult OvertimeRequestCreate(OvertimeCreateViewModel model)
        {
            try
            {
                var entity = ViewModelHelper.ToEntity(model);
                _overtimeRequestService.CreateOvertimeRequest(entity);
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });

            }
        }
    }
}
