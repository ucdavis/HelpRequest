using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using HelpRequest.Controllers.Filters;
using HelpRequest.Controllers.Helpers;
using HelpRequest.Controllers.ViewModels;
using HelpRequest.Core.Abstractions;
using HelpRequest.Core.Domain;
using HelpRequest.Core.Resources;
using MvcContrib;
using MvcContrib.Attributes;
using UCDArch.Web.Controller;
using UCDArch.Web.Validator;

namespace HelpRequest.Controllers
{
    public class TicketController : SuperController
    {
        private readonly IEmailProvider _emailProvider;

        public TicketController(IEmailProvider emailProvider)
        {
            _emailProvider = emailProvider;
        }

        public ActionResult LogOnAndSubmit(string appName)
        {
            CASHelper.Login(); //Do the CAS Login

            return this.RedirectToAction(a => a.SubmitRedirect(appName));
        }

        public ActionResult SubmitRedirect(string appName)
        {
            bool foundEmail = false;

            if(CurrentUser != null && CurrentUser.Identity != null && CurrentUser.Identity.IsAuthenticated)
            {               
                //if(CurrentUser.IsInRole(RoleNames.User) || CurrentUser.IsInRole(RoleNames.Admin))
                //{
                    var user = Repository.OfType<User>()
                        .Queryable
                        .Where(a => a.LoginId == CurrentUser.Identity.Name)
                        .FirstOrDefault();
                    if(user != null)
                    {
                        if(!string.IsNullOrEmpty(user.Email))
                        {
                            foundEmail = true;   
                        }
                    }
                //}
                if(!foundEmail)
                {
                    var kerbUser = DirectoryServices.FindUser(CurrentUser.Identity.Name); 
                    if(kerbUser != null)
                    {
                        if (!string.IsNullOrEmpty(kerbUser.EmailAddress))
                        {
                            foundEmail = true;
                        }
                    }
                }
            }
            if(foundEmail)
            {
                return this.RedirectToAction(a => a.Submit(appName));
            }
            return this.RedirectToAction(a => a.PublicSubmit(appName));
        }

        public ActionResult Submit(string appName)
        {
            //return View(TicketViewModel.Create(Repository, CurrentUser, appName));
            return View(TicketViewModel.Create(CurrentUser, appName));
        }


        [AcceptPost]
        [ValidateInput(false)]
        public ActionResult Submit(Ticket ticket, string[] avDates, string[] emailCCs, HttpPostedFileBase uploadAttachment, string appName, string availableDatesInput, string emailCCsInput)
        {
            bool foundEmail = false;
            var useKerbEmail = false;
            if (CurrentUser != null && CurrentUser.Identity != null)
            {
                //if (CurrentUser.IsInRole(RoleNames.User) || CurrentUser.IsInRole(RoleNames.Admin))
                //{
                    var user = Repository.OfType<User>()
                        .Queryable
                        .Where(a => a.LoginId == CurrentUser.Identity.Name)
                        .FirstOrDefault();
                    if (user != null)
                    {
                        if (!string.IsNullOrEmpty(user.Email))
                        {
                            foundEmail = true;

                            ticket.User = Repository
                                .OfType<User>()
                                .Queryable
                                .Where(a => a.LoginId == CurrentUser.Identity.Name)
                                .Single();
                        }
                    }
                //}
                if (!foundEmail)
                {
                    var kerbUser = DirectoryServices.FindUser(CurrentUser.Identity.Name);
                    if (kerbUser != null)
                    {
                        if (!string.IsNullOrEmpty(kerbUser.EmailAddress))
                        {
                            foundEmail = true;
                            useKerbEmail = true;
                            ticket.FromEmail = kerbUser.EmailAddress;
                        }
                    }
                }
            }
            if (!foundEmail)
            {
                Message = "Logged in email not found. Use this public submit instead.";
                return this.RedirectToAction(a => a.PublicSubmit(appName));
            }



            CommonSubmitValidationChecks(ticket, avDates, emailCCs, availableDatesInput, emailCCsInput);
            LoadFileContents(ticket, uploadAttachment);

            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, ticket.ValidationResults());

            if(ModelState.IsValid)
            {
                try
                {
                    var helpEmail = ConfigurationManager.AppSettings["HelpDeskEmail"];
                    if(ticket.SupportDepartment == StaticValues.STR_ProgrammingSupport)
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
                    //_emailProvider.SendHelpRequest(ticket, useKerbEmail, ConfigurationManager.AppSettings["HelpDeskEmail"]);
                    _emailProvider.SendHelpRequest(ticket, useKerbEmail, helpEmail);
                    Message = StaticValues.STR_HelpTicketSuccessfullySent;//"Help Ticket successfully sent";
                    return this.RedirectToAction<HomeController>(a => a.Index(appName));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Exception", "Application Exception sending email: " + ex.Message);
                    //var viewModel = TicketViewModel.Create(Repository, CurrentUser, appName);
                    var viewModel = TicketViewModel.Create(CurrentUser, appName);
                    viewModel.Ticket = ticket;
                    return View(viewModel);
                }
            }
            else
            {
                //var viewModel = TicketViewModel.Create(Repository, CurrentUser, appName);
                var viewModel = TicketViewModel.Create(CurrentUser, appName);
                viewModel.Ticket = ticket;
                return View(viewModel);
            }
        }

