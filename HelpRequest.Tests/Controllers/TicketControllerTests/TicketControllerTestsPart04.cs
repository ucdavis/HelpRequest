using System;
using HelpRequest.Controllers;
using HelpRequest.Controllers.ViewModels;
using HelpRequest.Core.Resources;
using HelpRequest.Tests.Core.Extensions;
using HelpRequest.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Rhino.Mocks;

namespace HelpRequest.Tests.Controllers.TicketControllerTests
{
    public partial class TicketControllerTests
    {
        #region PublicSubmit Get Tests        

        /// <summary>
        /// Tests the public submit get returns view.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitGetReturnsView1()
        {
            #region Arrange
            string appName = null;
            #endregion Arrange

            #region Act
            var result = Controller.PublicSubmit(appName)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.AppName);
            #endregion Assert		
        }

        /// <summary>
        /// Tests the public submit get returns view.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitGetReturnsView2()
        {
            #region Arrange
            string appName = "MAAPS";
            #endregion Arrange

            #region Act
            var result = Controller.PublicSubmit(appName)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("MAAPS", result.AppName);
            #endregion Assert
        }

        /// <summary>
        /// Tests the public submit get returns view.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitGetReturnsView3()
        {
            #region Arrange
            string appName = "MAAPS";
            #endregion Arrange

            #region Act
            var result = Controller.PublicSubmit(appName)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("MAAPS", result.AppName);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Ticket);
            #endregion Assert
        }

                /// <summary>
        /// Tests the public submit get returns view.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitGetReturnsView4()
        {
            #region Arrange
            string appName = null;
            #endregion Arrange

            #region Act
            var result = Controller.PublicSubmit(appName)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Urgency.Count);
            Assert.IsTrue(result.Urgency.Contains(StaticValues.STR_UrgencyLevelCritical));
            Assert.IsTrue(result.Urgency.Contains(StaticValues.STR_UrgencyLevelNonCritical));
            Assert.IsTrue(result.Urgency.Contains(StaticValues.STR_UrgencyLevelScheduledRequests));
            Assert.IsTrue(result.Urgency.Contains(StaticValues.STR_UrgencyLevelWorkaroundAvailable));
            Assert.IsTrue(result.Urgency.Contains(StaticValues.STR_UrgencyLevelWorkStoppage));
            #endregion Assert
        }

        /// <summary>
        /// Tests the public submit get returns view.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitGetReturnsView5()
        {
            #region Arrange
            string appName = "MAAPS";
            #endregion Arrange

            #region Act
            var result = Controller.PublicSubmit(appName)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.SupportDepartment.Count);
            Assert.IsTrue(result.SupportDepartment.Contains(StaticValues.STR_ProgrammingSupport));
            #endregion Assert
        }

        /// <summary>
        /// Tests the public submit get returns view.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitGetReturnsView6()
        {
            #region Arrange
            string appName = string.Empty;
            #endregion Arrange

            #region Act
            var result = Controller.PublicSubmit(appName)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.SupportDepartment.Count);
            Assert.IsTrue(result.SupportDepartment.Contains(StaticValues.STR_ProgrammingSupport));
            Assert.IsTrue(result.SupportDepartment.Contains(StaticValues.STR_WebSiteSupport));
            Assert.IsTrue(result.SupportDepartment.Contains(StaticValues.STR_ComputerSupport));
            #endregion Assert
        }
        #endregion PublicSubmit Get Tests

        #region PublicSubmit Post Tests

        /// <summary>
        /// Tests the public submit has correct email reg ex.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitHasCorrectEmailRegEx()
        {
            #region Assert
            Assert.AreEqual(@"(^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$){1}|^$", StaticValues.EmailErrorRegEx);
            #endregion Assert		
        }


        /// <summary>
        /// Tests the public submit with invalid re captcha returns view.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitWithInvalidReCaptchaReturnsView()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = "MAAPS";
            ticket.FromEmail = "test@ucdavis.edu";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            #endregion Arrange

            #region Act
            var result = Controller
                .PublicSubmit(ticket, avDates, emailCcs, upload, false, appName, availableDates, anotherEmailCc)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.AppName);
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("Captcha values are not valid.");
            #endregion Assert		
        }

        /// <summary>
        /// Tests the public submit with null from email returns view.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitWithNullFromEmailReturnsView()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = null;
            ticket.FromEmail = null;
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            #endregion Arrange

            #region Act
            var result = Controller
                .PublicSubmit(ticket, avDates, emailCcs, upload, true, appName, availableDates, anotherEmailCc)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.AppName);
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("Your email address is required.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the public submit with empty string from email returns view.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitWithEmptyStringFromEmailReturnsView()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = null;
            ticket.FromEmail = string.Empty;
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            #endregion Arrange

            #region Act
            var result = Controller
                .PublicSubmit(ticket, avDates, emailCcs, upload, true, appName, availableDates, anotherEmailCc)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.AppName);
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("Your email address is required.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the public submit with spaces only string from email returns view.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitWithSpacesOnlyStringFromEmailReturnsView()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = null;
            ticket.FromEmail = " ";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            #endregion Arrange

            #region Act
            var result = Controller
                .PublicSubmit(ticket, avDates, emailCcs, upload, true, appName, availableDates, anotherEmailCc)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.AppName);
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("Your email address is required.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the public submit with invalid from email returns view.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitWithInvalidFromEmailReturnsView()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = null;
            ticket.FromEmail = "test@test@test.edu";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            #endregion Arrange

            #region Act
            var result = Controller
                .PublicSubmit(ticket, avDates, emailCcs, upload, true, appName, availableDates, anotherEmailCc)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.AppName);
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("A valid email address is required.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the public submit calls common validation checks.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitCallsCommonValidationChecks()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = null;
            ticket.FromEmail = "test@test@test.edu";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            #endregion Arrange

            #region Act
            var result = Controller
                .PublicSubmit(ticket, avDates, emailCcs, upload, true, appName, availableDates, anotherEmailCc)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            TicketControllerService
                .AssertWasCalled(a => a.CommonSubmitValidationChecks(
                    Controller.ModelState, 
                    ticket, 
                    avDates, 
                    emailCcs, 
                    availableDates, 
                    anotherEmailCc));
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.AppName);
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("A valid email address is required.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the public submit calls load file contents.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitCallsLoadFileContents1()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = null;
            ticket.FromEmail = "test@test@test.edu";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            #endregion Arrange

            #region Act
            var result = Controller
                .PublicSubmit(ticket, avDates, emailCcs, upload, true, appName, availableDates, anotherEmailCc)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            TicketControllerService
                .AssertWasCalled(a => a.LoadFileContents(ticket, upload));
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.AppName);
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("A valid email address is required.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the public submit calls load file contents.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitCallsLoadFileContents2()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            //var upload = null;// new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = null;
            ticket.FromEmail = "test@test@test.edu";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            #endregion Arrange

            #region Act
            var result = Controller
                .PublicSubmit(ticket, avDates, emailCcs, null, true, appName, availableDates, anotherEmailCc)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            TicketControllerService
                .AssertWasCalled(a => a.LoadFileContents(ticket, null));
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.AppName);
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("A valid email address is required.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the public submit returns view when invalid ticket.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitReturnsViewWhenInvalidTicket()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = null;
            ticket.FromEmail = "test@test@test.edu";
            ticket.Subject = null;
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            #endregion Arrange

            #region Act
            var result = Controller
                .PublicSubmit(ticket, avDates, emailCcs, upload, true, appName, availableDates, anotherEmailCc)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.AppName);
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("A valid email address is required.", "Subject: You must enter the Subject.");
            Assert.AreSame(ticket, result.Ticket);
            #endregion Assert
        }


        /// <summary>
        /// Tests the send help request is called when valid ticket.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitSendHelpRequestIsCalledWhenValidTicket1()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = null;
            ticket.FromEmail = "test@test.edu";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            #endregion Arrange

            #region Act
            var result = Controller
                .PublicSubmit(ticket, avDates, emailCcs, upload, true, appName, availableDates, anotherEmailCc)
                .AssertActionRedirect()
                .ToAction<HomeController>(a => a.Index(appName));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            //EmailProvider.AssertWasCalled(a => a.SendHelpRequest(ticket, true, "test3@ucdavis.edu"));
            TicketControllerService.AssertWasCalled(a => a.SendHelpRequest(ticket, true, EmailProvider));
            #endregion Assert		
        }

        /// <summary>
        /// Tests the send help request is called when valid ticket.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitSendHelpRequestIsCalledWhenValidTicket2()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = null;
            ticket.FromEmail = "test@test.edu";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ticket.SupportDepartment = StaticValues.STR_ComputerSupport;
            #endregion Arrange

            #region Act
            var result = Controller
                .PublicSubmit(ticket, avDates, emailCcs, upload, true, appName, availableDates, anotherEmailCc)
                .AssertActionRedirect()
                .ToAction<HomeController>(a => a.Index(appName));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            //EmailProvider.AssertWasCalled(a => a.SendHelpRequest(ticket, true, "test1@ucdavis.edu"));
            TicketControllerService.AssertWasCalled(a => a.SendHelpRequest(ticket, true, EmailProvider));
            #endregion Assert
        }

        /// <summary>
        /// Tests the send help request is called when valid ticket.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitSendHelpRequestIsCalledWhenValidTicket3()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = "MAAPS";
            ticket.FromEmail = "test@test.edu";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ticket.SupportDepartment = StaticValues.STR_ProgrammingSupport;
            #endregion Arrange

            #region Act
            var result = Controller
                .PublicSubmit(ticket, avDates, emailCcs, upload, true, appName, availableDates, anotherEmailCc)
                .AssertActionRedirect()
                .ToAction<HomeController>(a => a.Index(appName));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            //EmailProvider.AssertWasCalled(a => a.SendHelpRequest(ticket, true, "test2@ucdavis.edu"));
            TicketControllerService.AssertWasCalled(a => a.SendHelpRequest(ticket, true, EmailProvider));
            #endregion Assert
        }

        /// <summary>
        /// Tests the send help request is called when valid ticket.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitSendHelpRequestIsCalledWhenValidTicket4()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = null;
            ticket.FromEmail = "test@test.edu";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ticket.SupportDepartment = "Huh?";
            #endregion Arrange

            #region Act
            var result = Controller
                .PublicSubmit(ticket, avDates, emailCcs, upload, true, appName, availableDates, anotherEmailCc)
                .AssertActionRedirect()
                .ToAction<HomeController>(a => a.Index(appName));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            //EmailProvider.AssertWasCalled(a => a.SendHelpRequest(ticket, true, "test1@ucdavis.edu"));
            TicketControllerService.AssertWasCalled(a => a.SendHelpRequest(ticket, true, EmailProvider));
            #endregion Assert
        }


        /// <summary>
        /// Tests the public submit returns view when email throws exception.
        /// </summary>
        [TestMethod]
        public void TestPublicSubmitReturnsViewWhenEmailThrowsException()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = null;
            ticket.FromEmail = "test@test.edu";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ticket.SupportDepartment = "Huh?";
            //EmailProvider
            //    .Expect(a => a.SendHelpRequest(
            //        ticket, 
            //        true, 
            //        "test1@ucdavis.edu"))
            //            .Throw(new Exception("Opps, another exception.")).Repeat.Any();
            TicketControllerService.Expect(a => a.SendHelpRequest(ticket, true, EmailProvider)).Throw(new Exception("Opps, another exception.")).Repeat.Any();            
            #endregion Arrange

            #region Act
            var result = Controller
                .PublicSubmit(ticket, avDates, emailCcs, upload, true, appName, availableDates, anotherEmailCc)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.AppName);
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("Application Exception sending email: Opps, another exception.");
            Assert.AreSame(ticket, result.Ticket);
            #endregion Assert		
        }
        #endregion PublicSubmit Post Tests
    }
}
