using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using WebApplication1.Models.Account;
using WebApplication1.Services;
using Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {

        private TimeWaltzContext _TimeWaltzContext;
        private UserService _UserService;

        public AccountController(TimeWaltzContext timeWaltz, UserService UserService) 
        {
            _TimeWaltzContext = timeWaltz;
            _UserService=UserService;
        }
        #region 登入
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            //去資料庫比對資料
            var user = _TimeWaltzContext.Users.Include(x => x.UserOfAdmin).Include(x => x.Role).FirstOrDefault(x => x.Account == model.Account && x.Stop == false);
            if (user == null)
            {
                ViewBag.Error = "帳號密碼錯誤";
                return View();
            }
            if (user.RoleId == 2)
            {
                ViewBag.Error = "員工不能使用該系統";
                return View();
            }
            if (_UserService.SHA256EncryptString(model.Password + user.Salt) != user.Password)
            {
                ViewBag.Error = "帳號密碼錯誤";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim("Id",user.Id.ToString()),
                new Claim("Name",user.UserOfAdmin?.Name),
                new Claim(ClaimTypes.Role,user.RoleId.ToString()),
                new Claim("RoleName",user.Role.RoleName),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            return RedirectToAction("index", "home");
        }
        #endregion


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult GenPwd(string pwd)
        {
           var  salt= _UserService.GenerateSalt();
            var  result =_UserService.SHA256EncryptString(pwd + salt);
            return Json(new 
            {
                pwd= result,
                salt = salt
            });
        }
    }
}
