using System;
using System.Collections.Generic;
using HelpRequest.Controllers;
using HelpRequest.Controllers.Helpers;
using HelpRequest.Controllers.ViewModels;
using HelpRequest.Core.Domain;
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
        #region Submit Get Tests

        /// <summary>
        /// Tests the submit get returns expected view.
        /// </summary>
        [TestMethod]
        public void TestSubmitGetReturnsExpectedView1()
        {
            #region Arrange
            string appName = "MAAPS";
            #endregion Arrange

            #region Act
            var result = Controller.Submit(appName, "SubjectLine")
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.AppName);
            #endregion Assert		
        }

        /// <summary>
        /// Tests the submit get returns expected view.
        /// </summary>
        [TestMethod]
        public void TestSubmitGetReturnsExpectedView2()
        {
            #region Arrange
            string appName = null;
            #endregion Arrange

            #region Act
            var result = Controller.Submit(appName, "SubjectLine")
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.AppName);
            #endregion Assert
        }

        /// <summary>
        /// Tests the submit get returns expected view.
        /// </summary>
        [TestMethod]
        public void TestSubmitGetReturnsExpectedView3()
        {
            #region Arrange
            string appName = "MAAPS";
            #endregion Arrange

            #region Act
            var result = Controller.Submit(appName, "SubjectLine")
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Ticket);
            #endregion Assert
        }

        /// <summary>
        /// Tests the submit get returns expected view.
        /// </summary>
        [TestMethod]
        public void TestSubmitGetReturnsExpectedView4()
        {
            #region Arrange
            string appName = "MAAPS";
            #endregion Arrange

            #region Act
            var result = Controller.Submit(appName, "SubjectLine")
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
        /// Tests the submit get returns expected view.
        /// </summary>
        [TestMethod]
        public void TestSubmitGetReturnsExpectedView5()
        {
            #region Arrange
            string appName = "MAAPS";
            #endregion Arrange

            #region Act
            var result = Controller.Submit(appName, "SubjectLine")
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
        /// Tests the submit get returns expected view.
        /// </summary>
        [TestMethod]
        public void TestSubmitGetReturnsExpectedView6()
        {
            #region Arrange
            string appName = null;
            #endregion Arrange

            #region Act
            var result = Controller.Submit(appName, "SubjectLine")
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
        #endregion Submit Get Tests

        #region Submit Post Tests

        /// <summary>
        /// Tests the submit post redirects to public submit if email not found.
        /// </summary>
        [TestMethod]
        public void TestSubmitPostRedirectsToPublicSubmitIfEmailNotFound1()
        {
            #region Arrange
            string appName = "MAAPS";
            //Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            #endregion Arrange

            #region Act
            var result = Controller.Submit(new Ticket(), null, null, null, appName, "SubjectLine", null, null)
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.PublicSubmit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.AreEqual("Logged in email not found. Use this public submit instead.", Controller.Message);
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasNotCalled(a => a.Queryable);
            TicketControllerService.AssertWasNotCalled(a => a.FindKerbUser(Arg<string>.Is.Anything));
            #endregion Assert			
        }

        /// <summary>
        /// Tests the submit post redirects to public submit if email not found.
        /// </summary>
        [TestMethod]
        public void TestSubmitPostRedirectsToPublicSubmitIfEmailNotFound2()
        {
            #region Arrange
            string appName = null;
            //Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            #endregion Arrange

            #region Act
            var result = Controller.Submit(new Ticket(), null, null, null, appName, "SubjectLine", null, null)
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.PublicSubmit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.AreEqual("Logged in email not found. Use this public submit instead.", Controller.Message);
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasNotCalled(a => a.Queryable);
            TicketControllerService.AssertWasNotCalled(a => a.FindKerbUser(Arg<string>.Is.Anything));
            #endregion Assert
        }

        /// <summary>
        /// Tests the submit post redirects to public submit if email not found.
        /// </summary>
        [TestMethod]
        public void TestSubmitPostRedirectsToPublicSubmitIfEmailNotFound3()
        {
            #region Arrange
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ControllerRecordFakes.FakeUsers(3, UserRepository);
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(null).Repeat.Any();
            #endregion Arrange

            #region Act
            var result = Controller.Submit(new Ticket(), null, null, null, appName, "SubjectLine", null, null)
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.PublicSubmit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.AreEqual("Logged in email not found. Use this public submit instead.", Controller.Message);
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasCalled(a => a.Queryable);
            TicketControllerService.AssertWasCalled(a => a.FindKerbUser("UserName"));
            #endregion Assert
        }

        /// <summary>
        /// Tests the submit post redirects to public submit if email not found.
        /// </summary>
        [TestMethod]
        public void TestSubmitPostRedirectsToPublicSubmitIfEmailNotFound4()
        {
            #region Arrange
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = null;
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(null).Repeat.Any();
            #endregion Arrange

            #region Act
            var result = Controller.Submit(new Ticket(), null, null, null, appName, "SubjectLine", null, null)
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.PublicSubmit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.AreEqual("Logged in email not found. Use this public submit instead.", Controller.Message);
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasCalled(a => a.Queryable);
            TicketControllerService.AssertWasCalled(a => a.FindKerbUser("UserName"));
            #endregion Assert
        }

        /// <summary>
        /// Tests the submit post redirects to public submit if email not found.
        /// </summary>
        [TestMethod]
        public void TestSubmitPostRedirectsToPublicSubmitIfEmailNotFound5()
        {
            #region Arrange
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = string.Empty;
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(null).Repeat.Any();
            #endregion Arrange

            #region Act
            var result = Controller.Submit(new Ticket(), null, null, null, appName, "SubjectLine", null, null)
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.PublicSubmit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.AreEqual("Logged in email not found. Use this public submit instead.", Controller.Message);
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasCalled(a => a.Queryable);
            TicketControllerService.AssertWasCalled(a => a.FindKerbUser("UserName"));
            #endregion Assert
        }

        /// <summary>
        /// Tests the submit post redirects to public submit if email not found.
        /// </summary>
        [TestMethod]
        public void TestSubmitPostRedirectsToPublicSubmitIfEmailNotFound6()
        {
            #region Arrange
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = null;
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            var directory = new DirectoryUser();
            directory.EmailAddress = null;
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(directory).Repeat.Any();
            #endregion Arrange

            #region Act
            var result = Controller.Submit(new Ticket(), null, null, null, appName, "SubjectLine", null, null)
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.PublicSubmit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.AreEqual("Logged in email not found. Use this public submit instead.", Controller.Message);
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasCalled(a => a.Queryable);
            TicketControllerService.AssertWasCalled(a => a.FindKerbUser("UserName"));
            #endregion Assert
        }

        /// <summary>
        /// Tests the submit post redirects to public submit if email not found.
        /// </summary>
        [TestMethod]
        public void TestSubmitPostRedirectsToPublicSubmitIfEmailNotFound7()
        {
            #region Arrange
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = null;
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            var directory = new DirectoryUser();
            directory.EmailAddress = string.Empty;
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(directory).Repeat.Any();
            #endregion Arrange

            #region Act
            var result = Controller.Submit(new Ticket(), null, null, null, appName, "SubjectLine", null, null)
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.PublicSubmit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.AreEqual("Logged in email not found. Use this public submit instead.", Controller.Message);
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasCalled(a => a.Queryable);
            TicketControllerService.AssertWasCalled(a => a.FindKerbUser("UserName"));
            #endregion Assert
        }


        /// <summary>
        /// Tests the common validation checks is called.
        /// </summary>
        [TestMethod]
        public void TestSubmitWhenCommonValidationChecksIsCalled()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.Subject = null;
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = null;
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            var directory = new DirectoryUser();
            directory.EmailAddress = "kerb@test.edu";
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(directory).Repeat.Any();
            //TicketControllerService.Expect(a => a.CommonSubmitValidationChecks(Controller.ModelState, ticket, null, null, null, null));
            #endregion Arrange

            #region Act
            var result = Controller.Submit(ticket, null, null, null, appName, "SubjectLine", null, null)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            TicketControllerService.AssertWasCalled(a => a.CommonSubmitValidationChecks(Controller.ModelState, ticket, null, null, null, null));
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("Subject: You must enter the Subject.");
            Assert.IsNotNull(result);
            Assert.AreSame(ticket, result.Ticket);
            #endregion Assert		
        }

        /// <summary>
        /// Tests the common validation checks is called with expected parameters.
        /// </summary>
        [TestMethod]
        public void TestSubmitWhenCommonValidationChecksIsCalledWithExpectedParameters()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.Subject = null;
            var avDates = new[] {"Today", "Tomorrow"};
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = null;
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            var directory = new DirectoryUser();
            directory.EmailAddress = "kerb@test.edu";
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(directory).Repeat.Any();
            //TicketControllerService.Expect(a => a.CommonSubmitValidationChecks(Controller.ModelState, ticket, null, null, null, null));
            #endregion Arrange

            #region Act
            var result = Controller.Submit(ticket, avDates, emailCcs, upload, appName, "SubjectLine", availableDates, anotherEmailCc)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            TicketControllerService.AssertWasCalled(a => a.CommonSubmitValidationChecks(Controller.ModelState, ticket, avDates, emailCcs, availableDates, anotherEmailCc));
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("Subject: You must enter the Subject.");
            Assert.IsNotNull(result);
            Assert.AreSame(ticket, result.Ticket);
            #endregion Assert
        }

        /// <summary>
        /// Tests the load file contents is called.
        /// </summary>
        [TestMethod]
        public void TestSubmitWhenLoadFileContentsIsCalled()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.Subject = null;
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = null;
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            var directory = new DirectoryUser();
            directory.EmailAddress = "kerb@test.edu";
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(directory).Repeat.Any();
            #endregion Arrange

            #region Act
            var result = Controller.Submit(ticket, null, null, null, appName, "SubjectLine", null, null)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            TicketControllerService.AssertWasCalled(a => a.LoadFileContents(ticket, null));
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("Subject: You must enter the Subject.");
            Assert.IsNotNull(result);
            Assert.AreSame(ticket, result.Ticket);
            #endregion Assert
        }
        /// <summary>
        /// Tests the load file contents is called with expected parameters.
        /// </summary>
        [TestMethod]
        public void TestSubmitWhenLoadFileContentsIsCalledWithExpectedParameters()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.Subject = null;
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = null;
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            var directory = new DirectoryUser();
            directory.EmailAddress = "kerb@test.edu";
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(directory).Repeat.Any();
            #endregion Arrange

            #region Act
            var result = Controller.Submit(ticket, avDates, emailCcs, upload, appName, "SubjectLine", availableDates, anotherEmailCc)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            TicketControllerService.AssertWasCalled(a => a.LoadFileContents(ticket, upload));
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("Subject: You must enter the Subject.");
            Assert.IsNotNull(result);
            Assert.AreSame(ticket, result.Ticket);
            #endregion Assert
        }

        /// <summary>
        /// Tests the expected email is sent with valid values.
        /// </summary>
        [TestMethod]
        public void TestSubmitWhenExpectedEmailIsSentWithValidValues1()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = null;
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            var directory = new DirectoryUser();
            directory.EmailAddress = "kerb@test.edu";
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(directory).Repeat.Any();
            ticket.SupportDepartment = StaticValues.STR_ComputerSupport;
            #endregion Arrange

            #region Act
            var result = Controller.Submit(ticket, avDates, emailCcs, upload, appName, "SubjectLine", availableDates, anotherEmailCc)
                .AssertActionRedirect()
                .ToAction<HomeController>(a => a.Index(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.AreEqual("Your ticket has been successfully submitted, you will be receiving a confirmation email with a ticket # shortly (usually 5-10min).", Controller.Message);
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            //EmailProvider.AssertWasCalled(a => a.SendHelpRequest(ticket, true, "test1@ucdavis.edu"));
            TicketControllerService.AssertWasCalled(a => a.SendHelpRequest(ticket, true, EmailProvider));
            #endregion Assert		
        }

        /// <summary>
        /// Tests the expected email is sent with valid values.
        /// </summary>
        [TestMethod]
        public void TestSubmitWhenExpectedEmailIsSentWithValidValues2()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = "test@email.com";
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            //var directory = new DirectoryUser();
            //directory.EmailAddress = "kerb@test.edu";
            //TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(directory).Repeat.Any();
            ticket.SupportDepartment = StaticValues.STR_ComputerSupport;
            #endregion Arrange

            #region Act
            var result = Controller.Submit(ticket, avDates, emailCcs, upload, appName, "SubjectLine", availableDates, anotherEmailCc)
                .AssertActionRedirect()
                .ToAction<HomeController>(a => a.Index(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.AreEqual("Your ticket has been successfully submitted, you will be receiving a confirmation email with a ticket # shortly (usually 5-10min).", Controller.Message);
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            //EmailProvider.AssertWasCalled(a => a.SendHelpRequest(ticket, false, "test1@ucdavis.edu"));
            TicketControllerService.AssertWasCalled(a => a.SendHelpRequest(ticket, false, EmailProvider));
            #endregion Assert
        }

        /// <summary>
        /// Tests the expected email is sent with valid values.
        /// </summary>
        [TestMethod]
        public void TestSubmitWhenExpectedEmailIsSentWithValidValues3()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = null;
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            var directory = new DirectoryUser();
            directory.EmailAddress = "kerb@test.edu";
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(directory).Repeat.Any();
            ticket.SupportDepartment = "UnKnown";
            #endregion Arrange

            #region Act
            var result = Controller.Submit(ticket, avDates, emailCcs, upload, appName, "SubjectLine", availableDates, anotherEmailCc)
                .AssertActionRedirect()
                .ToAction<HomeController>(a => a.Index(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.AreEqual("Your ticket has been successfully submitted, you will be receiving a confirmation email with a ticket # shortly (usually 5-10min).", Controller.Message);
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            //EmailProvider.AssertWasCalled(a => a.SendHelpRequest(ticket, true, "test1@ucdavis.edu"));
            TicketControllerService.AssertWasCalled(a => a.SendHelpRequest(ticket, true, EmailProvider));
            #endregion Assert
        }

        /// <summary>
        /// Tests the expected email is sent with valid values.
        /// </summary>
        [TestMethod]
        public void TestSubmitWhenExpectedEmailIsSentWithValidValues4()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = null;
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            var directory = new DirectoryUser();
            directory.EmailAddress = "kerb@test.edu";
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(directory).Repeat.Any();
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            #endregion Arrange

            #region Act
            var result = Controller.Submit(ticket, avDates, emailCcs, upload, appName, "SubjectLine", availableDates, anotherEmailCc)
                .AssertActionRedirect()
                .ToAction<HomeController>(a => a.Index(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.AreEqual("Your ticket has been successfully submitted, you will be receiving a confirmation email with a ticket # shortly (usually 5-10min).", Controller.Message);
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            //EmailProvider.AssertWasCalled(a => a.SendHelpRequest(ticket, true, "test3@ucdavis.edu"));
            TicketControllerService.AssertWasCalled(a => a.SendHelpRequest(ticket, true, EmailProvider));
            #endregion Assert
        }

        /// <summary>
        /// Tests the expected email is sent with valid values.
        /// </summary>
        [TestMethod]
        public void TestSubmitWhenExpectedEmailIsSentWithValidValues5()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = null;
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            var directory = new DirectoryUser();
            directory.EmailAddress = "kerb@test.edu";
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(directory).Repeat.Any();
            ticket.SupportDepartment = StaticValues.STR_ProgrammingSupport;
            #endregion Arrange

            #region Act
            var result = Controller.Submit(ticket, avDates, emailCcs, upload, appName, "SubjectLine", availableDates, anotherEmailCc)
                .AssertActionRedirect()
                .ToAction<HomeController>(a => a.Index(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.AreEqual("Your ticket has been successfully submitted, you will be receiving a confirmation email with a ticket # shortly (usually 5-10min).", Controller.Message);
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            //EmailProvider.AssertWasCalled(a => a.SendHelpRequest(ticket, true, "test2@ucdavis.edu"));
            TicketControllerService.AssertWasCalled(a => a.SendHelpRequest(ticket, true, EmailProvider));
            #endregion Assert
        }


        /// <summary>
        /// Tests the submit when email throws an exception.
        /// </summary>
        [TestMethod]
        public void TestSubmitWhenEmailThrowsAnException()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new[] { "Today", "Tomorrow" };
            var emailCcs = new[] { "test@testy.com", "test@tester.edu" };
            var upload = new MockHttpPostedFileBase();
            var availableDates = "Hah";
            var anotherEmailCc = "test@icle.com";
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = null;
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            var directory = new DirectoryUser();
            directory.EmailAddress = "kerb@test.edu";
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(directory).Repeat.Any();
            ticket.SupportDepartment = StaticValues.STR_ProgrammingSupport;

            //EmailProvider.Expect(a => a.SendHelpRequest(
            //    Arg<Ticket>.Is.Anything, 
            //    Arg<bool>.Is.Anything, 
            //    Arg<string>.Is.Anything))
            //    .Throw(new Exception("Oops, my bad."));
            TicketControllerService.Expect(a => a.SendHelpRequest(ticket, true, EmailProvider)).Throw(new Exception("Oops, my bad.")).Repeat.Any();
            #endregion Arrange

            #region Act
            var result = Controller.Submit(ticket, avDates, emailCcs, upload, appName, "SubjectLine", availableDates, anotherEmailCc)
                .AssertViewRendered()
                .WithViewData<TicketViewModel>();
            #endregion Act

            #region Assert
            TicketControllerService.AssertWasCalled(a => a.LoadFileContents(ticket, upload));
            Assert.IsFalse(Controller.ModelState.IsValid);
            Controller.ModelState.AssertErrorsAre("Application Exception sending email: Oops, my bad.");
            Assert.IsNotNull(result);
            Assert.AreSame(ticket, result.Ticket);
            #endregion Assert		
        }
        #endregion Submit Post Tests
    }

}
