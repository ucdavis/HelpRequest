﻿using System.Linq;
using HelpRequest.Core.Domain;
using UCDArch.Core.PersistanceSupport;

namespace HelpRequest.Controllers.ViewModels
{
    public class HomeViewModel :GenericViewModel
    {
        public string ReturnUrl;
        public string ReturnAppName;

        public static HomeViewModel Create(IRepository<CatbertApplication> catbertApplications, string appName, string subject)
        {
            var viewModel = new HomeViewModel();
            viewModel.AppName = appName;
            viewModel.PassedSubject = subject;
            if(!string.IsNullOrEmpty(appName))
            {
                if (appName.ToLower().StartsWith("opp") || appName.ToLower().StartsWith("ace"))
                {
                    if (appName.ToLower().StartsWith("opp"))
                    {
                        viewModel.AppName = "PrePurchasing";
                    }
                    if (appName.ToLower().StartsWith("ace"))
                    {
                        viewModel.AppName = "Ace";
                    }
                }
                else
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
            }

            return viewModel;
        }
    }
}
