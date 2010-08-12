using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HelpRequest.Controllers.Filters;
using UCDArch.Web.Authentication;
using MvcContrib;
using UCDArch.Web.Controller;

namespace HelpRequest.Controllers
{
    public class AccountController : SuperController
    {
        public ActionResult LogOn(string appName)
        {
            string resultUrl = CASHelper.Login(); //Do the CAS Login

            if (resultUrl != null)
            {
                if(!string.IsNullOrEmpty(appName))
                {
                    resultUrl = resultUrl + "?appName=" + appName;
                }
                return Redirect(resultUrl);
            }

            


            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("https://cas.ucdavis.edu/cas/logout");
        }  
    }
}
