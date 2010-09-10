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
            viewModel.Urgency.Add("Non-Critical Issue");
            viewModel.Urgency.Add("Scheduled Requests");
            viewModel.Urgency.Add("Workaround Available");
            viewModel.Urgency.Add("Work Stoppage");
            viewModel.Urgency.Add("Critical");
            
            
            
            

            viewModel.SupportDepartment = new List<string>();
            if (string.IsNullOrEmpty(viewModel.AppName))
            {
                viewModel.SupportDepartment.Add(StaticValues.STR_ComputerSupport);
                viewModel.SupportDepartment.Add(StaticValues.STR_WebSiteSupport);
                viewModel.SupportDepartment.Add(StaticValues.STR_ProgrammingSupport);
                //viewModel.SupportDepartment.Add("Computer Support");
                //viewModel.SupportDepartment.Add("Web Site Support");
                //viewModel.SupportDepartment.Add("Programming Support");
            }
            else
            {
                viewModel.SupportDepartment.Add(StaticValues.STR_ProgrammingSupport);
                //viewModel.SupportDepartment.Add("Programming Support");  
            }

            return viewModel;
        }
    }
}
