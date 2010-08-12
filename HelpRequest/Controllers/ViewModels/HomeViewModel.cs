using System.Collections.Generic;
using System.Linq;
using HelpRequest.Core.Domain;
using UCDArch.Core.PersistanceSupport;

namespace HelpRequest.Controllers.ViewModels
{
    public class HomeViewModel :GenericViewModel
    {
        public string ReturnUrl;
        public string ReturnAppName;

        public static HomeViewModel Create(IRepository<CatbertApplication> catbertApplications, string appName)
        {
            var viewModel = new HomeViewModel();
            viewModel.AppName = appName;
            if(!string.IsNullOrEmpty(appName))
            {
                var catbertApps = catbertApplications.GetAll().ToList();
                foreach (var catbertApplication in catbertApps)
                {
                    if (catbertApplication.Abbr != null && catbertApplication.Abbr.ToLower() == appName.ToLower() && !string.IsNullOrEmpty(catbertApplication.Location))
                    {
                        viewModel.ReturnUrl = catbertApplication.Location;
                        viewModel.ReturnAppName = catbertApplication.Name;
                        break;
                    }
                }
            }

            return viewModel;
        }
    }
}
