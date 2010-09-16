using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using HelpRequest.Controllers;
using HelpRequest.Controllers.Filters;
using HelpRequest.Controllers.Helpers;
using HelpRequest.Controllers.ViewModels;
using HelpRequest.Core.Abstractions;
using HelpRequest.Core.Domain;
using HelpRequest.Core.Resources;
using HelpRequest.Tests.Core.Extensions;
using HelpRequest.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.Attributes;
using MvcContrib.TestHelper;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Testing;
using UCDArch.Web.Attributes;
using Rhino.Mocks;

namespace HelpRequest.Tests.Controllers
{
    [TestClass]
    public class TicketControllerTests : ControllerTestBase<TicketController>
    {
        protected readonly Type ControllerClass = typeof(TicketController);
        public IRepository<Ticket> TicketRepository;
        protected IEmailProvider EmailProvider;
        protected ITicketControllerService TicketControllerService;

        #region Init

        protected override void SetupController()
        {
            TicketRepository = FakeRepository<Ticket>();
            EmailProvider = MockRepository.GenerateStub<IEmailProvider>();
            TicketControllerService = MockRepository.GenerateStub<ITicketControllerService>();
            Controller = new TestControllerBuilder().CreateController<TicketController>(EmailProvider, TicketControllerService);
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
        /// Tests the log on and submit mapping.
        /// </summary>
        [TestMethod]
        public void TestLogOnAndSubmitMapping()
        {
            "~/Ticket/LogOnAndSubmit/?appName=Test".ShouldMapTo<TicketController>(a => a.LogOnAndSubmit("Test"), true);
        }

        /// <summary>
        /// Tests the submit redirect mapping.
        /// </summary>
        [TestMethod]
        public void TestSubmitRedirectMapping()
        {
            "~/Ticket/SubmitRedirect/?appName=Test".ShouldMapTo<TicketController>(a => a.SubmitRedirect("Test"), true);
        }
        #endregion Mapping Tests

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
            var result = Controller.LogOnAndSubmit(appName)
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.SubmitRedirect(appName));
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
            var result = Controller.LogOnAndSubmit(appName)
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.SubmitRedirect(appName));
            #endregion Act

