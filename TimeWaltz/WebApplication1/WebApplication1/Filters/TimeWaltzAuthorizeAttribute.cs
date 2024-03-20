using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System.Security.Claims;

namespace WebApplication1.Filters
{
    public class TimeWaltzAuthorize :Attribute, IAuthorizationFilter
    {
        private  TimeWaltzContext db;

        public TimeWaltzAuthorize()
        {
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            db = context.HttpContext.RequestServices.GetRequiredService<TimeWaltzContext>();
            var userClaims = context.HttpContext.User.Claims;
            var hasRequiredClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            var routeData = context.RouteData;
            string controllerName = routeData.Values["controller"]?.ToString();
            string actionName = routeData.Values["action"]?.ToString();
            if (hasRequiredClaim ==null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "", returnurl = context.HttpContext.Request.Path });
                return;
            }
            if (!CheckUserPermissionFromDatabase(int.Parse(hasRequiredClaim.Value), controllerName, actionName))
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "", returnurl = context.HttpContext.Request.Path });
            }
        }

        private bool CheckUserPermissionFromDatabase(int roleId, string controllerName, string actionName)
        {
            var rold = db.Roles.AsNoTracking().Include(x => x.Accesses).FirstOrDefault(x => x.Id == roleId);
            if (rold == null)
            {
                return false;
            }
            return rold.Accesses.Any(x => x.Controller == controllerName && x.Action == actionName);
        }
    }
}
