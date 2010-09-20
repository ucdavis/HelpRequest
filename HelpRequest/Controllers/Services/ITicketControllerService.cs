using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using HelpRequest.Controllers.Helpers;
using HelpRequest.Core.Domain;
using HelpRequest.Core.Resources;

namespace HelpRequest.Controllers.Services
{
    public interface ITicketControllerService
    {
        void CheckForSupportEmailAddresses(ModelStateDictionary modelState, Ticket ticket);

        void CommonSubmitValidationChecks(ModelStateDictionary modelState, Ticket ticket, string[] avDates, string[] emailCCs, string availableDatesInput,
                                          string emailCCsInput);

        void LoadFileContents(Ticket ticket, HttpPostedFileBase uploadAttachment);

        void CasLogin();

        DirectoryUser FindKerbUser(string identityName);
    }


    public class TicketControllerService : ITicketControllerService
    {
        /// <summary>
        /// Checks for support email addresses.
        /// #1
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="ticket">The ticket.</param>
        public void CheckForSupportEmailAddresses(ModelStateDictionary modelState, Ticket ticket)
        {
            var supportEmail = new List<string>(10);
            supportEmail.Add("ASISupport@caes.ucdavis.edu".ToLower());
            supportEmail.Add("AppRequests@caes.ucdavis.edu".ToLower());
            supportEmail.Add("Clusters@caes.ucdavis.edu".ToLower());
            supportEmail.Add("CSRequests@caes.ucdavis.edu".ToLower());
            supportEmail.Add("ITPLPNEM@ucdavis.edu".ToLower());
            supportEmail.Add("PLPNEMITSupport@caes.ucdavis.edu".ToLower());
            supportEmail.Add("ITSupport@ucdavis.edu".ToLower());
            supportEmail.Add("OCSSupport@caes.ucdavis.edu".ToLower());
            supportEmail.Add("OGSWeb@caes.ucdavis.edu".ToLower());
            supportEmail.Add("WebRequests@caes.ucdavis.edu".ToLower());

            if (!string.IsNullOrEmpty(ticket.FromEmail) && supportEmail.Contains(ticket.FromEmail.ToLower()))
            {
                modelState.AddModelError("Ticket.FromEmail", "Your Email can't be a support email.");
            }
            var i = 0;
            foreach (var emailCc in ticket.EmailCCs)
            {
                i++;
                if (supportEmail.Contains(emailCc.ToLower()))
                {
                    modelState.AddModelError("emailCCsContainer", string.Format("Carbon Copy Email {0} can't be a support email.", i));
                }
            }
        }

        /// <summary>
        /// Common submit validation checks (Common to both public and non-public submit).
        /// #2
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="ticket">The ticket.</param>
        /// <param name="avDates">The av dates.</param>
        /// <param name="emailCCs">The email C cs.</param>
        /// <param name="availableDatesInput">The available dates input.</param>
        /// <param name="emailCCsInput">The email C cs input.</param>
        public void CommonSubmitValidationChecks(ModelStateDictionary modelState, Ticket ticket, string[] avDates, string[] emailCCs, string availableDatesInput, string emailCCsInput)
        {
            if (ticket.SupportDepartment == "Web Site Support")
            {
                if (string.IsNullOrEmpty(ticket.ForWebSite) || ticket.ForWebSite.Trim() == string.Empty)
                {
                    modelState.AddModelError("Ticket.ForWebSite",
                                             "Web Site Address must be entered when Web Site Support is selected.");
                }
                else
                {
                    var regExVal = new Regex(StaticValues.WebSiteRegEx);                    

                    if (!regExVal.IsMatch(ticket.ForWebSite.ToLower()))
                    {
                        modelState.AddModelError("Ticket.ForWebSite", "A valid Web Site Address is required.");
                    }
                }
            }
            if (!string.IsNullOrEmpty(availableDatesInput))
            {
                ticket.Availability.Add(availableDatesInput);
            }

            if (avDates != null)
            {
                foreach (var avDate in avDates)
                {
                    if (!string.IsNullOrEmpty(avDate))
                    {
                        ticket.Availability.Add(avDate);
                    }
                }
            }

            ticket.EmailCCs = new List<string>();
            if (!string.IsNullOrEmpty(emailCCsInput))
            {
                ticket.EmailCCs.Add(emailCCsInput.ToLower());
            }
            if (emailCCs != null)
            {
                foreach (var emailCc in emailCCs)
                {
                    if (!string.IsNullOrEmpty(emailCc))
                    {
                        ticket.EmailCCs.Add(emailCc.ToLower());
                    }
                }
            }
            var emailRegExVal = new Regex(StaticValues.EmailErrorRegEx);
            var i = 0;
            foreach (var emailCc in ticket.EmailCCs)
            {
                i++;
                if (!emailRegExVal.IsMatch(emailCc.ToLower()))
                {
                    modelState.AddModelError("emailCCsContainer", string.Format("Carbon Copy Email {0} is not valid", i));
                }

            }
            CheckForSupportEmailAddresses(modelState, ticket);
        }

        /// <summary>
        /// Loads the file contents.
        /// #3
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="uploadAttachment">The upload attachment.</param>
        public void LoadFileContents(Ticket ticket, HttpPostedFileBase uploadAttachment)
        {
            ticket.Attachments = new List<Attachment>();
            if (uploadAttachment != null && uploadAttachment.ContentLength != 0)
            {
                var reader = new BinaryReader(uploadAttachment.InputStream);
                var attachment = new Attachment(uploadAttachment.FileName, uploadAttachment.FileName);
                attachment.Contents = reader.ReadBytes(uploadAttachment.ContentLength);
                attachment.FileName = uploadAttachment.FileName;
                attachment.ContentType = uploadAttachment.ContentType;
                ticket.Attachments.Add(attachment);
            }
        }

        /// <summary>
        /// CAS Login
        /// #4
        /// </summary>
        public void CasLogin()
        {
            CASHelper.Login();
        }

        /// <summary>
        /// Finds the kerberos user.
        /// #5
        /// </summary>
        /// <param name="identityName">Name of the identity.</param>
        /// <returns></returns>
        public DirectoryUser FindKerbUser(string identityName)
        {
            return DirectoryServices.FindUser(identityName);
        }
    }
}
