using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using HelpRequest.Core.Domain;
using HelpRequest.Core.Resources;

namespace HelpRequest.Controllers.Helpers
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
                    var regExVal =
                        new Regex(
                            StaticValues.WebSiteRegEx);
                    //@"(^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$){1}|^$");

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
            var emailRegExVal =
                new Regex(
                    @"(^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$){1}|^$");
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

        public void CasLogin()
        {
            CASHelper.Login();
        }

        /// <summary>
        /// Finds the kerberos user.
        /// </summary>
        /// <param name="identityName">Name of the identity.</param>
        /// <returns></returns>
        public DirectoryUser FindKerbUser(string identityName)
        {
            return DirectoryServices.FindUser(identityName);
        }
    }
}
