using System.Security.Claims;

namespace WebApplication1.Helpers
{
    public static class ClaimHelper
    {
        public static int GetEmployeeId(this ClaimsPrincipal principal) 
        {
            //var result = principal.Claims.FirstOrDefault(x => x.ValueType == "id");
            //if (result == null)
            //{
            //    throw new InvalidOperationException("護照裡面沒東西");
            //}
            //else {
            //    return Convert.ToInt32(result.Value);
            //}

            return 1;
        }

    }
}
