using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.Serialization;

namespace WebApplication1.Helpers
{
    public static class DateExtenstion
    {
        public static string ToISODateTimeString(this DateTime dateTime ) {
            return dateTime.ToString("u").Replace(" ", "T");
        }
    }
}
