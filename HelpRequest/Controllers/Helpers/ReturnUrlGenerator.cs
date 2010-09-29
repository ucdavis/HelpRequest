using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HelpRequest.Controllers.Helpers
{
    public static class ReturnUrlGenerator
    {
        private static UrlHelper Url = new UrlHelper(new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData()));

        public static string LogOnReturn(string appName)
        {
            if (!string.IsNullOrEmpty(appName))
            {
                return Url.RouteUrl(new {controller = "Home", action = "Index"}) + "?appName=" + appName;
            }
            else
            {
                return Url.RouteUrl(new {controller = "Home", action = "Index"});
            }
        }


    }
}
