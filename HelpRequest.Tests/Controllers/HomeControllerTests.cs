using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HelpRequest.Controllers;
using HelpRequest.Controllers.Filters;
using HelpRequest.Controllers.ViewModels;
using HelpRequest.Core.Domain;
using HelpRequest.Tests.Core.Extensions;
using HelpRequest.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Testing;
using UCDArch.Web.Attributes;

namespace HelpRequest.Tests.Controllers
{
    /// <summary>
    /// Home Controller Tests
    /// </summary>
    [TestClass]
    public class HomeControllerTests : ControllerTestBase<HomeController>
    {
        private readonly Type _controllerClass = typeof(HomeController);
        public IRepository<CatbertApplication> CatbertApplicationRepository;
        
        #region Init
        /// <summary>
        /// Setups the controller.
        /// </summary>
        protected override void SetupController()
        {
            CatbertApplicationRepository = FakeRepository<CatbertApplication>();
            Controller = new TestControllerBuilder().CreateController<HomeController>(CatbertApplicationRepository);
        }
        /// <summary>
        /// Registers the routes.
        /// </summary>
        protected override void RegisterRoutes()
        {
            new RouteConfigurator().RegisterRoutes();
        }

        #endregion Init

        #region Mapping Tests

        /// <summary>
        /// Tests the index mapping.
        /// </summary>
        [TestMethod]
        public void TestIndexMapping()
        {
            "~/Home/Index/?appName=Test".ShouldMapTo<HomeController>(a => a.Index("Test", "SubjectLine"), true);
        }

        /// <summary>
        /// Tests the about mapping.
        /// </summary>
        [TestMethod]
        public void TestAboutMapping()
        {
            "~/Home/About/?appName=Test".ShouldMapTo<HomeController>(a => a.About("Test", null), true);
        }

        /// <summary>
        /// Tests the return to calling application mapping.
        /// </summary>
        [TestMethod]
        public void TestReturnToCallingApplicationMapping()
        {
            "~/Home/ReturnToCallingApplication/?url=Test".ShouldMapTo<HomeController>(a => a.ReturnToCallingApplication("Test"), true);
        }

        [TestMethod]
        public void TestAuthorizedHomeMapping()
        {
            "~/Home/AuthorizedHome/?appName=Test".ShouldMapTo<HomeController>(a => a.AuthorizedHome("Test", "SubjectLine"), true);
        }

        [TestMethod]
        public void TestResetCacheMapping()
        {
            "~/Home/ResetCache/".ShouldMapTo<HomeController>(a => a.ResetCache());
        }

        #endregion Mapping Tests

        #region Index Tests

        /// <summary>
        /// Tests the index returns view.
        /// </summary>
        [TestMethod]
        public void TestIndexReturnsView()
        {
            Controller.Index(null, null)
                .AssertViewRendered()
                .WithViewData<HomeViewModel>();
        }


        /// <summary>
        /// Tests the index returns view with expected values.
        /// </summary>
        [TestMethod]
        public void TestIndexReturnsViewWithExpectedValues1()
        {
            #region Arrange
            var catBertApplications = new List<CatbertApplication>();
            catBertApplications.Add(CreateValidEntities.CatbertApplication(1));
            catBertApplications.Add(CreateValidEntities.CatbertApplication(2));
            catBertApplications.Add(CreateValidEntities.CatbertApplication(3));
            ControllerRecordFakes.FakeCatbert(0, CatbertApplicationRepository, catBertApplications);
            #endregion Arrange

            #region Act
            var result = Controller.Index("ABBR2", "SubjectLine")
                .AssertViewRendered()
                .WithViewData<HomeViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Location2", result.ReturnUrl);
            Assert.AreEqual("Name2", result.ReturnAppName);
            #endregion Assert		
        }

