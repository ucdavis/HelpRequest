using System.Web.Mvc;
using HelpRequest.Controllers.Filters;
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

        //
        // GET: /Help/
        public ActionResult Index(string appName)
        {
            HelpTopicViewModel viewModel = HelpTopicViewModel.Create(HelpTopicRepository, CurrentUser, appName);
            return View(viewModel);
        }

        //
        // GET: /Help/Create
        [AdminOnly]
        public ActionResult Create(string appName)
        {
            var helpTopic = new HelpTopic();
            helpTopic.AppFilter = appName;
            var viewModel = HelpTopicViewModel.Create(HelpTopicRepository, CurrentUser, appName);
            viewModel.HelpTopic = helpTopic;
            return View(viewModel);
        }

        //
        // POST: /Help/Create

        [AdminOnly]
        [AcceptPost]
        [ValidateInput(false)]
        public ActionResult Create(HelpTopicViewModel viewModel, string appName)
        {
            var topic = new HelpTopic();
            topic.Question = viewModel.HelpTopic.Question;
            topic.Answer = viewModel.HelpTopic.Answer;
            topic.IsActive = viewModel.HelpTopic.IsActive;
            topic.NumberOfReads = viewModel.HelpTopic.NumberOfReads;
            topic.IsVideo = viewModel.HelpTopic.IsVideo;
            topic.VideoName = viewModel.HelpTopic.VideoName;
            topic.AvailableToPublic = viewModel.HelpTopic.AvailableToPublic;
            topic.AppFilter = viewModel.HelpTopic.AppFilter;

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
            if (helpTopic == null)
            {
                return this.RedirectToAction(a => a.Index(appName));
            }
            topic.Question = helpTopic.Question;
            topic.Answer = helpTopic.Answer;
            topic.IsActive = helpTopic.IsActive;
            topic.NumberOfReads = helpTopic.NumberOfReads;
            topic.IsVideo = helpTopic.IsVideo;
            topic.VideoName = helpTopic.VideoName;
            topic.AvailableToPublic = helpTopic.AvailableToPublic;
            topic.AppFilter = helpTopic.AppFilter;
            topic.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                HelpTopicRepository.EnsurePersistent(topic);
                Message = "Help Topic saved";
                return this.RedirectToAction(a => a.Index(appName));
            }

            return View(topic);
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
