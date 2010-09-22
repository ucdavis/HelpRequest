using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using UCDArch.Web.Controller;

namespace HelpRequest.Controllers
{
    public class ErrorController : SuperController
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return View();
        }

    }
}
