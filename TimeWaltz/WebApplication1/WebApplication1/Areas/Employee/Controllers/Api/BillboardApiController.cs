using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebApplication1.Helpers;
using WebApplication1.Models.SettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Areas.Employee.Controllers.Api
{
    [Route("mobile/api/Billboard/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "EmployeeAuthScheme")]
    public class BillboardApiController : ControllerBase
    {
        private readonly TimeWaltzContext _db;

        public BillboardApiController(TimeWaltzContext db)
        {
            _db = db;
        }

        [HttpPost]
        public bool Create(BillboardCreat model)
        {
            try
            {
                model.EmployeesID = User.GetId();

                var entity = new Billboard
                {
                    EmployeesId = model.EmployeesID,
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
        [Route("{id}")]
        public BillboardEditDto GetBillBoardData(int id)
        {
            try
            {
                var entity = _db.Billboards.FirstOrDefault(x => x.Id == id);
                var model = new BillboardEditDto
                {
                    Id = entity.Id,
                    EmployeesID = entity.EmployeesId,
                    Title = entity.Title,
                    Content = entity.Content,
                    StartTime = entity.StartTime,
                    EndTime = entity.EndTime,
                    PathRoute = entity.PathRoute,

                };
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
