using System.Web.Mvc;
using System.Web.Security;
using UCDArch.Web.Authentication;
using UCDArch.Web.Controller;

namespace HelpRequest.Controllers
{
    public class AccountController : ApplicationController
    {
        public ActionResult LogOn(string appName, string subject)
        {
            string resultUrl = CASHelper.Login(); //Do the CAS Login

            if (resultUrl != null)
            {
                if(!string.IsNullOrEmpty(appName))
                {
                    resultUrl = resultUrl + "?appName=" + appName;
                    if (!string.IsNullOrEmpty(subject))
                    {
                        resultUrl = resultUrl + "&subject=" + subject;
                    }
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