        private void CommonSubmitValidationChecks(Ticket ticket, string[] avDates, string[] emailCCs, string availableDatesInput, string emailCCsInput)
        {
            if (ticket.SupportDepartment == "Web Site Support")
            {
                if (string.IsNullOrEmpty(ticket.ForWebSite) || ticket.ForWebSite.Trim() == string.Empty)
                {
                    ModelState.AddModelError("Ticket.ForWebSite",
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
                        ModelState.AddModelError("Ticket.ForWebSite", "A valid Web Site Address is required.");
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
                foreach (var emailCC in emailCCs)
                {
                    if (!string.IsNullOrEmpty(emailCC))
                    {
                        ticket.EmailCCs.Add(emailCC.ToLower());
                    }
                }
            }
            var emailRegExVal =
                new Regex(
                    @"(^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$){1}|^$");
            var i = 0;
            foreach (var emailCC in ticket.EmailCCs)
            {
                i++;
                if (!emailRegExVal.IsMatch(emailCC.ToLower()))
                {
                    ModelState.AddModelError("emailCCsContainer", string.Format("Carbon Copy Email {0} is not valid", i));
                }
            
            }
            CheckForSupportEmailAddresses(ticket);
        }

        private void CheckForSupportEmailAddresses(Ticket ticket)
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
                ModelState.AddModelError("Ticket.FromEmail", "Your Email can't be a support email.");
            }
            var i = 0;
            foreach (var emailCC in ticket.EmailCCs)
            {
                i++;
                if(supportEmail.Contains(emailCC.ToLower()))
                {
                    ModelState.AddModelError("emailCCsContainer", string.Format("Carbon Copy Email {0} can't be a support email.", i));
                }
            }
        }

        public ActionResult PublicSubmit(string appName)
        {
            //return View(TicketViewModel.Create(Repository, CurrentUser, appName));
            return View(TicketViewModel.Create(CurrentUser, appName));
        }

        private static void LoadFileContents(Ticket ticket, HttpPostedFileBase uploadAttachment)
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

        [CaptchaValidator]
        [AcceptPost]
        [ValidateInput(false)]
        public ActionResult PublicSubmit(Ticket ticket, string[] avDates, string[] emailCCs, HttpPostedFileBase uploadAttachment, bool captchaValid, string appName, string availableDatesInput, string emailCCsInput)
        {
            if(!captchaValid)
            {
                ModelState.AddModelError("Captcha", "Captcha values are not valid.");
            }

            if(string.IsNullOrEmpty(ticket.FromEmail) || ticket.FromEmail.Trim() == string.Empty)
            {
                ModelState.AddModelError("Ticket.FromEmail", "Your email address is required.");
            }
            else
            {
                var regExVal = new Regex(@"(^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$){1}|^$");
   
                if (!regExVal.IsMatch(ticket.FromEmail.ToLower()))
                {
                    ModelState.AddModelError("Ticket.FromEmail", "A valid email address is required.");
                }
            }

            CommonSubmitValidationChecks(ticket, avDates, emailCCs, availableDatesInput, emailCCsInput);
            
            LoadFileContents(ticket, uploadAttachment);

            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, ticket.ValidationResults());

            if (ModelState.IsValid)
            {
                try
                {
                    var helpEmail = ConfigurationManager.AppSettings["HelpDeskEmail"];
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
                    //_emailProvider.SendHelpRequest(ticket, true, ConfigurationManager.AppSettings["HelpDeskEmail"]);
                    _emailProvider.SendHelpRequest(ticket, true, helpEmail);
                    Message = StaticValues.STR_HelpTicketSuccessfullySent;
                    return this.RedirectToAction<HomeController>(a => a.Index(appName));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Exception", "Application Exception sending email: " + ex.Message);
                    //var viewModel = TicketViewModel.Create(Repository, CurrentUser, appName);
                    var viewModel = TicketViewModel.Create(CurrentUser, appName);
                    viewModel.Ticket = ticket;
                    return View(viewModel);
                }
            }
            else
            {
                //var viewModel = TicketViewModel.Create(Repository, CurrentUser, appName);
                var viewModel = TicketViewModel.Create(CurrentUser, appName);
                viewModel.Ticket = ticket;
                return View(viewModel);
            }
        }


    }
}
