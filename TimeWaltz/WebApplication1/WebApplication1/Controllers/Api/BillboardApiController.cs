using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebApplication1.Helpers;
using WebApplication1.Models.SettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BillboardApiController : ControllerBase
    {
        private readonly TimeWaltzContext _db;
        private readonly BillboardService _billboardService;

        public BillboardApiController(TimeWaltzContext db, BillboardService billboardService)
        {
            _db = db;
            _billboardService = billboardService;
        }

        [HttpPost]
        public bool Create(BillboardCreat model)
        {
            try
            {
                model.EmployeesID = User.GetId();
                
                var entity = new Billboard
                {
                    EmployeesId = 5,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    Title = model.Title,
                    Content = model.Content,
                };
                _db.Billboards.Add(entity);
                _db.SaveChanges();
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
