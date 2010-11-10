using System.Web.Mvc;
using HelpRequest.Controllers.ViewModels;
using HelpRequest.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Web.Controller;
using MvcContrib;

namespace HelpRequest.Controllers
{
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

        /// <summary>
        /// About.
        /// #2
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <returns></returns>
        public ActionResult About(string appName)
        {
            return View(GenericViewModel.Create(appName));
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
        /// <returns></returns>
        [Authorize]
        public ActionResult AuthorizedHome(string appName)
        {
            if (!string.IsNullOrEmpty(appName))
            {
                if (appName.Contains("appName="))
                {
                    appName = appName.Substring(appName.LastIndexOf("appName=")+8);
                }
            }
            return this.RedirectToAction(a => a.Index(appName));
        }
    }
}
