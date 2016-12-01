using System.Security.Principal;
using System.Web;

namespace Blog.DLL
{

    public static class CurrentScreenName
    {

        public static string GetFullName(this IIdentity identity)
        {
            return HttpContext.Current.Request.Cookies["FullName"]?.Value ?? "";
        }
    }
}
