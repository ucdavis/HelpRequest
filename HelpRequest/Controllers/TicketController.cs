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
        private readonly ITicketControllerService _ticketControllerService;

        public TicketController(IEmailProvider emailProvider, ITicketControllerService ticketControllerService)
        {
            _emailProvider = emailProvider;
            _ticketControllerService = ticketControllerService;
        }

        /// <summary>
        /// Log on and submit.
        /// #1
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <returns></returns>
        public ActionResult LogOnAndSubmit(string appName)
        {
            //CASHelper.Login(); //Do the CAS Login
            _ticketControllerService.CasLogin();

            return this.RedirectToAction(a => a.SubmitRedirect(appName));
        }

        /// <summary>
        /// Submit and redirect. Determines if it goes to the public or authenticated one.
        /// #2
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <returns></returns>
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
                    var kerbUser = _ticketControllerService.FindKerbUser(CurrentUser.Identity.Name);//DirectoryServices.FindUser(CurrentUser.Identity.Name); 
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

        /// <summary>
        /// Submit Ticket
        /// #3
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Submit(string appName)
        {
            //return View(TicketViewModel.Create(Repository, CurrentUser, appName));
            return View(TicketViewModel.Create(CurrentUser, appName));
        }


        [AcceptPost]
        [ValidateInput(false)]
        [Authorize]
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
                    var kerbUser = _ticketControllerService.FindKerbUser(CurrentUser.Identity.Name); //DirectoryServices.FindUser(CurrentUser.Identity.Name);
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



            _ticketControllerService.CommonSubmitValidationChecks(ModelState, ticket, avDates, emailCCs, availableDatesInput, emailCCsInput);
            _ticketControllerService.LoadFileContents(ticket, uploadAttachment);

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


        public ActionResult PublicSubmit(string appName)
        {
            //return View(TicketViewModel.Create(Repository, CurrentUser, appName));
            return View(TicketViewModel.Create(CurrentUser, appName));
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

            _ticketControllerService.CommonSubmitValidationChecks(ModelState, ticket, avDates, emailCCs, availableDatesInput, emailCCsInput);

            _ticketControllerService.LoadFileContents(ticket, uploadAttachment);

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
