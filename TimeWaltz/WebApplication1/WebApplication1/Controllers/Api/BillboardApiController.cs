using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models.Entity;
using WebApplication1.Models.SettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BillboardApiController : ControllerBase
    {
        private readonly TimeWaltzContext _timeWaltzDb;
        private readonly BillboardService _billboardService;

        public BillboardApiController(TimeWaltzContext timeWaltzDb,BillboardService billboardService)
        {
            _timeWaltzDb = timeWaltzDb;
            _billboardService = billboardService;
        }
        [HttpPost]
        public bool Create(BillboardCreat dto)
        {
            try
            {
                var entity = ViewModelHelper.ToEntity(dto);
                 _billboardService.CreateBillboard(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
