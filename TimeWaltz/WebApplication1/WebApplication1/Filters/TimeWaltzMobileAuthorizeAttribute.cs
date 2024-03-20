using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Filters
{
    public class TimeWaltzMobileAuthorize : Attribute, IAuthorizationFilter
    {
        private TimeWaltzContext db;

        public TimeWaltzMobileAuthorize()
        {
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            db = context.HttpContext.RequestServices.GetRequiredService<TimeWaltzContext>();
            var userClaims = context.HttpContext.User.Claims;
            var hasRequiredClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            var path = GetPathName(context);

            if (hasRequiredClaim == null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "", returnurl = context.HttpContext.Request.Path });
                return;
            }
            if (!CheckUserPermissionFromDatabase(int.Parse(hasRequiredClaim.Value), path.controllerName, path.actionName))
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "", returnurl = context.HttpContext.Request.Path });
                return;
            }
        }

        private static (string controllerName, string actionName) GetPathName(AuthorizationFilterContext context)
        {
            var routeData = context.RouteData;
            var controllerName = routeData.Values["controller"]?.ToString();
            var actionName = routeData.Values["action"]?.ToString();
            return (controllerName, actionName);
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
