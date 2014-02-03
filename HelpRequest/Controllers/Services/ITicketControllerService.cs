using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using HelpRequest.Controllers.Helpers;
using HelpRequest.Core.Abstractions;
using HelpRequest.Core.Domain;
using HelpRequest.Core.Resources;
using UCDArch.Core.Utils;
using Attachment = System.Net.Mail.Attachment;

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

        void SendHelpRequest(Ticket ticket, bool isPublicEmail, IEmailProvider emailProvider);
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
            else if (ticket.SupportDepartment == StaticValues.STR_ProgrammingSupport)
            {
                if (string.IsNullOrWhiteSpace(ticket.ForApplication))
                {
                    modelState.AddModelError("Ticket.ForApplication", "For Programming Support you must pick the program from the list.");
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
            ticket.Attachments = new List<Core.Domain.Attachment>();
            if (uploadAttachment != null && uploadAttachment.ContentLength != 0)
            {
                var reader = new BinaryReader(uploadAttachment.InputStream);
                var attachment = new Core.Domain.Attachment(uploadAttachment.FileName, uploadAttachment.FileName);
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

        /// <summary>
        /// Sends the help request.
        /// #6
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="isPublicEmail">if set to <c>true</c> [is public email or if Kerb email].</param>
        /// <param name="emailProvider">The email provider.</param>
        public void SendHelpRequest(Ticket ticket, bool isPublicEmail, IEmailProvider emailProvider)
        {
            Check.Require(ticket != null, "Details are missing.");

            var supportEmail = GetHelpEmail(ticket);
            var fromEmail = "";
            if (isPublicEmail)
            {
                fromEmail = ticket.FromEmail;
            }
            else
            {
                Check.Require(ticket.User != null, "Login Details missing.");
                fromEmail = ticket.User.Email;
            }
            Check.Require(!string.IsNullOrEmpty(fromEmail), "Email details missing.");
            Check.Require(!string.IsNullOrEmpty(supportEmail), "Help Desk Email address not supplied.");

            MailMessage message = new MailMessage(fromEmail, supportEmail,
                                                   ticket.Subject,
                                                   BuildBody(ticket));

            foreach (var emailCC in ticket.EmailCCs)
            {
                if (!FilterCruEmail(emailCC))
                {
                    message.CC.Add(emailCC);
                }
            }
            foreach (var attachment in ticket.Attachments)
            {
                var messStream = new MemoryStream(attachment.Contents);
                var messAttach = new Attachment(messStream, attachment.FileName, attachment.ContentType);
                message.Attachments.Add(messAttach);
            }


            message.IsBodyHtml = false;

            emailProvider.SendEmail(message);

        }

        /// <summary>
        /// Builds the body of the email.
        /// #7
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns></returns>
        public string BuildBody(Ticket ticket)
        {
            var bodyBuilder = new StringBuilder();
            bodyBuilder.AppendLine("Original Subject     : " + ticket.Subject);
            bodyBuilder.AppendLine("Urgency Level        : " + ticket.UrgencyLevel);
            bodyBuilder.AppendLine("Support Department   : " + ticket.SupportDepartment);
            if (!string.IsNullOrEmpty(ticket.ForApplication) && ticket.SupportDepartment == StaticValues.STR_ProgrammingSupport)
            {
                bodyBuilder.AppendLine("For Application      : " + ticket.ForApplication);
            }
            if (!string.IsNullOrEmpty(ticket.ForWebSite) && ticket.SupportDepartment == StaticValues.STR_WebSiteSupport)
            {
                bodyBuilder.AppendLine("For Web Site         : " + ticket.ForWebSite);
            }
            if (ticket.Availability != null && ticket.Availability.Count > 0)
            {
                bodyBuilder.AppendLine("Available Times      : ");
                foreach (var availableDate in ticket.Availability)
                {
                    bodyBuilder.AppendLine("   " + availableDate);
                }
            }
            if(!string.IsNullOrWhiteSpace(ticket.YourPhoneNumber) && ticket.SupportDepartment == StaticValues.STR_ComputerSupport)
            {
                bodyBuilder.AppendLine(string.Format("Contact Phone Number : {0}", ticket.YourPhoneNumber));
            }
            if (!string.IsNullOrWhiteSpace(ticket.LocationOfProblem) && ticket.SupportDepartment == StaticValues.STR_ComputerSupport)
            {
                bodyBuilder.AppendLine(string.Format("Location             : {0}", ticket.LocationOfProblem));
            }
            bodyBuilder.AppendLine("");
            bodyBuilder.AppendLine("");
            bodyBuilder.AppendLine("Supplied Message Body :");
            bodyBuilder.AppendLine(ticket.MessageBody);

            return bodyBuilder.ToString();
        }

        /// <summary>
        /// Filters the cru email.
        /// #8
        /// </summary>
        /// <param name="emailCc">The email cc.</param>
        /// <returns></returns>
        public bool FilterCruEmail(string emailCc)
        {
            var cruEmail = new List<string>(10);
            #region Shuka
            cruEmail.Add("shuka@ucdavis.edu".ToLower());
            cruEmail.Add("shuka@caes.ucdavis.edu".ToLower());
            cruEmail.Add("smith@caes.ucdavis.edu".ToLower());
            cruEmail.Add("ssmith@ucdavis.edu".ToLower());
            cruEmail.Add("ssmith@caes.ucdavis.edu".ToLower());
            #endregion Shuka
            #region Uwe
            cruEmail.Add("urossbach@ucdavis.edu".ToLower());
            cruEmail.Add("hi@caes.ucdavis.edu".ToLower());
            cruEmail.Add("rossbach@caes.ucdavis.edu".ToLower());
            #endregion Uwe
            if (cruEmail.Contains(emailCc.ToLower()))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Gets the help email.
        /// #9
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns></returns>
        public string GetHelpEmail(Ticket ticket)
        {
            string helpEmail;
            if (ticket.SupportDepartment == StaticValues.STR_ProgrammingSupport)
            {
                helpEmail = ConfigurationManager.AppSettings["AppHelpDeskEmail"];
            }
            else if (ticket.SupportDepartment == StaticValues.STR_WebSiteSupport)
            {
                helpEmail = ConfigurationManager.AppSettings["WebHelpDeskEmail"];
            }
            else
            {
                helpEmail = ConfigurationManager.AppSettings["HelpDeskEmail"];
            }
            return helpEmail;
        }
    }
}