        /// <summary>
        /// Tests the index returns view with expected values.
        /// </summary>
        [TestMethod]
        public void TestIndexReturnsViewWithExpectedValues2()
        {
            #region Arrange
            var catBertApplications = new List<CatbertApplication>();
            catBertApplications.Add(CreateValidEntities.CatbertApplication(1));
            catBertApplications.Add(CreateValidEntities.CatbertApplication(2));
            catBertApplications.Add(CreateValidEntities.CatbertApplication(3));
            ControllerRecordFakes.FakeCatbert(0, CatbertApplicationRepository, catBertApplications);
            #endregion Arrange

            #region Act
            var result = Controller.Index(null, null)
                .AssertViewRendered()
                .WithViewData<HomeViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(null, result.ReturnUrl);
            Assert.AreEqual(null, result.ReturnAppName);
            #endregion Assert
        }

        /// <summary>
        /// Tests the index returns view with expected values.
        /// </summary>
        [TestMethod]
        public void TestIndexReturnsViewWithExpectedValues3()
        {
            #region Arrange
            var catBertApplications = new List<CatbertApplication>();
            catBertApplications.Add(CreateValidEntities.CatbertApplication(1));
            catBertApplications.Add(CreateValidEntities.CatbertApplication(2));
            catBertApplications.Add(CreateValidEntities.CatbertApplication(3));
            ControllerRecordFakes.FakeCatbert(0, CatbertApplicationRepository, catBertApplications);
            #endregion Arrange

            #region Act
            var result = Controller.Index("Not Found", "SubjectLine")
                .AssertViewRendered()
                .WithViewData<HomeViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(null, result.ReturnUrl);
            Assert.AreEqual(null, result.ReturnAppName);
            #endregion Assert
        }

        /// <summary>
        /// Tests the index returns view with expected values.
        /// </summary>
        [TestMethod]
        public void TestIndexReturnsViewWithExpectedValues4()
        {
            #region Arrange
            var catBertApplications = new List<CatbertApplication>();
            catBertApplications.Add(CreateValidEntities.CatbertApplication(1));
            catBertApplications.Add(CreateValidEntities.CatbertApplication(2));
            catBertApplications[1].Location = null;
            catBertApplications.Add(CreateValidEntities.CatbertApplication(3));
            ControllerRecordFakes.FakeCatbert(0, CatbertApplicationRepository, catBertApplications);
            #endregion Arrange

            #region Act
            var result = Controller.Index("ABBR2", "SubjectLine")
                .AssertViewRendered()
                .WithViewData<HomeViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(null, result.ReturnUrl);
            Assert.AreEqual(null, result.ReturnAppName);
            Assert.AreEqual("ABBR2", result.AppName);
            #endregion Assert
        }


        /// <summary>
        /// Tests the index has expected message.
        /// </summary>
        [TestMethod]
        public void TestIndexHasExpectedMessage1()
        {
            #region Arrange
            ControllerRecordFakes.FakeCatbert(1, CatbertApplicationRepository);
            #endregion Arrange

            #region Act
            Controller.Index("ABBR2", "SubjectLine");
            #endregion Act

            #region Assert
            Assert.IsNull(Controller.Message);
            Assert.AreEqual("Welcome to the Help Request submission form home page for ABBR2.", Controller.ViewData["Message"]);
            #endregion Assert		
        }

        /// <summary>
        /// Tests the index has expected message.
        /// </summary>
        [TestMethod]
        public void TestIndexHasExpectedMessage2()
        {
            #region Arrange
            ControllerRecordFakes.FakeCatbert(1, CatbertApplicationRepository);
            #endregion Arrange

            #region Act
            Controller.Index(string.Empty, "");
            #endregion Act

            #region Assert
            Assert.IsNull(Controller.Message);
            Assert.AreEqual("Welcome to the Help Request submission form home page.", Controller.ViewData["Message"]);
            #endregion Assert
        }

        /// <summary>
        /// Tests the index has expected message.
        /// </summary>
        [TestMethod]
        public void TestIndexHasExpectedMessage3()
        {
            #region Arrange
            ControllerRecordFakes.FakeCatbert(1, CatbertApplicationRepository);
            #endregion Arrange

            #region Act
            Controller.Index(null, null);
            #endregion Act

            #region Assert
            Assert.IsNull(Controller.Message);
            Assert.AreEqual("Welcome to the Help Request submission form home page.", Controller.ViewData["Message"]);
            #endregion Assert
        }

