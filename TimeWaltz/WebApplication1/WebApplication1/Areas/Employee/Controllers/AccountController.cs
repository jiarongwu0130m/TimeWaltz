using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Areas.Employee.Models;
using Repository.Models;
using WebApplication1.Services;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class AccountController : Controller
    {
        private readonly TimeWaltzContext db;
        private readonly UserService _userService;

        public AccountController(TimeWaltzContext db, UserService userService)
        {
            this.db = db;
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("EmployeeAuthScheme");
            return RedirectToAction("index","home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            //去資料庫比對資料
            var user = db.Users.Include(x => x.Employee).Include(x => x.Role).FirstOrDefault(x => x.Account == model.Account && x.Stop == false);
            if (user == null)
            {
                ViewBag.Error = "帳號密碼錯誤";
                return View();
            }
            if (user.RoleId == 1) 
            {
                ViewBag.Error = "管理員不能使用該系統";
                return View();
            }
            if (_userService.SHA256EncryptString(model.Password + user.Salt) != user.Password)
            {
                ViewBag.Error = "帳號密碼錯誤";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim("Id",user.Id.ToString()),
                new Claim("Name",user.Employee == null ? "":user.Employee.Name ),
                new Claim(ClaimTypes.Role,user.RoleId.ToString()),
                new Claim("RoleName",user.Role.RoleName),
            };

            var claimsIdentity = new ClaimsIdentity(claims,"EmployeeAuthScheme");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("EmployeeAuthScheme",claimsPrincipal);
            return RedirectToAction("Clock","Event");
        }
    }
}
