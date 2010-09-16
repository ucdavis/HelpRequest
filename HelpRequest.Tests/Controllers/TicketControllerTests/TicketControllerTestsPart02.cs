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
            var result = Controller.SubmitRedirect(appName)
                .AssertActionRedirect()
                .ToAction<TicketController>(a => a.PublicSubmit(appName));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            #endregion Assert		
        }

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
    }
}