        /// <summary>
        /// Tests the index has expected message.
        /// </summary>
        [TestMethod]
        public void TestIndexHasExpectedMessage4()
        {
            #region Arrange
            ControllerRecordFakes.FakeCatbert(1, CatbertApplicationRepository);
            #endregion Arrange

            #region Act
            Controller.Index("  ", " ");
            #endregion Act

            #region Assert
            Assert.IsNull(Controller.Message);
            Assert.AreEqual("Welcome to the Help Request submission form home page for   .", Controller.ViewData["Message"]);
            #endregion Assert
        }
        #endregion Index Tests

        #region About Tests

        /// <summary>
        /// Tests the about returns generic view model.
        /// </summary>
        [TestMethod]
        public void TestAboutReturnsGenericViewModel1()
        {
            #region Arrange
            string appName = "TestApp";
            #endregion Arrange

            #region Act
            var result = Controller.About(appName, null)
                .AssertViewRendered()
                .WithViewData<GenericViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.AppName);
            Assert.IsNull(result.CallingPage);
            #endregion Assert		
        }

        /// <summary>
        /// Tests the about returns generic view model.
        /// </summary>
        [TestMethod]
        public void TestAboutReturnsGenericViewModel2()
        {
            #region Arrange
            string appName = null;
            #endregion Arrange

            #region Act
            var result = Controller.About(appName, "")
                .AssertViewRendered()
                .WithViewData<GenericViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.AppName);
            Assert.IsNull(result.CallingPage);
            #endregion Assert
        }
        #endregion About Tests

        #region ReturnToCallingApplication Tests

        /// <summary>
        /// Tests the return to calling application redirects to passed URL.
        /// </summary>
        [TestMethod]
        public void TestReturnToCallingApplicationRedirectsToPassedUrl()
        {
            #region Arrange
            var url = "http://www.google.com";            
            #endregion Arrange

            #region Act
            var result = Controller.ReturnToCallingApplication(url)
                .AssertHttpRedirect();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(url, result.Url);
            #endregion Assert		
        }

        #endregion ReturnToCallingApplication Tests

        #region AuthorizedHome Tests

        [TestMethod]
        public void TestAuthorizedHomeWithoutAppNameRedirectsToIndex()
        {
            #region Arrange
            string appName = null;
            #endregion Arrange

            #region Act
            var result = Controller.AuthorizedHome(appName, "SubjectLine")
                .AssertActionRedirect()
                .ToAction<HomeController>(a => a.Index(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            #endregion Assert		
        }

        [TestMethod]
        public void TestAuthorizedHomeWithAppNameRedirectsToIndex()
        {
            #region Arrange
            string appName = "MAAPS";
            #endregion Arrange

            #region Act
            var result = Controller.AuthorizedHome(appName, "SubjectLine")
                .AssertActionRedirect()
                .ToAction<HomeController>(a => a.Index(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            #endregion Assert
        }

        [TestMethod]
        public void TestAuthorizedHomeWithExtraAppNameInfoRedirectsToIndex()
        {
            #region Arrange
            string appName = "?appName=STD appName=STD";
            #endregion Arrange

            #region Act
            var result = Controller.AuthorizedHome(appName, "SubjectLine")
                .AssertActionRedirect()
                .ToAction<HomeController>(a => a.Index(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("STD", result.RouteValues["appName"]);
            #endregion Assert
        }

        [TestMethod]
        public void TestAuthorizedHomeWithExtraAppNameInfoRedirectsToIndex2()
        {
            #region Arrange
            string appName = "?appName=";
            #endregion Arrange

            #region Act
            var result = Controller.AuthorizedHome(appName, "SubjectLine")
                .AssertActionRedirect()
                .ToAction<HomeController>(a => a.Index(appName, "SubjectLine"));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.RouteValues["appName"]);
            #endregion Assert
        }
        #endregion AuthorizedHome Tests


        #region Reflection
        #region Controller Class Tests

        /// <summary>
        /// Tests the controller inherits from super controller.
        /// </summary>
        [TestMethod]
        public void TestControllerInheritsFromApplicationController()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            #endregion Arrange

            #region Act
            Assert.IsNotNull(controllerClass.BaseType);
            var result = controllerClass.BaseType.Name;
            #endregion Act

            #region Assert
            Assert.AreEqual("ApplicationController", result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the controller has only 3 attributes.
        /// </summary>
        [TestMethod]
        public void TestControllerHasOnlyThreeAttributes()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            #endregion Arrange

            #region Act
            var result = controllerClass.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(4, result.Count());
            #endregion Assert
        }

        /// <summary>
        /// Tests the controller has transaction attribute.
        /// </summary>
        [TestMethod]
        public void TestControllerHasTransactionAttribute()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            #endregion Arrange

            #region Act
            var result = controllerClass.GetCustomAttributes(true).OfType<UseTransactionsByDefaultAttribute>();
            #endregion Act

            #region Assert
            Assert.IsTrue(result.Count() > 0, "UseTransactionsByDefaultAttribute not found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the controller has anti forgery token attribute.
        /// </summary>
        [TestMethod]
        public void TestControllerHasAntiForgeryTokenAttribute()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            #endregion Arrange

            #region Act
            var result = controllerClass.GetCustomAttributes(true).OfType<UseAntiForgeryTokenOnPostByDefault>();
            #endregion Act

            #region Assert
            Assert.IsTrue(result.Count() > 0, "UseAntiForgeryTokenOnPostByDefault not found.");
            #endregion Assert
        }

        [TestMethod]
        public void TestControllerHasVersionAttribute()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            #endregion Arrange

            #region Act
            var result = controllerClass.GetCustomAttributes(true).OfType<VersionAttribute>();
            #endregion Act

            #region Assert
            Assert.IsTrue(result.Count() > 0, "VersionAttribute not found.");
            #endregion Assert
        }
        #endregion Controller Class Tests

        #region Controller Method Tests

        /// <summary>
        /// Tests the controller contains expected number of public methods.
        /// </summary>
        [TestMethod]
        public void TestControllerContainsExpectedNumberOfPublicMethods()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            #endregion Arrange

            #region Act
            var result = controllerClass.GetMethods().Where(a => a.DeclaringType == controllerClass);
            #endregion Act

            #region Assert
            Assert.AreEqual(5, result.Count(), "It looks like a method was added or removed from the controller.");
            #endregion Assert
        }


        /// <summary>
        /// Tests the controller method index contains expected attributes.
        /// #1
        /// </summary>
        [TestMethod]
        public void TestControllerMethodIndexContainsExpectedAttributes()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            var controllerMethod = controllerClass.GetMethod("Index");
            #endregion Arrange

            #region Act
            var allAttributes = controllerMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the controller method about contains expected attributes.
        /// #2
        /// </summary>
        [TestMethod]
        public void TestControllerMethodAboutContainsExpectedAttributes()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            var controllerMethod = controllerClass.GetMethod("About");
            #endregion Arrange

            #region Act
            var allAttributes = controllerMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the controller method return to calling application contains expected attributes.
        /// #3
        /// </summary>
        [TestMethod]
        public void TestControllerMethodReturnToCallingApplicationContainsExpectedAttributes()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            var controllerMethod = controllerClass.GetMethod("ReturnToCallingApplication");
            #endregion Arrange

            #region Act
            var allAttributes = controllerMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }
        /// <summary>
        /// #4
        /// </summary>
        [TestMethod]
        public void TestControllerMethodAuthorizedHomeContainsExpectedAttributes()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            var controllerMethod = controllerClass.GetMethod("AuthorizedHome");
            #endregion Arrange

            #region Act
            var expectedAttribute = controllerMethod.GetCustomAttributes(true).OfType<AuthorizeAttribute>();
            var allAttributes = controllerMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(1, expectedAttribute.Count(), "AuthorizeAttribute not found");
            Assert.AreEqual(1, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// #5
        /// </summary>
        [TestMethod]
        public void TestControllerMethodResetCacheContainsExpectedAttributes()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            var controllerMethod = controllerClass.GetMethod("ResetCache");
            #endregion Arrange

            #region Act
            var expectedAttribute = controllerMethod.GetCustomAttributes(true).OfType<AdminOnlyAttribute>();
            var allAttributes = controllerMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(1, expectedAttribute.Count(), "AdminOnlyAttribute not found");
            Assert.AreEqual(1, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        #endregion Controller Method Tests

        #endregion Reflection
    }
}
