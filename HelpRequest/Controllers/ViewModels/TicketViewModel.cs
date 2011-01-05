using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using HelpRequest.Core.Domain;
using HelpRequest.Core.Resources;
using UCDArch.Core.PersistanceSupport;


namespace HelpRequest.Controllers.ViewModels
{
    public class TicketViewModel : GenericViewModel
    {       
        public List<string> Urgency { get; set; }
        public List<string> SupportDepartment { get; set; }
        public Ticket Ticket { get; set; }
        public List<string> ProgrammingSupportApps { get; set; }


        public static TicketViewModel Create(IRepository<Application> applicationRepository, IPrincipal currentUser, string appName, string subject)
        {
            //Check.Require(ticketRepository != null, "ticketRepository is required.");            
            var viewModel = new TicketViewModel();
            viewModel.AppName = appName;
            viewModel.PassedSubject = subject;
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
                viewModel.Ticket.Subject = subject;
            }

            viewModel.ProgrammingSupportApps = new List<string>();
            if (applicationRepository.Queryable.Where(x => x.Abbr == viewModel.AppName).Any())
            {
                viewModel.ProgrammingSupportApps.Add(applicationRepository.Queryable.Where(x => x.Abbr == viewModel.AppName).Single().ApplicationName);
                viewModel.Ticket.ForApplication = viewModel.ProgrammingSupportApps[0];
            }
            else
            {
                foreach (var application in applicationRepository.Queryable.OrderBy(x => x.SortOrder))
                {
                    viewModel.ProgrammingSupportApps.Add(application.ApplicationName);                    
                }
            }
            return viewModel;
        }
    }
}
