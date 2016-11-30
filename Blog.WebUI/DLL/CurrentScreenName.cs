using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Web;

namespace Blog.WebUI.DLL
{

    public static class CurrentScreenName
    {

        public static string GetScreenName(this IIdentity identity)
        {
            return HttpContext.Current.Session["CurrentScreenName"]?.ToString() ?? "";
        }
    }
}
