﻿using System.Collections.Generic;
using System.Security.Principal;
using HelpRequest.Core.Domain;
using HelpRequest.Core.Resources;


namespace HelpRequest.Controllers.ViewModels
{
    public class TicketViewModel : GenericViewModel
    {       
        public List<string> Urgency { get; set; }
        public List<string> SupportDepartment { get; set; }
        public Ticket Ticket { get; set; }


        public static TicketViewModel Create(IPrincipal currentUser, string appName)
        {
            //Check.Require(ticketRepository != null, "ticketRepository is required.");            
            var viewModel = new TicketViewModel();
            viewModel.AppName = appName;
            viewModel.Ticket = new Ticket();

            viewModel.Urgency = new List<string>(5);
            viewModel.Urgency.Add(StaticValues.STR_UrgencyLevelNonCritical);
            viewModel.Urgency.Add(StaticValues.STR_UrgencyLevelScheduledRequests);
            viewModel.Urgency.Add(StaticValues.STR_UrgencyLevelWorkaroundAvailable);
            viewModel.Urgency.Add(StaticValues.STR_UrgencyLevelWorkStoppage);
            viewModel.Urgency.Add(StaticValues.STR_UrgencyLevelCritical);
            
            
            viewModel.SupportDepartment = new List<string>();
            if (string.IsNullOrEmpty(viewModel.AppName))
            {
                viewModel.SupportDepartment.Add(StaticValues.STR_ComputerSupport);
                viewModel.SupportDepartment.Add(StaticValues.STR_WebSiteSupport);
                viewModel.SupportDepartment.Add(StaticValues.STR_ProgrammingSupport);
            }
            else
            {
                viewModel.SupportDepartment.Add(StaticValues.STR_ProgrammingSupport);
            }

            return viewModel;
        }
    }
}
