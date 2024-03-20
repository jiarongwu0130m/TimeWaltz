using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Models.Account;
using WebApplication1.Models.Entity;
using WebApplication1.Services;


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




        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            
            HttpContext.Response.Cookies.Append("sick","running nose");

            //去資料庫比對資料
            var user = _TimeWaltzContext.Users.FirstOrDefault(x => x.Account == model.Account && x.Stop==true );
            if (user == null)
            {
                ViewBag.Error = "帳號密碼錯誤";
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
                new Claim("EmployeesId",user.EmployeesId.ToString()),
                new Claim(ClaimTypes.Role,"user"),
                new Claim("DepartmentId", user.DepartmentId.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            return RedirectToAction("index", "home");
        }

    }
}
