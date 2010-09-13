using System.Web.Mvc;
using HelpRequest.Controllers.Filters;
using HelpRequest.Controllers.Helpers;
using HelpRequest.Controllers.ViewModels;
using HelpRequest.Core.Domain;
using MvcContrib;
using MvcContrib.Attributes;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Web.Controller;
using UCDArch.Web.Helpers;

namespace HelpRequest.Controllers
{
    public class HelpController : SuperController
    {
        public IRepository<HelpTopic> HelpTopicRepository;

        public HelpController(IRepository<HelpTopic> helpTopicRepository)
        {
            HelpTopicRepository = helpTopicRepository;
        }


        /// <summary>
        /// Index
        /// #1
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <returns></returns>
        public ActionResult Index(string appName)
        {
            HelpTopicViewModel viewModel = HelpTopicViewModel.Create(HelpTopicRepository, CurrentUser, appName);
            return View(viewModel);
        }


        /// <summary>
        /// Create Get /Help/Create
        /// #2
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <returns></returns>
        [AdminOnly]
        public ActionResult Create(string appName)
        {
            var helpTopic = new HelpTopic();
            helpTopic.AppFilter = appName;
            var viewModel = HelpTopicViewModel.Create(HelpTopicRepository, CurrentUser, appName);
            viewModel.HelpTopic = helpTopic;
            return View(viewModel);
        }


        /// <summary>
        /// POST: /Help/Create
        /// #3
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="appName">Name of the app.</param>
        /// <returns></returns>
        [AdminOnly]
        [AcceptPost]
        [ValidateInput(false)]
        public ActionResult Create(HelpTopicViewModel viewModel, string appName)
        {
            var topic = Copiers.HelpTopic(new HelpTopic(), viewModel.HelpTopic);

            topic.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                HelpTopicRepository.EnsurePersistent(topic);
                Message = "Help Topic created";
                return this.RedirectToAction(a => a.Index(appName));
            }
            viewModel.HelpTopic = topic;
            return View(viewModel);
        }

        //
        // GET: /Help/Edit/5
        [AdminOnly]
        public ActionResult Edit(int id, string appName)
        {
            var helpTopic = HelpTopicRepository.GetNullableById(id);
            if (helpTopic == null)
            {
                Message = "Help Topic not found";
                return this.RedirectToAction(a => a.Index(appName));
            }

            var viewModel = HelpTopicViewModel.Create(HelpTopicRepository, CurrentUser, appName);
            viewModel.HelpTopic = helpTopic;
            return View(viewModel);
        }

        //
        // POST: /Help/Edit/5

        [AdminOnly]
        [AcceptPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, HelpTopic helpTopic, string appName)
        {
            var topic = HelpTopicRepository.GetNullableById(id);
            if (topic == null)
            {
                Message = "Help Topic not found";
                return this.RedirectToAction(a => a.Index(appName));
            }
            topic = Copiers.HelpTopic(topic, helpTopic);


            topic.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                HelpTopicRepository.EnsurePersistent(topic);
                Message = "Help Topic saved";
                return this.RedirectToAction(a => a.Index(appName));
            }

            var viewModel = HelpTopicViewModel.Create(HelpTopicRepository, CurrentUser, appName);
            viewModel.HelpTopic = topic;
            return View(viewModel);
        }

        //
        // GET: /Help/Details/5
        public ActionResult Details(int id, string appName)
        {
            var helpTopic = HelpTopicRepository.GetNullableById(id);
            if (helpTopic == null)
            {
                return this.RedirectToAction(a => a.Index(appName));
            }
            helpTopic.NumberOfReads++;
            HelpTopicRepository.EnsurePersistent(helpTopic);
            HelpTopicViewModel viewModel = HelpTopicViewModel.Create(HelpTopicRepository, CurrentUser, appName);
            viewModel.HelpTopic = helpTopic;
            return View(viewModel);
        }

        public ActionResult WatchVideo(int id, string appName)
        {
            var helpTopic = HelpTopicRepository.GetNullableById(id);
            if (helpTopic == null)
            {
                return this.RedirectToAction(a => a.Index(appName));
            }
            helpTopic.NumberOfReads++;
            HelpTopicRepository.EnsurePersistent(helpTopic);
            HelpTopicViewModel viewModel = HelpTopicViewModel.Create(HelpTopicRepository, CurrentUser, appName);
            viewModel.HelpTopic = helpTopic;
            return View(viewModel);
        }
    }
}
