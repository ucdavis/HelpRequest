using System;
using System.Web.Mvc;
using HelpRequest.Controllers.ViewModels;
using HelpRequest.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Web.Controller;
using MvcContrib;
using HelpRequest.Controllers.Filters;

namespace HelpRequest.Controllers
{   
    [Version]
    public class HomeController : SuperController
    {
        public IRepository<CatbertApplication> CatbertApplicationRepository;

        public HomeController (IRepository<CatbertApplication> catbertApplicationRepository)
        {
            CatbertApplicationRepository = catbertApplicationRepository;
        }

        /// <summary>
        /// Index
        /// #1
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <returns></returns>
        public ActionResult Index(string appName, string subject)
        {
            if (string.IsNullOrEmpty(appName))
            {
                ViewData["Message"] = "Welcome to the Help Request submission form home page.";
            }
            else
            {
                ViewData["Message"] = string.Format("Welcome to the Help Request submission form home page for {0}.", appName);
            }
            return View(HomeViewModel.Create(CatbertApplicationRepository, appName, subject));
        }

        /// <summary>
        /// About.
        /// #2
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public ActionResult About(string appName, string subject)
        {
            return View(GenericViewModel.Create(appName, subject));
        }

        /// <summary>
        /// Returns to calling application.
        /// #3
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public ActionResult ReturnToCallingApplication(string url)
        {
            return Redirect(url);
        }

        /// <summary>
        /// AuthorizedHome
        /// #4
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult AuthorizedHome(string appName, string subject)
        {
            if (!string.IsNullOrEmpty(appName))
            {
                if (appName.Contains("appName="))
                {
                    appName = appName.Substring(appName.LastIndexOf("appName=")+8);
                }
            }
            if (!string.IsNullOrWhiteSpace(subject))
            {
                var subjectLength = (subject.Trim().Length - 1)/2;
                if (subject[subjectLength] == ',')
                {
                    var subjectFirst = subject.Substring(0, subjectLength);
                    var subjectLast = subject.Substring(subjectLength+1);
                    if (subjectFirst == subjectLast)
                    {
                        subject = subjectLast;
                    }
                }
            }
            return this.RedirectToAction(a => a.Index(appName, subject));
        }
    }
}
