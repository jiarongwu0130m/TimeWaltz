using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebApplication1.Filters;
using WebApplication1.Helpers;
using WebApplication1.Models.BasicSettingViewModels;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DepartmentApiController : ControllerBase
    {
        private readonly TimeWaltzContext _db;

        public DepartmentApiController(TimeWaltzContext timeWaltzContext)
        {
            _db = timeWaltzContext;
        }
        [HttpGet]
        public ActionResult<DepartmentCreateDto> GetDropDownList()
        {
            try
            {
                var empList = _db.Employees.ToList();
                var model = new DepartmentCreateDto
                {
                    EmployeeNameSelectList = DropDownHelper
                           .GetEmployeeNameDropDownList(empList),
                };
                return model;
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }
    }
}
