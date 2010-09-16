using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using HelpRequest.Controllers.ViewModels;
using HelpRequest.Core.Domain;
using HelpRequest.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelpRequest;
using HelpRequest.Controllers;
using MvcContrib.TestHelper;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Rhino.Mocks;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Testing;
using UCDArch.Web.Attributes;
using HelpRequest.Tests.Core.Extensions;

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
            "~/Home/Index/?appName=Test".ShouldMapTo<HomeController>(a => a.Index("Test"), true);
        }

        /// <summary>
        /// Tests the about mapping.
        /// </summary>
        [TestMethod]
        public void TestAboutMapping()
        {
            "~/Home/About/?appName=Test".ShouldMapTo<HomeController>(a => a.About("Test"), true);
        }

        /// <summary>
        /// Tests the return to calling application mapping.
        /// </summary>
        [TestMethod]
        public void TestReturnToCallingApplicationMapping()
        {
            "~/Home/ReturnToCallingApplication/?url=Test".ShouldMapTo<HomeController>(a => a.ReturnToCallingApplication("Test"), true);
        }

        #endregion Mapping Tests

        #region Index Tests

        /// <summary>
        /// Tests the index returns view.
        /// </summary>
        [TestMethod]
        public void TestIndexReturnsView()
        {
            Controller.Index(null)
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
            var result = Controller.Index("ABBR2")
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
            var result = Controller.Index(null)
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
            var result = Controller.Index("Not Found")
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
            var result = Controller.Index("ABBR2")
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
            Controller.Index("ABBR2");
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
            Controller.Index(string.Empty);
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
            Controller.Index(null);
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
            Controller.Index("  ");
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
            var result = Controller.About(appName)
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
            var result = Controller.About(appName)
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

        #region Reflection
        #region Controller Class Tests

        /// <summary>
        /// Tests the controller inherits from super controller.
        /// </summary>
        [TestMethod]
        public void TestControllerInheritsFromSuperController()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            #endregion Arrange

            #region Act
            Assert.IsNotNull(controllerClass.BaseType);
            var result = controllerClass.BaseType.Name;
            #endregion Act

            #region Assert
            Assert.AreEqual("SuperController", result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the controller has only two attributes.
        /// </summary>
        [TestMethod]
        public void TestControllerHasOnlyTwoAttributes()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            #endregion Arrange

            #region Act
            var result = controllerClass.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(2, result.Count());
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
            Assert.AreEqual(3, result.Count(), "It looks like a method was added or removed from the controller.");
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

        #endregion Controller Method Tests

        #endregion Reflection
    }
}
