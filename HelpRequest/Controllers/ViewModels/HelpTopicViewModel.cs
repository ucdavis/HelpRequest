using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using HelpRequest.Controllers.Filters;
using HelpRequest.Core.Domain;
using HelpRequest.Core.Resources;
using UCDArch.Core.PersistanceSupport;
using Check = UCDArch.Core.Utils.Check;


namespace HelpRequest.Controllers.ViewModels
{
    public class HelpTopicViewModel : GenericViewModel
    {
        public bool IsUserAuthorized { get; set; }
        public bool IsUserAdmin { get; set; }
        public IEnumerable<HelpTopic> HelpTopics { get; set; }
        public HelpTopic HelpTopic { get; set; }

        public static HelpTopicViewModel Create(IRepository<HelpTopic> helpTopicRepository, IRepository<Application> applicationRepository, IPrincipal currentUser, string appName, string passedSubject)
        {
            Check.Require(helpTopicRepository != null, "helpTopicRepository is required.");

            var viewModel = new HelpTopicViewModel();
            viewModel.AppName = appName;
            viewModel.PassedSubject = passedSubject;
            viewModel.IsUserAuthorized = currentUser.IsInRole(RoleNames.Admin) || currentUser.IsInRole(RoleNames.User);
            viewModel.IsUserAdmin = currentUser.IsInRole(RoleNames.Admin);

            if (viewModel.IsUserAdmin)
            {
                viewModel.HelpTopics = helpTopicRepository.Queryable;
            }
            else if (viewModel.IsUserAuthorized)
            {
                if (string.IsNullOrEmpty(viewModel.AppName))
                {
                    viewModel.HelpTopics = helpTopicRepository
                        .Queryable.Where(a => a.IsActive && (a.AppFilter == null || a.AppFilter == string.Empty || a.AppFilter == StaticValues.STR_HelpRequest));
                }
                else
                {
                    viewModel.HelpTopics = helpTopicRepository
                        .Queryable.Where(a => a.IsActive && (a.AppFilter == null || a.AppFilter == string.Empty || a.AppFilter == viewModel.AppName));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(viewModel.AppName))
                {
                    viewModel.HelpTopics = helpTopicRepository
                        .Queryable.Where(a => a.AvailableToPublic && a.IsActive && (a.AppFilter == null || a.AppFilter == string.Empty || a.AppFilter == StaticValues.STR_HelpRequest))
                        .OrderByDescending(a => a.NumberOfReads);
                }
                else
                {
                    if (applicationRepository.Queryable.Where(a => a.Abbr == viewModel.AppName && a.HideOtherFaq).Any())
                    {
                        viewModel.HelpTopics = helpTopicRepository
                            .Queryable.Where(a => a.AvailableToPublic && a.IsActive && a.AppFilter == viewModel.AppName)
                            .OrderByDescending(a => a.NumberOfReads);
                    }
                    else
                    {
                        viewModel.HelpTopics = helpTopicRepository
                            .Queryable.Where(a =>a.AvailableToPublic && a.IsActive &&(a.AppFilter == null || a.AppFilter == string.Empty || a.AppFilter == viewModel.AppName))
                            .OrderByDescending(a => a.NumberOfReads);
                    }
                }
            }

            return viewModel;
        }
    }
}