            #region Assert
            TicketControllerService.AssertWasCalled(a => a.CasLogin());
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            #endregion Assert
        }

        #endregion LogOnAndSubmit Tests

        #region SubmitRedirect Tests

        [TestMethod]
        public void TestAddTest()
        {
            #region Arrange

            Assert.Inconclusive("Need to add these tests");

            #endregion Arrange

            #region Act

            #endregion Act

            #region Assert

            #endregion Assert		
        }

        #endregion SubmitRedirect Tests

        #region Reflection
        #region Controller Class Tests

        /// <summary>
        /// Tests the controller inherits from super controller.
        /// </summary>
        [TestMethod]
        public void TestControllerInheritsFromSuperController()
        {
            #region Arrange
            var controllerClass = ControllerClass;
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
            var controllerClass = ControllerClass;
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
            var controllerClass = ControllerClass;
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
            var controllerClass = ControllerClass;
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
            var controllerClass = ControllerClass;
            #endregion Arrange

            #region Act
            var result = controllerClass.GetMethods().Where(a => a.DeclaringType == controllerClass);
            #endregion Act

            #region Assert
            Assert.AreEqual(1, result.Count(), "It looks like a method was added or removed from the controller.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the controller method log on and submit contains expected attributes.
        /// </summary>
        [TestMethod]
        public void TestControllerMethodLogOnAndSubmitContainsExpectedAttributes()
        {
            #region Arrange
            var controllerClass = ControllerClass;
            var controllerMethod = controllerClass.GetMethod("LogOnAndSubmit");
            #endregion Arrange

            #region Act
            var allAttributes = controllerMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        ///// <summary>
        ///// Tests the controller method create get contains expected attributes.
        ///// #2
        ///// </summary>
        //[TestMethod]
        //public void TestControllerMethodCreateGetContainsExpectedAttributes()
        //{
        //    #region Arrange
        //    var controllerClass = _controllerClass;
        //    var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Create");
        //    #endregion Arrange

        //    #region Act
        //    var expectedAttribute = controllerMethod.ElementAt(0).GetCustomAttributes(true).OfType<AdminOnlyAttribute>();
        //    var allAttributes = controllerMethod.ElementAt(0).GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    Assert.AreEqual(1, expectedAttribute.Count(), "AdminOnlyAttribute not found");
        //    Assert.AreEqual(1, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}

        ///// <summary>
        ///// Tests the controller method create post contains expected attributes.
        ///// #3
        ///// </summary>
        //[TestMethod]
        //public void TestControllerMethodCreatePostContainsExpectedAttributes1()
        //{
        //    #region Arrange
        //    var controllerClass = _controllerClass;
        //    var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Create");
        //    #endregion Arrange

        //    #region Act
        //    var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<AdminOnlyAttribute>();
        //    var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    Assert.AreEqual(1, expectedAttribute.Count(), "AdminOnlyAttribute not found");
        //    Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}

        ///// <summary>
        ///// Tests the controller method create post contains expected attributes.
        ///// #3
        ///// </summary>
        //[TestMethod]
        //public void TestControllerMethodCreatePostContainsExpectedAttributes2()
        //{
        //    #region Arrange
        //    var controllerClass = _controllerClass;
        //    var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Create");
        //    #endregion Arrange

        //    #region Act
        //    var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<AcceptPostAttribute>();
        //    var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    Assert.AreEqual(1, expectedAttribute.Count(), "AcceptPostAttribute not found");
        //    Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}

        ///// <summary>
        ///// Tests the controller method create post contains expected attributes.
        ///// #3
        ///// </summary>
        //[TestMethod]
        //public void TestControllerMethodCreatePostContainsExpectedAttributes3()
        //{
        //    #region Arrange
        //    var controllerClass = _controllerClass;
        //    var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Create");
        //    #endregion Arrange

        //    #region Act
        //    var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<ValidateInputAttribute>();
        //    var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    Assert.AreEqual(1, expectedAttribute.Count(), "ValidateInputAttribute not found");
        //    Assert.IsFalse(expectedAttribute.ElementAt(0).EnableValidation);
        //    Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}

        ///// <summary>
        ///// Tests the controller method edit get contains expected attributes.
        ///// #4
        ///// </summary>
        //[TestMethod]
        //public void TestControllerMethodEditGetContainsExpectedAttributes()
        //{
        //    #region Arrange
        //    var controllerClass = _controllerClass;
        //    var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Edit");
        //    #endregion Arrange

        //    #region Act
        //    var expectedAttribute = controllerMethod.ElementAt(0).GetCustomAttributes(true).OfType<AdminOnlyAttribute>();
        //    var allAttributes = controllerMethod.ElementAt(0).GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    Assert.AreEqual(1, expectedAttribute.Count(), "AdminOnlyAttribute not found");
        //    Assert.AreEqual(1, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}

        ///// <summary>
        ///// Tests the controller method edit post contains expected attributes1.
        ///// #5
        ///// </summary>
        //[TestMethod]
        //public void TestControllerMethodEditPostContainsExpectedAttributes1()
        //{
        //    #region Arrange
        //    var controllerClass = _controllerClass;
        //    var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Edit");
        //    #endregion Arrange

        //    #region Act
        //    var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<AdminOnlyAttribute>();
        //    var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    Assert.AreEqual(1, expectedAttribute.Count(), "AdminOnlyAttribute not found");
        //    Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}


        ///// <summary>
        ///// Tests the controller method edit post contains expected attributes2.
        ///// #5
        ///// </summary>
        //[TestMethod]
        //public void TestControllerMethodEditPostContainsExpectedAttributes2()
        //{
        //    #region Arrange
        //    var controllerClass = _controllerClass;
        //    var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Edit");
        //    #endregion Arrange

        //    #region Act
        //    var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<AcceptPostAttribute>();
        //    var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    Assert.AreEqual(1, expectedAttribute.Count(), "AcceptPostAttribute not found");
        //    Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}


        ///// <summary>
        ///// Tests the controller method edit post contains expected attributes3.
        ///// #5
        ///// </summary>
        //[TestMethod]
        //public void TestControllerMethodEditPostContainsExpectedAttributes3()
        //{
        //    #region Arrange
        //    var controllerClass = _controllerClass;
        //    var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Create");
        //    #endregion Arrange

        //    #region Act
        //    var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<ValidateInputAttribute>();
        //    var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    Assert.AreEqual(1, expectedAttribute.Count(), "ValidateInputAttribute not found");
        //    Assert.IsFalse(expectedAttribute.ElementAt(0).EnableValidation);
        //    Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}

        ///// <summary>
        ///// Tests the controller method details contains expected attributes.
        ///// #6
        ///// </summary>
        //[TestMethod]
        //public void TestControllerMethodDetailsContainsExpectedAttributes()
        //{
        //    #region Arrange
        //    var controllerClass = _controllerClass;
        //    var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Details");
        //    #endregion Arrange

        //    #region Act
        //    //var expectedAttribute = controllerMethod.ElementAt(0).GetCustomAttributes(true).OfType<AdminOnlyAttribute>();
        //    var allAttributes = controllerMethod.ElementAt(0).GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    //Assert.AreEqual(1, expectedAttribute.Count(), "AdminOnlyAttribute not found");
        //    Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}

        ///// <summary>
        ///// Tests the controller method watch video contains expected attributes.
        ///// #7
        ///// </summary>
        //[TestMethod]
        //public void TestControllerMethodWatchVideoContainsExpectedAttributes()
        //{
        //    #region Arrange
        //    var controllerClass = _controllerClass;
        //    var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "WatchVideo");
        //    #endregion Arrange

        //    #region Act
        //    //var expectedAttribute = controllerMethod.ElementAt(0).GetCustomAttributes(true).OfType<AdminOnlyAttribute>();
        //    var allAttributes = controllerMethod.ElementAt(0).GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    //Assert.AreEqual(1, expectedAttribute.Count(), "AdminOnlyAttribute not found");
        //    Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}

        #endregion Controller Method Tests

        #endregion Reflection
    }
}
