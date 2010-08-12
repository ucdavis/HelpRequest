using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpRequest.Controllers.ViewModels;
using HelpRequest.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Web.Controller;

namespace HelpRequest.Controllers
{
    public class HomeController : SuperController
    {
        public IRepository<CatbertApplication> CatbertApplicationRepository;

        public HomeController (IRepository<CatbertApplication> catbertApplicationRepository)
        {
            CatbertApplicationRepository = catbertApplicationRepository;
        }
        public ActionResult Index(string appName)
        {
            if (string.IsNullOrEmpty(appName))
            {
                ViewData["Message"] = "Welcome to the Help Request submission form home page.";
            }
            else
            {
                ViewData["Message"] = string.Format("Welcome to the Help Request submission form home page for {0}.", appName);
            }
            return View(HomeViewModel.Create(CatbertApplicationRepository, appName));
        }

        public ActionResult About(string appName)
        {
            return View(GenericViewModel.Create(appName));
        }

        public ActionResult ReturnToCallingApplication(string url)
        {
            return Redirect(url);
        }
    }
}
