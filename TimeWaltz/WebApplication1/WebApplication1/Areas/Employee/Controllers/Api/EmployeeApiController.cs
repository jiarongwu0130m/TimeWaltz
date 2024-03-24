using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Models;
using WebApplication1.Filters;
using WebApplication1.Helpers;

namespace WebApplication1.Areas.Employee.Controllers.Api
{
    [Route("/mobile/EmployeeApi/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "EmployeeAuthScheme")]

    public class EmployeeApiController : ControllerBase
    {
        private readonly TimeWaltzContext _db;

        public EmployeeApiController(TimeWaltzContext db)
        {
            this._db = db;
        }
        /// <summary>
        /// 取得審核人下拉式選單資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<SelectListItem>> GetApprovalList()
        {
            try
            {
                var empId = User.GetId();
                var empDepartmentId = _db.Employees.Find(empId).DepartmentId;
                var result = _db.Employees.Where(x => x.Department.Id == empDepartmentId && x.IsManager == true).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return Ok(new { status = true });
            }
        }
    }
}
