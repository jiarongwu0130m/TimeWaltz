using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace WebApplication1.Controllers.Api
{
    [Route("api/Setting/[action]")]
    [ApiController]
    public class SettingApiController : ControllerBase
    {
        private readonly TimeWaltzContext _db;

        public SettingApiController(TimeWaltzContext db)
        {
            _db = db;
        }
        public bool DelEmployee(int id)
        {
            try
            {
                var user = _db.Users.Find(id);
                if (user == null)
                {
                    return false;
                }
                user.Stop = true;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public object AllEmployee()
        {
            return _db.Users.AsNoTracking().Include(x=>x.Employee)
                .ThenInclude(x=>x.Department)
                .Where(x => x.RoleId == 2)
                .Select(x=> new
                {
                    x.Id,
                    x.Account,
                    x.Stop,
                    x.Employee.HireDate,
                    x.Employee.Name,
                    DepartmentId = x.Employee.Department.Id,
                    x.Employee.Department.DepartmentName
                }).ToList();
        }
        public object AllDepartment()
        {
            return _db.Departments.AsNoTracking()
                .Select(x => new
                {
                    Name=x.DepartmentName,
                    x.Id
                }).ToList();
        }
        
    }
}
