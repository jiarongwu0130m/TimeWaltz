using Repository.Models;
using System.Security.Claims;

namespace WebApplication1.Helpers
{
    public static class ClaimHelper
    {
        public static int GetId(this ClaimsPrincipal principal) 
        {
            var result = GetClaimValue(principal, "Id");
            return Convert.ToInt32(result);
        }
        public static string GetName(this ClaimsPrincipal principal)
        {
            return GetClaimValue(principal, "Name");
        }
        public static int GetRoleId(this ClaimsPrincipal principal)
        {
            var result= GetClaimValue(principal, ClaimTypes.Role);
            return Convert.ToInt32(result);
        }
        public static string GetRoleName(this ClaimsPrincipal principal)
        {
            return GetClaimValue(principal, "RoleName");
        }

        private static string GetClaimValue(ClaimsPrincipal principal, string type) 
        {
            var result = principal.Claims.FirstOrDefault(x => x.ValueType == type);
            if (result == null) throw new InvalidOperationException($"{type} is null");
            return result.Value;
        }

    }
}
