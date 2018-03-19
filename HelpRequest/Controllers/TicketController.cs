using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Net;
using HelpRequest.Controllers.Filters;
using HelpRequest.Controllers.Services;
using HelpRequest.Controllers.ViewModels;
using HelpRequest.Core.Abstractions;
using HelpRequest.Core.Domain;
using HelpRequest.Core.Resources;
using MvcContrib;
using UCDArch.Web.Controller;
using UCDArch.Web.Validator;
using System.Configuration;

namespace HelpRequest.Controllers
{
    public class TicketController : ApplicationController
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
        /// <param name="subject"></param>
        /// <returns></returns>
        public ActionResult LogOnAndSubmit(string appName, string subject)
        {
            //CASHelper.Login(); //Do the CAS Login
            _ticketControllerService.CasLogin();

            return this.RedirectToAction(a => a.SubmitRedirect(appName, subject));
        }

        /// <summary>
        /// Submit and redirect. Determines if it goes to the public or authenticated one.
        /// #2
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public ActionResult SubmitRedirect(string appName, string subject)
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
                return this.RedirectToAction(a => a.Submit(appName, subject));
            }
            return this.RedirectToAction(a => a.PublicSubmit(appName, subject));
        }

        /// <summary>
        /// Submit Ticket Get
        /// #3
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <param name="subject"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Submit(string appName, string subject)
        {
            //return View(TicketViewModel.Create(Repository, CurrentUser, appName));
            if (!string.IsNullOrEmpty(appName))
            {
                if (appName.Contains("appName="))
                {
                    appName = appName.Substring(appName.LastIndexOf("appName=") + 8);
                }
            }
            if (!string.IsNullOrWhiteSpace(subject))
            {
                var subjectLength = (subject.Trim().Length - 1) / 2;
                if (subject[subjectLength] == ',')
                {
                    var subjectFirst = subject.Substring(0, subjectLength);
                    var subjectLast = subject.Substring(subjectLength + 1);
                    if (subjectFirst == subjectLast)
                    {
                        subject = subjectLast;
                    }
                }
            }
            return View(TicketViewModel.Create(Repository.OfType<Application>(), CurrentUser, appName, subject));
        }


        /// <summary>
        /// Submit Ticket Post
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="avDates">Array of available dates as strings.</param>
        /// <param name="emailCCs">Array of email Carbon Copy.</param>
        /// <param name="uploadAttachment">The upload attachment.</param>
        /// <param name="appName">Name of the app.</param>
        /// <param name="passedSubject"></param>
        /// <param name="availableDatesInput">Non-Array available dates as strings.</param>
        /// <param name="emailCCsInput">Non-Array email Carbon Copy</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult Submit(Ticket ticket, string[] avDates, string[] emailCCs, HttpPostedFileBase uploadAttachment, string appName, string passedSubject,string availableDatesInput, string emailCCsInput)
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
                return this.RedirectToAction(a => a.PublicSubmit(appName, passedSubject));
            }



            _ticketControllerService.CommonSubmitValidationChecks(ModelState, ticket, avDates, emailCCs, availableDatesInput, emailCCsInput);
            _ticketControllerService.LoadFileContents(ticket, uploadAttachment);

            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, ticket.ValidationResults());

            if(ModelState.IsValid)
            {
                try
                {
                    //_emailProvider.SendHelpRequest(ticket, useKerbEmail, GetHelpEmail(ticket));
                    _ticketControllerService.SendHelpRequest(ticket, useKerbEmail, _emailProvider);
                    Message = StaticValues.STR_HelpTicketSuccessfullySent;//"Help Ticket successfully sent";
                    return this.RedirectToAction<HomeController>(a => a.Index(appName, passedSubject));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Exception", "Application Exception sending email: " + ex.Message);
                    //var viewModel = TicketViewModel.Create(Repository, CurrentUser, appName);
                    var viewModel = TicketViewModel.Create(Repository.OfType<Application>(), CurrentUser, appName, passedSubject);
                    viewModel.Ticket = ticket;
                    return View(viewModel);
                }
            }
            else
            {
                //var viewModel = TicketViewModel.Create(Repository, CurrentUser, appName);
                var viewModel = TicketViewModel.Create(Repository.OfType<Application>(), CurrentUser, appName, passedSubject);
                viewModel.Ticket = ticket;
                var test = View(viewModel);

                return View(viewModel);
            }
        }


        /// <summary>
        /// Public submit get.
        /// #5
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public ActionResult PublicSubmit(string appName, string subject)
        {
            //return View(TicketViewModel.Create(Repository, CurrentUser, appName));
            return View(TicketViewModel.Create(Repository.OfType<Application>(), CurrentUser, appName, subject));
        }


        /// <summary>
        /// Public submit post.
        /// #6
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="avDates">Array of available dates as strings.</param>
        /// <param name="emailCCs">Array of email Carbon Copy.</param>
        /// <param name="uploadAttachment">The upload attachment.</param>
        /// <param name="captchaValid">true/false</param>
        /// <param name="appName">Name of the app.</param>
        /// <param name="passedSubject"></param>
        /// <param name="availableDatesInput">Non-Array available dates as strings.</param>
        /// <param name="emailCCsInput">Non-Array email Carbon Copy</param>
        /// <returns></returns>
        //[CaptchaValidator]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PublicSubmit(Ticket ticket, string[] avDates, string[] emailCCs, HttpPostedFileBase uploadAttachment, string appName, string passedSubject, string availableDatesInput, string emailCCsInput)
        {
            var captchaValid = false;
            var response = Request.Form["g-Recaptcha-Response"];
            using (var client = new WebClient())
            {
                var text = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", ConfigurationManager.AppSettings["NewRecaptchaPrivateKey"], response));
                captchaValid = text.Contains("\"success\": true");
            }

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
                var regExVal = new Regex(StaticValues.EmailErrorRegEx);
   
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
                    //_emailProvider.SendHelpRequest(ticket, true, GetHelpEmail(ticket));
                    _ticketControllerService.SendHelpRequest(ticket, true, _emailProvider);
                    Message = StaticValues.STR_HelpTicketSuccessfullySent;
                    return this.RedirectToAction<HomeController>(a => a.Index(appName, passedSubject));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Exception", "Application Exception sending email: " + ex.Message);
                    var viewModel = TicketViewModel.Create(Repository.OfType<Application>(), CurrentUser, appName, passedSubject);
                    viewModel.Ticket = ticket;
                    return View(viewModel);
                }
            }
            else
            {
                var viewModel = TicketViewModel.Create(Repository.OfType<Application>(), CurrentUser, appName, passedSubject);
                viewModel.Ticket = ticket;
                return View(viewModel);
            }
        }

        //private string GetHelpEmail(Ticket ticket)
        //{
        //    string helpEmail;
        //    if (ticket.SupportDepartment == StaticValues.STR_ProgrammingSupport)
        //    {
        //        helpEmail = ConfigurationManager.AppSettings["AppHelpDeskEmail"];
        //    }
        //    else if (ticket.SupportDepartment == StaticValues.STR_WebSiteSupport)
        //    {
        //        helpEmail = ConfigurationManager.AppSettings["WebHelpDeskEmail"];
        //    }
        //    else
        //    {
        //        helpEmail = ConfigurationManager.AppSettings["HelpDeskEmail"];
        //    }
        //    return helpEmail;
        //}
    }
}
