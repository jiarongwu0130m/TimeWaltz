using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models.ApplicationFormViewModels;
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

        public BillboardApiController(TimeWaltzContext timeWaltzDb, BillboardService billboardService)
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

        [HttpGet]
        public List<BillboardDto> GetBillboardList()
        {
            var entities = _billboardService.GetBillboardList();
            var models = EntityHelper.ToViewModel(entities);
            return models;
        }

        [HttpGet]
        public List<BillboardDto> GetBillBoardData()
        {
            var entities = _billboardService.GetBillboardList();
            var models = EntityHelper.ToViewModel(entities);
            return models;
        }
        [HttpGet]
        [Route("{id}")]
        public BillboardEditDto GetBillBoardData(int id)
        {
            try
            {
                var entity = _billboardService.GetBillboardTypeOrNull(id);
                var model = EntityHelper.ToEditViewModel(entity);
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public bool Edit(BillboardEditDto dto)
        {
            try
            {
                _billboardService.EditBillboard(dto);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
