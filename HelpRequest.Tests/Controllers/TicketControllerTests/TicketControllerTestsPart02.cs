using System.Collections.Generic;
using HelpRequest.Controllers;
using HelpRequest.Controllers.Helpers;
using HelpRequest.Core.Domain;
using HelpRequest.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Rhino.Mocks;

namespace HelpRequest.Tests.Controllers.TicketControllerTests
{
    public partial class TicketControllerTests
    {
        #region LogOnAndSubmit Tests

        /// <summary>
        /// Tests the log on and submit redirects to submit redirection.
        /// </summary>
        [TestMethod]
        public void TestLogOnAndSubmitRedirectsToSubmitRedirection1()
        {
            #region Arrange
            string appName = "MAAPS";
            #endregion Arrange

            #region Act
            var result = Controller.LogOnAndSubmit(appName, "SubjectLine")
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.SubmitRedirect(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            TicketControllerService.AssertWasCalled(a => a.CasLogin());
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            #endregion Assert
        }

        /// <summary>
        /// Tests the log on and submit redirects to submit redirection.
        /// </summary>
        [TestMethod]
        public void TestLogOnAndSubmitRedirectsToSubmitRedirection2()
        {
            #region Arrange
            string appName = null;
            #endregion Arrange

            #region Act
            var result = Controller.LogOnAndSubmit(appName, "SubjectLine")
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.SubmitRedirect(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            TicketControllerService.AssertWasCalled(a => a.CasLogin());
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            #endregion Assert
        }

        #endregion LogOnAndSubmit Tests

        #region SubmitRedirect Tests


        /// <summary>
        /// Tests the submit redirect redirects to public submit when current user is null.
        /// </summary>
        [TestMethod]
        public void TestSubmitRedirectRedirectsToPublicSubmitWhenCurrentUserIsNull()
        {
            #region Arrange
            string appName = "MAAPS";
            //Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            #endregion Arrange

            #region Act
            var result = Controller.SubmitRedirect(appName, "SubjectLine")
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.PublicSubmit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasNotCalled(a => a.Queryable);
            TicketControllerService.AssertWasNotCalled(a => a.FindKerbUser(Arg<string>.Is.Anything));
            #endregion Assert		
        }


        /// <summary>
        /// Tests the submit redirect redirects to public submit when no email is found.
        /// </summary>
        [TestMethod]
        public void TestSubmitRedirectRedirectsToPublicSubmitWhenNoEmailIsFound1()
        {
            #region Arrange
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            ControllerRecordFakes.FakeUsers(3, UserRepository);
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(null).Repeat.Any();
            #endregion Arrange

            #region Act
            var result = Controller.SubmitRedirect(appName, "SubjectLine")
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.PublicSubmit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasCalled(a => a.Queryable);
            TicketControllerService.AssertWasCalled(a => a.FindKerbUser("UserName"));
            #endregion Assert		
        }

        /// <summary>
        /// Tests the submit redirect redirects to public submit when no email is found.
        /// </summary>
        [TestMethod]
        public void TestSubmitRedirectRedirectsToPublicSubmitWhenNoEmailIsFound2()
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
            var result = Controller.SubmitRedirect(appName, "SubjectLine")
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.PublicSubmit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasCalled(a => a.Queryable);
            TicketControllerService.AssertWasCalled(a => a.FindKerbUser("UserName"));
            #endregion Assert
        }

        /// <summary>
        /// Tests the submit redirect redirects to public submit when no email is found.
        /// </summary>
        [TestMethod]
        public void TestSubmitRedirectRedirectsToPublicSubmitWhenNoEmailIsFound3()
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
            var result = Controller.SubmitRedirect(appName, "SubjectLine")
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.PublicSubmit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasCalled(a => a.Queryable);
            TicketControllerService.AssertWasCalled(a => a.FindKerbUser("UserName"));
            #endregion Assert
        }

        /// <summary>
        /// Tests the submit redirect redirects to public submit when no email is found.
        /// </summary>
        [TestMethod]
        public void TestSubmitRedirectRedirectsToPublicSubmitWhenNoEmailIsFound4()
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
            var result = Controller.SubmitRedirect(appName, "SubjectLine")
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.PublicSubmit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasCalled(a => a.Queryable);
            TicketControllerService.AssertWasCalled(a => a.FindKerbUser("UserName"));
            #endregion Assert
        }

        /// <summary>
        /// Tests the submit redirect redirects to public submit when no email is found.
        /// </summary>
        [TestMethod]
        public void TestSubmitRedirectRedirectsToPublicSubmitWhenNoEmailIsFound5()
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
            var result = Controller.SubmitRedirect(appName, "SubjectLine")
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.PublicSubmit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasCalled(a => a.Queryable);
            TicketControllerService.AssertWasCalled(a => a.FindKerbUser("UserName"));
            #endregion Assert
        }

        /// <summary>
        /// Tests the submit redirect redirects to submit when user email is found.
        /// </summary>
        [TestMethod]
        public void TestSubmitRedirectRedirectsToSubmitWhenUserEmailIsFound()
        {
            #region Arrange
            string appName = "MAAPS";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var users = new List<User>();
            users.Add(CreateValidEntities.User(9));
            users[0].LoginId = "UserName";
            users[0].Email = "found@test.edu";
            ControllerRecordFakes.FakeUsers(3, UserRepository, users);
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(null).Repeat.Any();
            #endregion Arrange

            #region Act
            var result = Controller.SubmitRedirect(appName, "SubjectLine")
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.Submit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasCalled(a => a.Queryable);
            TicketControllerService.AssertWasNotCalled(a => a.FindKerbUser(Arg<string>.Is.Anything));
            #endregion Assert
        }

        /// <summary>
        /// Tests the submit redirect redirects to submit when user email is not found but kerb is.
        /// </summary>
        [TestMethod]
        public void TestSubmitRedirectRedirectsToSubmitWhenUserEmailIsNotFoundButKerbIs()
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
            directory.EmailAddress = "kerb@test.edu";
            TicketControllerService.Expect(a => a.FindKerbUser("UserName")).Return(directory).Repeat.Any();
            #endregion Arrange

            #region Act
            var result = Controller.SubmitRedirect(appName, "SubjectLine")
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.Submit(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            UserRepository.AssertWasCalled(a => a.Queryable);
            TicketControllerService.AssertWasCalled(a => a.FindKerbUser("UserName"));
            #endregion Assert
        }

        #endregion SubmitRedirect Tests
    }
}
