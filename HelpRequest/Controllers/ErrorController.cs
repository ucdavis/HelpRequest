using System.Web.Mvc;
using UCDArch.Web.Controller;

namespace HelpRequest.Controllers
{
    public class ErrorController : ApplicationController
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return View(); 
        }

    }
}
