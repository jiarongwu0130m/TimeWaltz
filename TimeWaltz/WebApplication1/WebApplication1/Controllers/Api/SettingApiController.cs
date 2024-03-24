using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using WebApplication1.Models.SettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/Setting/[action]")]
    [ApiController]
    public class SettingApiController : ControllerBase
    {
        private readonly TimeWaltzContext _db;
        private readonly UserService _userService;

        public SettingApiController(TimeWaltzContext db,UserService userService)
        {
            _db = db;
            _userService = userService;
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


        /// <summary>
        ///     新增帳號
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AccountCreate(UserCreateModel model)
        {
            try
            {
                var u = _db.Users.FirstOrDefault(x => x.Account == model.Account);
                //帳號是否重複
                if (u != null) return Ok(new { status = false, msg = "帳號重複" });

                //密碼鹽
                var Salts = _userService.GenerateSalt();
                //密碼雜湊
                model.Password = _userService.SHA256EncryptString(model.Password + Salts);

                _db.Users.Add(new User
                {
                    Account = model.Account,
                    Password = model.Password,
                    Stop = model.Stop,
                    Salt = Salts,
                    RoleId = 2,
                    Employee = new Employee()
                    {
                        DepartmentId = model.DepartmentName,
                        HireDate = DateTime.Now,
                        Name = model.EmployeesName,
                        EmployeesNo = DateTime.Now.Ticks.ToString()
                    },
                    PasswordDate = DateTime.Now
                });
                _db.SaveChanges();
                return Ok(new { status = true, msg = "新增成功" });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, msg = "錯誤請聯絡管理員" });
            }
        }

    }
}
