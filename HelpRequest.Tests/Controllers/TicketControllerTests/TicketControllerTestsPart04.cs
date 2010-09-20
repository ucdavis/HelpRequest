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

        #endregion PublicSubmit Post Tests
    }
}
