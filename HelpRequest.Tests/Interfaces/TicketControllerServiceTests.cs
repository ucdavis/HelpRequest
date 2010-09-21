using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using HelpRequest.Controllers.Services;
using HelpRequest.Core.Abstractions;
using HelpRequest.Core.Domain;
using HelpRequest.Core.Resources;
using HelpRequest.Tests.Core.Extensions;
using HelpRequest.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Attachment = HelpRequest.Core.Domain.Attachment;

namespace HelpRequest.Tests.Interfaces
{
    [TestClass]
    public class TicketControllerServiceTests
    {
        protected readonly Type ServiceClass = typeof(TicketControllerService);
        protected readonly Type InterfaceClass = typeof(ITicketControllerService);
        protected TicketControllerService TicketControllerService;
        protected IEmailProvider EmailProvider;

        public TicketControllerServiceTests()
        {
            TicketControllerService = new TicketControllerService();
            EmailProvider = MockRepository.GenerateStub<IEmailProvider>();
        }

        #region CheckForSupportEmailAddresses Tests

        #region Check FromEmail is not support email
        
        /// <summary>
        /// Tests the check for support email addresses sets model state error when email is detected.
        /// </summary>
        [TestMethod]
        public void TestCheckForSupportEmailAddressesSetsModelStateErrorWhenEmailIsDetected01()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "ASISupport@caes.ucdavis.edu";            
            #endregion Arrange

            #region Act
            TicketControllerService.CheckForSupportEmailAddresses(modelState, ticket);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("Your Email can't be a support email.");
            #endregion Assert		
        }
        /// <summary>
        /// Tests the check for support email addresses sets model state error when email is detected.
        /// </summary>
        [TestMethod]
        public void TestCheckForSupportEmailAddressesSetsModelStateErrorWhenEmailIsDetected02()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "AppRequests@caes.ucdavis.edu";
            #endregion Arrange

            #region Act
            TicketControllerService.CheckForSupportEmailAddresses(modelState, ticket);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("Your Email can't be a support email.");
            #endregion Assert
        }
        /// <summary>
        /// Tests the check for support email addresses sets model state error when email is detected.
        /// </summary>
        [TestMethod]
        public void TestCheckForSupportEmailAddressesSetsModelStateErrorWhenEmailIsDetected03()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "Clusters@caes.ucdavis.edu";
            #endregion Arrange

            #region Act
            TicketControllerService.CheckForSupportEmailAddresses(modelState, ticket);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("Your Email can't be a support email.");
            #endregion Assert
        }
        /// <summary>
        /// Tests the check for support email addresses sets model state error when email is detected.
        /// </summary>
        [TestMethod]
        public void TestCheckForSupportEmailAddressesSetsModelStateErrorWhenEmailIsDetected04()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "CSRequests@caes.ucdavis.edu";
            #endregion Arrange

            #region Act
            TicketControllerService.CheckForSupportEmailAddresses(modelState, ticket);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("Your Email can't be a support email.");
            #endregion Assert
        }
        /// <summary>
        /// Tests the check for support email addresses sets model state error when email is detected.
        /// </summary>
        [TestMethod]
        public void TestCheckForSupportEmailAddressesSetsModelStateErrorWhenEmailIsDetected05()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "ITPLPNEM@ucdavis.edu";
            #endregion Arrange

            #region Act
            TicketControllerService.CheckForSupportEmailAddresses(modelState, ticket);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("Your Email can't be a support email.");
            #endregion Assert
        }
        /// <summary>
        /// Tests the check for support email addresses sets model state error when email is detected.
        /// </summary>
        [TestMethod]
        public void TestCheckForSupportEmailAddressesSetsModelStateErrorWhenEmailIsDetected06()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "PLPNEMITSupport@caes.ucdavis.edu";
            #endregion Arrange

            #region Act
            TicketControllerService.CheckForSupportEmailAddresses(modelState, ticket);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("Your Email can't be a support email.");
            #endregion Assert
        }
        /// <summary>
        /// Tests the check for support email addresses sets model state error when email is detected.
        /// </summary>
        [TestMethod]
        public void TestCheckForSupportEmailAddressesSetsModelStateErrorWhenEmailIsDetected07()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "ITSupport@ucdavis.edu";
            #endregion Arrange

            #region Act
            TicketControllerService.CheckForSupportEmailAddresses(modelState, ticket);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("Your Email can't be a support email.");
            #endregion Assert
        }
        /// <summary>
        /// Tests the check for support email addresses sets model state error when email is detected.
        /// </summary>
        [TestMethod]
        public void TestCheckForSupportEmailAddressesSetsModelStateErrorWhenEmailIsDetected08()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "OCSSupport@caes.ucdavis.edu";
            #endregion Arrange

            #region Act
            TicketControllerService.CheckForSupportEmailAddresses(modelState, ticket);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("Your Email can't be a support email.");
            #endregion Assert
        }
        /// <summary>
        /// Tests the check for support email addresses sets model state error when email is detected.
        /// </summary>
        [TestMethod]
        public void TestCheckForSupportEmailAddressesSetsModelStateErrorWhenEmailIsDetected09()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "OGSWeb@caes.ucdavis.edu";
            #endregion Arrange

            #region Act
            TicketControllerService.CheckForSupportEmailAddresses(modelState, ticket);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("Your Email can't be a support email.");
            #endregion Assert
        }
        /// <summary>
        /// Tests the check for support email addresses sets model state error when email is detected.
        /// </summary>
        [TestMethod]
        public void TestCheckForSupportEmailAddressesSetsModelStateErrorWhenEmailIsDetected10()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "WebRequests@caes.ucdavis.edu";
            #endregion Arrange

            #region Act
            TicketControllerService.CheckForSupportEmailAddresses(modelState, ticket);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("Your Email can't be a support email.");
            #endregion Assert
        }
        #endregion Check FromEmail is not support email


        /// <summary>
        /// Tests the check for support email addresses sets model state error when email is detected.
        /// </summary>
        [TestMethod]
        public void TestCheckForSupportEmailAddressesSetsModelStateErrorWhenEmailIsDetected11()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "test@ucdavis.edu";
            ticket.EmailCCs = new List<string>();
            ticket.EmailCCs.Add("ASISupport@caes.ucdavis.edu");
            ticket.EmailCCs.Add("AppRequests@caes.ucdavis.edu");
            ticket.EmailCCs.Add("Clusters@caes.ucdavis.edu");
            ticket.EmailCCs.Add("CSRequests@caes.ucdavis.edu");
            ticket.EmailCCs.Add("ITPLPNEM@ucdavis.edu");

            ticket.EmailCCs.Add("thisone@isok.com");

            ticket.EmailCCs.Add("PLPNEMITSupport@caes.ucdavis.edu");
            ticket.EmailCCs.Add("ITSupport@ucdavis.edu");
            ticket.EmailCCs.Add("OCSSupport@caes.ucdavis.edu");
            ticket.EmailCCs.Add("OGSWeb@caes.ucdavis.edu");
            ticket.EmailCCs.Add("WebRequests@caes.ucdavis.edu");
            #endregion Arrange

            #region Act
            TicketControllerService.CheckForSupportEmailAddresses(modelState, ticket);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre(
                "Carbon Copy Email 1 can't be a support email.",
                "Carbon Copy Email 2 can't be a support email.",
                "Carbon Copy Email 3 can't be a support email.",
                "Carbon Copy Email 4 can't be a support email.",
                "Carbon Copy Email 5 can't be a support email.",
                "Carbon Copy Email 7 can't be a support email.",
                "Carbon Copy Email 8 can't be a support email.",
                "Carbon Copy Email 9 can't be a support email.",
                "Carbon Copy Email 10 can't be a support email.",
                "Carbon Copy Email 11 can't be a support email.");
            #endregion Assert
        }

        #endregion CheckForSupportEmailAddresses Tests

        #region CommonSubmitValidationChecks Tests


        /// <summary>
        /// Tests the common submit validation checks calls check for support email addresses.
        /// </summary>
        [TestMethod]
        public void TestCommonSubmitValidationChecksCallsCheckForSupportEmailAddresses()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "ASISupport@caes.ucdavis.edu";
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, null, null, null);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("Your Email can't be a support email.");
            #endregion Assert		
        }


        /// <summary>
        /// Tests the web site is required when web support is picked.
        /// </summary>
        [TestMethod]
        public void TestWebSiteIsRequiredWhenWebSupportIsPicked1()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            ticket.ForWebSite = null;     
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, null, null, null);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("Web Site Address must be entered when Web Site Support is selected.");
            #endregion Assert		
        }

        /// <summary>
        /// Tests the web site is required when web support is picked.
        /// </summary>
        [TestMethod]
        public void TestWebSiteIsRequiredWhenWebSupportIsPicked2()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            ticket.ForWebSite = string.Empty;
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, null, null, null);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("Web Site Address must be entered when Web Site Support is selected.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the web site is required when web support is picked.
        /// </summary>
        [TestMethod]
        public void TestWebSiteIsRequiredWhenWebSupportIsPicked3()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            ticket.ForWebSite = " ";
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, null, null, null);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("Web Site Address must be entered when Web Site Support is selected.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the valid web site is required when web support is picked.
        /// </summary>
        [TestMethod]
        public void TestValidWebSiteIsRequiredWhenWebSupportIsPicked1()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            ticket.ForWebSite = "www.test.com";
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, null, null, null);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("A valid Web Site Address is required.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the valid web site is required when web support is picked.
        /// </summary>
        [TestMethod]
        public void TestValidWebSiteIsRequiredWhenWebSupportIsPicked2()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            ticket.ForWebSite = "httpe://builder/overview.html";
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, null, null, null);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("A valid Web Site Address is required.");
            Assert.IsFalse(modelState.IsValid);
            #endregion Assert
        }

        /// <summary>
        /// Tests the valid web site is required when web support is picked.
        /// </summary>
        [TestMethod]
        public void TestValidWebSiteIsRequiredWhenWebSupportIsPicked3()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            ticket.ForWebSite = "https://builder/overview.html";
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, null, null, null);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("A valid Web Site Address is required.");
            Assert.IsFalse(modelState.IsValid);
            #endregion Assert
        }

        /// <summary>
        /// Tests the valid web site is required when web support is picked.
        /// </summary>
        [TestMethod]
        public void TestValidWebSiteIsRequiredWhenWebSupportIsPicked4()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            ticket.ForWebSite = "hTTp://builder/overview.html";
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, null, null, null);
            #endregion Act

            #region Assert
            modelState.AssertErrorsAre("A valid Web Site Address is required.");
            Assert.IsFalse(modelState.IsValid);
            #endregion Assert
        }

        /// <summary>
        /// Tests the valid web site is required when web support is picked.
        /// </summary>
        [TestMethod]
        public void TestValidWebSiteIsRequiredWhenWebSupportIsPicked5()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            ticket.ForWebSite = "http://www.google.com/ig?hl=en&source=iglk&refresh=1";
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, null, null, null);
            #endregion Act

            #region Assert
            //modelState.AssertErrorsAre("A valid Web Site Address is required.");
            Assert.IsTrue(modelState.IsValid);
            #endregion Assert
        }

        /// <summary>
        /// Tests the valid web site is required when web support is picked.
        /// </summary>
        [TestMethod]
        public void TestValidWebSiteIsRequiredWhenWebSupportIsPicked6()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            ticket.ForWebSite = "hTTps://www.google.com/ig?hl=en&source=iglk&refresh=1";
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, null, null, null);
            #endregion Act

            #region Assert
            //modelState.AssertErrorsAre("A valid Web Site Address is required.");
            Assert.IsTrue(modelState.IsValid);
            #endregion Assert
        }


        /// <summary>
        /// Tests the common submit validation checks adds available dates to list.
        /// </summary>
        [TestMethod]
        public void TestCommonSubmitValidationChecksAddsAvailableDatesToList()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            Assert.AreEqual(0, ticket.Availability.Count);
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, null, "SomeDate", null);
            #endregion Act

            #region Assert
            Assert.AreEqual(1, ticket.Availability.Count);
            Assert.AreEqual("SomeDate", ticket.Availability[0]);
            #endregion Assert		
        }

        /// <summary>
        /// Tests the common submit validation checks removes null available dates from list.
        /// </summary>
        [TestMethod]
        public void TestCommonSubmitValidationChecksAddsNonNullAvailableDatesToList()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new [] { "One", null, "Two" };
            Assert.AreEqual(0, ticket.Availability.Count);
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, avDates, null, null, null);
            #endregion Act

            #region Assert
            Assert.AreEqual(2, ticket.Availability.Count);
            Assert.IsTrue(ticket.Availability.Contains("One"));
            Assert.IsTrue(ticket.Availability.Contains("Two"));
            #endregion Assert
        }

        /// <summary>
        /// Tests the common submit validation checks adds non empty available dates to list.
        /// </summary>
        [TestMethod]
        public void TestCommonSubmitValidationChecksAddsNonEmptyAvailableDatesToList()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            var avDates = new [] { "One", "", "Two" };
            Assert.AreEqual(0, ticket.Availability.Count);
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, avDates, null, null, null);
            #endregion Act

            #region Assert
            Assert.AreEqual(2, ticket.Availability.Count);
            Assert.IsTrue(ticket.Availability.Contains("One"));
            Assert.IsTrue(ticket.Availability.Contains("Two"));
            #endregion Assert
        }

        /// <summary>
        /// Tests the common submit validation checks adds email cc to list.
        /// </summary>
        [TestMethod]
        public void TestCommonSubmitValidationChecksAddsEmailCcToList()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            Assert.AreEqual(0, ticket.EmailCCs.Count);
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, null, null, "Test@Testy.COM");
            #endregion Act

            #region Assert
            Assert.AreEqual(1, ticket.EmailCCs.Count);
            Assert.AreEqual("test@testy.com", ticket.EmailCCs[0]);
            #endregion Assert
        }

        /// <summary>
        /// Tests the common submit validation checks adds non empty email CCS to list.
        /// </summary>
        [TestMethod]
        public void TestCommonSubmitValidationChecksAddsNonEmptyEmailCcsToList()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            var emalCCs = new[] { "One@test.com", "", "Two@TEST.COM" };
            Assert.AreEqual(0, ticket.EmailCCs.Count);
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, emalCCs, null, null);
            #endregion Act

            #region Assert
            Assert.AreEqual(2, ticket.EmailCCs.Count);
            Assert.IsTrue(ticket.EmailCCs.Contains("one@test.com"));
            Assert.IsTrue(ticket.EmailCCs.Contains("two@test.com"));
            #endregion Assert
        }

        /// <summary>
        /// Tests the common submit validation checks adds non null email CCS to list.
        /// </summary>
        [TestMethod]
        public void TestCommonSubmitValidationChecksAddsNonNullEmailCcsToList()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            var emalCCs = new[] { "One@test.com", null, "Two@TEST.COM" };
            Assert.AreEqual(0, ticket.EmailCCs.Count);
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, emalCCs, null, null);
            #endregion Act

            #region Assert
            Assert.AreEqual(2, ticket.EmailCCs.Count);
            Assert.IsTrue(ticket.EmailCCs.Contains("one@test.com"));
            Assert.IsTrue(ticket.EmailCCs.Contains("two@test.com"));
            #endregion Assert
        }

        /// <summary>
        /// Tests the common submit validation checks validates email CCS.
        /// </summary>
        [TestMethod]
        public void TestCommonSubmitValidationChecksValidatesEmailCcs()
        {
            #region Arrange
            var modelState = new ModelStateDictionary();
            var ticket = CreateValidEntities.Ticket(1);
            var emalCCs = new[] { "One@test.com", "", "Two@TEST@com.COM" };
            Assert.AreEqual(0, ticket.EmailCCs.Count);
            #endregion Arrange

            #region Act
            TicketControllerService.CommonSubmitValidationChecks(modelState, ticket, null, emalCCs, null, "Another@invalid@one.com");
            #endregion Act

            #region Assert
            Assert.AreEqual(3, ticket.EmailCCs.Count);
            modelState.AssertErrorsAre("Carbon Copy Email 1 is not valid", "Carbon Copy Email 3 is not valid");
            #endregion Assert
        }

        #endregion CommonSubmitValidationChecks Tests

        #region LoadFileContents Tests

        /// <summary>
        /// Tests the load file contents with null value does not add file.
        /// </summary>
        [TestMethod]
        public void TestLoadFileContentsWithNullValueDoesNotAddFile()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.Attachments = new List<Attachment>();
            Assert.AreEqual(0, ticket.Attachments.Count);
            #endregion Arrange

            #region Act
            TicketControllerService.LoadFileContents(ticket, null);
            #endregion Act

            #region Assert
            Assert.AreEqual(0, ticket.Attachments.Count);
            #endregion Assert		
        }

        /// <summary>
        /// Tests the load file contents with non null value does add file.
        /// </summary>
        [TestMethod]
        public void TestLoadFileContentsWithNonNullValueDoesAddFile()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.Attachments = new List<Attachment>();
            Assert.AreEqual(0, ticket.Attachments.Count);
            var upload = new MockHttpPostedFileBase();
            #endregion Arrange

            #region Act
            TicketControllerService.LoadFileContents(ticket, upload);
            #endregion Act

            #region Assert
            Assert.AreEqual(1, ticket.Attachments.Count);
            Assert.AreEqual("Mocked File Name", ticket.Attachments.ElementAtOrDefault(0).FileName);
            Assert.AreEqual("45678", ticket.Attachments.ElementAtOrDefault(0).Contents.ByteArrayToString());
            Assert.AreEqual("Mocked Type", ticket.Attachments.ElementAtOrDefault(0).ContentType);
            #endregion Assert
        }

        #endregion LoadFileContents Tests

        #region CAS Login Tests
        //The helper is a static class and not really worth testing
        #endregion CAS Login Tests

        #region FindKerbUser Tests              
        //I don't want to test this because that means multiple calls to this.
        #endregion FindKerbUser Tests

        #region SendHelpRequest Tests

        /// <summary>
        /// Tests the send help request throws exception if the ticket is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UCDArch.Core.Utils.PreconditionException))]
        public void TestSendHelpRequestThrowsExceptionIfTheTicketIsNull()
        {
            try
            {
                #region Arrange
                Ticket ticket = null;
                #endregion Arrange

                #region Act
                TicketControllerService.SendHelpRequest(ticket, true, EmailProvider);
                #endregion Act
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual("Details are missing.", ex.Message);
                throw;
            }	
        }

        /// <summary>
        /// Tests the send help request throws exception if it is not A public email and user is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UCDArch.Core.Utils.PreconditionException))]
        public void TestSendHelpRequestThrowsExceptionIfItIsNotAPublicEmailAndUserIsNull()
        {
            try
            {
                #region Arrange
                Ticket ticket = CreateValidEntities.Ticket(1);
                ticket.User = null;
                #endregion Arrange

                #region Act
                TicketControllerService.SendHelpRequest(ticket, false, EmailProvider);
                #endregion Act
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual("Login Details missing.", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tests the send help request throws exception if it is A public email and from email is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UCDArch.Core.Utils.PreconditionException))]
        public void TestSendHelpRequestThrowsExceptionIfItIsAPublicEmailAndFromEmailIsNull()
        {
            try
            {
                #region Arrange
                Ticket ticket = CreateValidEntities.Ticket(1);
                ticket.User = CreateValidEntities.User(1);
                ticket.FromEmail = null;
                #endregion Arrange

                #region Act
                TicketControllerService.SendHelpRequest(ticket, true, EmailProvider);
                #endregion Act
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual("Email details missing.", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tests the send help request throws exception if it is A public email and from email is empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UCDArch.Core.Utils.PreconditionException))]
        public void TestSendHelpRequestThrowsExceptionIfItIsAPublicEmailAndFromEmailIsEmpty()
        {
            try
            {
                #region Arrange
                Ticket ticket = CreateValidEntities.Ticket(1);
                ticket.User = CreateValidEntities.User(1);
                ticket.FromEmail = string.Empty;
                #endregion Arrange

                #region Act
                TicketControllerService.SendHelpRequest(ticket, true, EmailProvider);
                #endregion Act
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual("Email details missing.", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tests the send help request throws exception if it is not A public email and user email is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UCDArch.Core.Utils.PreconditionException))]
        public void TestSendHelpRequestThrowsExceptionIfItIsNotAPublicEmailAndUserEmailIsNull()
        {
            try
            {
                #region Arrange
                Ticket ticket = CreateValidEntities.Ticket(1);
                ticket.User = CreateValidEntities.User(1);
                ticket.User.Email = null;
                #endregion Arrange

                #region Act
                TicketControllerService.SendHelpRequest(ticket, false, EmailProvider);
                #endregion Act
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual("Email details missing.", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tests the send help request throws exception support email is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UCDArch.Core.Utils.PreconditionException))]
        public void TestSendHelpRequestThrowsExceptionSupportEmailIsNull()
        {
            var saveAppHelpDeskEmail = ConfigurationManager.AppSettings["AppHelpDeskEmail"];
            try
            {
                #region Arrange
                Ticket ticket = CreateValidEntities.Ticket(1);
                ticket.User = CreateValidEntities.User(1);
                ticket.FromEmail = "test@testy.com";
                ticket.SupportDepartment = StaticValues.STR_ProgrammingSupport;
                ConfigurationManager.AppSettings["AppHelpDeskEmail"] = null;
                #endregion Arrange

                #region Act
                TicketControllerService.SendHelpRequest(ticket, true, EmailProvider);
                #endregion Act
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual("Help Desk Email address not supplied.", ex.Message);
                throw;
            }
            finally
            {
                ConfigurationManager.AppSettings["AppHelpDeskEmail"] = saveAppHelpDeskEmail;
            }
        }

        /// <summary>
        /// Tests the send help request throws exception support email is empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UCDArch.Core.Utils.PreconditionException))]
        public void TestSendHelpRequestThrowsExceptionSupportEmailIsEmpty()
        {
            var saveAppHelpDeskEmail = ConfigurationManager.AppSettings["AppHelpDeskEmail"];
            try
            {
                #region Arrange
                Ticket ticket = CreateValidEntities.Ticket(1);
                ticket.User = CreateValidEntities.User(1);
                ticket.FromEmail = "test@testy.com";
                ticket.SupportDepartment = StaticValues.STR_ProgrammingSupport;
                ConfigurationManager.AppSettings["AppHelpDeskEmail"] = string.Empty;
                #endregion Arrange

                #region Act
                TicketControllerService.SendHelpRequest(ticket, true, EmailProvider);
                #endregion Act
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual("Help Desk Email address not supplied.", ex.Message);
                throw;
            }
            finally
            {
                ConfigurationManager.AppSettings["AppHelpDeskEmail"] = saveAppHelpDeskEmail;
            }
        }

        /// <summary>
        /// Tests the send help request throws exception if it is not A public email and user email is empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UCDArch.Core.Utils.PreconditionException))]
        public void TestSendHelpRequestThrowsExceptionIfItIsNotAPublicEmailAndUserEmailIsEmpty()
        {
            try
            {
                #region Arrange
                Ticket ticket = CreateValidEntities.Ticket(1);
                ticket.User = CreateValidEntities.User(1);
                ticket.User.Email = string.Empty;
                #endregion Arrange

                #region Act
                TicketControllerService.SendHelpRequest(ticket, false, EmailProvider);
                #endregion Act
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual("Email details missing.", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tests the send help request throws exception if from email is not an email format.
        /// Note, thrown by MailMessage
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestSendHelpRequestThrowsExceptionIfFromEmailIsNotAnEmailFormat()
        {
            try
            {
                #region Arrange
                Ticket ticket = CreateValidEntities.Ticket(1);
                ticket.User = CreateValidEntities.User(1);
                ticket.FromEmail = "NotValid";
                #endregion Arrange

                #region Act
                TicketControllerService.SendHelpRequest(ticket, true, EmailProvider);
                #endregion Act
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual("The specified string is not in the form required for an e-mail address.", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tests the send help request throws exception if to email is not an email format.
        /// Note, thrown by MailMessage
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestSendHelpRequestThrowsExceptionIfToEmailIsNotAnEmailFormat()
        {
            var saveAppHelpDeskEmail = ConfigurationManager.AppSettings["AppHelpDeskEmail"];
            try
            {
                #region Arrange
                Ticket ticket = CreateValidEntities.Ticket(1);
                ticket.SupportDepartment = StaticValues.STR_ProgrammingSupport;
                ticket.FromEmail = "test@testy.com";
                ConfigurationManager.AppSettings["AppHelpDeskEmail"] = "notvalid";
                #endregion Arrange

                #region Act
                TicketControllerService.SendHelpRequest(ticket, true, EmailProvider);
                #endregion Act
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual("The specified string is not in the form required for an e-mail address.", ex.Message);
                throw;
            }
            finally
            {
                ConfigurationManager.AppSettings["AppHelpDeskEmail"] = saveAppHelpDeskEmail;
            }
        }


        /// <summary>
        /// Tests the send help request filters some emails from carbon copy.
        /// </summary>
        [TestMethod]
        public void TestSendHelpRequestFiltersSomeEmailsFromCarbonCopy()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "test@testy.com";
            ticket.EmailCCs.Add("jason@test.com");
            ticket.EmailCCs.Add("smith@caes.ucdavis.edu");
            ticket.EmailCCs.Add("Jason2@test.com");           
            #endregion Arrange

            #region Act
            TicketControllerService.SendHelpRequest(ticket, true, EmailProvider);
            #endregion Act

            #region Assert
            EmailProvider.AssertWasCalled(a => a.SendEmail(Arg<MailMessage>.Is.Anything));
            var args = (MailMessage)EmailProvider.GetArgumentsForCallsMadeOn(a => a.SendEmail(Arg<MailMessage>.Is.Anything))[0][0];
            Assert.IsNotNull(args);
            Assert.AreEqual(2, args.CC.Count);
            foreach (var carbonCopy in args.CC)
            {
                Assert.AreNotEqual("smith@caes.ucdavis.edu", carbonCopy);
            }
            #endregion Assert		
        }

        /// <summary>
        /// Tests the send help request adds all attachments in the ticket.
        /// </summary>
        [TestMethod]
        public void TestSendHelpRequestAddsAllAttachmentsInTheTicket()
        {
            #region Arrange           
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "test@testy.com";
            ticket.Attachments.Add(CreateValidEntities.Attachment(1));
            ticket.Attachments.Add(CreateValidEntities.Attachment(2));
            ticket.Attachments.Add(CreateValidEntities.Attachment(3));
            foreach (var attachment in ticket.Attachments)
            {
                attachment.ContentType = MediaTypeNames.Application.Octet;
            }
            ticket.Attachments.ElementAt(0).Contents = new byte[] { 0, 1, 1, 0 };
            ticket.Attachments.ElementAt(1).Contents = new byte[] { 0, 1, 1, 1, 0 };
            ticket.Attachments.ElementAt(2).Contents = new byte[] { 0, 1, 1, 1, 1, 0 };
            #endregion Arrange

            #region Act
            TicketControllerService.SendHelpRequest(ticket, true, EmailProvider);
            #endregion Act

            #region Assert
            EmailProvider.AssertWasCalled(a => a.SendEmail(Arg<MailMessage>.Is.Anything));
            var args = (MailMessage)EmailProvider.GetArgumentsForCallsMadeOn(a => a.SendEmail(Arg<MailMessage>.Is.Anything))[0][0];
            Assert.IsNotNull(args);
            Assert.AreEqual(3, args.Attachments.Count);
            Assert.AreEqual("FileName1", args.Attachments.ElementAt(0).Name);
            Assert.AreEqual("FileName2", args.Attachments.ElementAt(1).Name);
            Assert.AreEqual("FileName3", args.Attachments.ElementAt(2).Name);
            Assert.AreEqual(4, args.Attachments.ElementAt(0).ContentStream.Length);
            Assert.AreEqual(5, args.Attachments.ElementAt(1).ContentStream.Length);
            Assert.AreEqual(6, args.Attachments.ElementAt(2).ContentStream.Length);
            #endregion Assert
        }

        /// <summary>
        /// Tests the send help request has plain text body.
        /// </summary>
        [TestMethod]
        public void TestSendHelpRequestHasPlainTextBody()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "test@testy.com";
            #endregion Arrange

            #region Act
            TicketControllerService.SendHelpRequest(ticket, true, EmailProvider);
            #endregion Act

            #region Assert
            EmailProvider.AssertWasCalled(a => a.SendEmail(Arg<MailMessage>.Is.Anything));
            var args = (MailMessage)EmailProvider.GetArgumentsForCallsMadeOn(a => a.SendEmail(Arg<MailMessage>.Is.Anything))[0][0];
            Assert.IsNotNull(args);
            Assert.IsFalse(args.IsBodyHtml);
            #endregion Assert
        }

        /// <summary>
        /// Tests the send help request has formatted body body.
        /// </summary>
        [TestMethod]
        public void TestSendHelpRequestHasFormattedBodyBody()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "test@testy.com";
            #endregion Arrange

            #region Act
            TicketControllerService.SendHelpRequest(ticket, true, EmailProvider);
            #endregion Act

            #region Assert
            EmailProvider.AssertWasCalled(a => a.SendEmail(Arg<MailMessage>.Is.Anything));
            var args = (MailMessage)EmailProvider.GetArgumentsForCallsMadeOn(a => a.SendEmail(Arg<MailMessage>.Is.Anything))[0][0];
            Assert.IsNotNull(args);
            Assert.IsTrue(args.Body.StartsWith("Original Subject     : Subject1"));
            #endregion Assert
        }

        /// <summary>
        /// Tests the send help request has formatted expected subject.
        /// </summary>
        [TestMethod]
        public void TestSendHelpRequestHasFormattedExpectedSubject()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.FromEmail = "test@testy.com";
            #endregion Arrange

            #region Act
            TicketControllerService.SendHelpRequest(ticket, true, EmailProvider);
            #endregion Act

            #region Assert
            EmailProvider.AssertWasCalled(a => a.SendEmail(Arg<MailMessage>.Is.Anything));
            var args = (MailMessage)EmailProvider.GetArgumentsForCallsMadeOn(a => a.SendEmail(Arg<MailMessage>.Is.Anything))[0][0];
            Assert.IsNotNull(args);
            Assert.AreEqual("Subject1", args.Subject);
            #endregion Assert
        }

        #endregion SendHelpRequest Tests

        #region BuildBody Tests

        /// <summary>
        /// Tests the build body has the expected format with computer support.
        /// </summary>
        [TestMethod]
        public void TestBuildBodyHasTheExpectedFormatWithComputerSupport()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_ComputerSupport;
            ticket.ForApplication = "MAAPS";
            ticket.ForWebSite = "http://builder/overview.html";
            ticket.MessageBody = "This is a   test.";
            const string expectedValue = "Original Subject     : Subject1\r\nUrgency Level        : UrgencyLevel1\r\nSupport Department   : Computer Support\r\n\r\n\r\nSupplied Message Body :\r\nThis is a   test.\r\n";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.BuildBody(ticket);
            #endregion Act

            #region Assert
            Assert.AreEqual(expectedValue, result);
            #endregion Assert		
        }

        /// <summary>
        /// Tests the build body has the expected format with computer support and available times.
        /// </summary>
        [TestMethod]
        public void TestBuildBodyHasTheExpectedFormatWithComputerSupportAndAvailableTimes()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_ComputerSupport;
            ticket.Availability.Add("today 11-noon");
            ticket.Availability.Add("Wed approx. 8-11");
            ticket.ForApplication = "MAAPS";
            ticket.ForWebSite = "http://builder/overview.html";
            ticket.MessageBody = "This is a   test.";
            const string expectedValue = "Original Subject     : Subject1\r\nUrgency Level        : UrgencyLevel1\r\nSupport Department   : Computer Support\r\nAvailable Times      : \r\n   today 11-noon\r\n   Wed approx. 8-11\r\n\r\n\r\nSupplied Message Body :\r\nThis is a   test.\r\n";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.BuildBody(ticket);
            #endregion Act

            #region Assert
            Assert.AreEqual(expectedValue, result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the build body has the expected format with programming support.
        /// </summary>
        [TestMethod]
        public void TestBuildBodyHasTheExpectedFormatWithProgrammingSupport()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_ProgrammingSupport;
            ticket.ForApplication = "MAAPS";
            ticket.ForWebSite = "http://builder/overview.html";
            ticket.MessageBody = "This is a   test.";
            const string expectedValue = "Original Subject     : Subject1\r\nUrgency Level        : UrgencyLevel1\r\nSupport Department   : Programming Support\r\nFor Application      : MAAPS\r\n\r\n\r\nSupplied Message Body :\r\nThis is a   test.\r\n";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.BuildBody(ticket);
            #endregion Act

            #region Assert
            Assert.AreEqual(expectedValue, result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the build body has the expected format with programming support and available times.
        /// </summary>
        [TestMethod]
        public void TestBuildBodyHasTheExpectedFormatWithProgrammingSupportAndAvailableTimes()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_ProgrammingSupport;
            ticket.Availability.Add("today 11-noon");
            ticket.Availability.Add("Wed approx. 8-11");
            ticket.ForApplication = "MAAPS";
            ticket.ForWebSite = "http://builder/overview.html";
            ticket.MessageBody = "This is a   test.";
            const string expectedValue = "Original Subject     : Subject1\r\nUrgency Level        : UrgencyLevel1\r\nSupport Department   : Programming Support\r\nFor Application      : MAAPS\r\nAvailable Times      : \r\n   today 11-noon\r\n   Wed approx. 8-11\r\n\r\n\r\nSupplied Message Body :\r\nThis is a   test.\r\n";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.BuildBody(ticket);
            #endregion Act

            #region Assert
            Assert.AreEqual(expectedValue, result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the build body has the expected format with web support.
        /// </summary>
        [TestMethod]
        public void TestBuildBodyHasTheExpectedFormatWithWebSupport()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            ticket.ForApplication = "MAAPS";
            ticket.ForWebSite = "http://builder/overview.html";
            ticket.MessageBody = "This is a   test.";
            const string expectedValue = "Original Subject     : Subject1\r\nUrgency Level        : UrgencyLevel1\r\nSupport Department   : Web Site Support\r\nFor Web Site         : http://builder/overview.html\r\n\r\n\r\nSupplied Message Body :\r\nThis is a   test.\r\n";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.BuildBody(ticket);
            #endregion Act

            #region Assert
            Assert.AreEqual(expectedValue, result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the build body has the expected format with web support and available times.
        /// </summary>
        [TestMethod]
        public void TestBuildBodyHasTheExpectedFormatWithWebSupportAndAvailableTimes()
        {
            #region Arrange
            var ticket = CreateValidEntities.Ticket(1);
            ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
            ticket.Availability.Add("today 11-noon");
            ticket.Availability.Add("Wed approx. 8-11");
            ticket.ForApplication = "MAAPS";
            ticket.ForWebSite = "http://builder/overview.html";
            ticket.MessageBody = "This is a   test.";
            const string expectedValue = "Original Subject     : Subject1\r\nUrgency Level        : UrgencyLevel1\r\nSupport Department   : Web Site Support\r\nFor Web Site         : http://builder/overview.html\r\nAvailable Times      : \r\n   today 11-noon\r\n   Wed approx. 8-11\r\n\r\n\r\nSupplied Message Body :\r\nThis is a   test.\r\n";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.BuildBody(ticket);
            #endregion Act

            #region Assert
            Assert.AreEqual(expectedValue, result);
            #endregion Assert
        }

        #endregion BuildBody Tests

        #region FilterCruEmail Tests

        /// <summary>
        /// Tests the filter cru email returns true for expected emails.
        /// </summary>
        [TestMethod]
        public void TestFilterCruEmailReturnsTrueForExpectedEmails1()
        {
            #region Arrange
            var email = "shuka@ucdavis.EDU";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.FilterCruEmail(email);
            #endregion Act

            #region Assert
            Assert.IsTrue(result);
            #endregion Assert		
        }

        /// <summary>
        /// Tests the filter cru email returns true for expected emails.
        /// </summary>
        [TestMethod]
        public void TestFilterCruEmailReturnsTrueForExpectedEmails2()
        {
            #region Arrange
            var email = "shuka@ucdavis.edu";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.FilterCruEmail(email);
            #endregion Act

            #region Assert
            Assert.IsTrue(result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the filter cru email returns true for expected emails.
        /// </summary>
        [TestMethod]
        public void TestFilterCruEmailReturnsTrueForExpectedEmails3()
        {
            #region Arrange
            var email = "shuka@caes.ucdavis.edu";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.FilterCruEmail(email);
            #endregion Act

            #region Assert
            Assert.IsTrue(result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the filter cru email returns true for expected emails.
        /// </summary>
        [TestMethod]
        public void TestFilterCruEmailReturnsTrueForExpectedEmails4()
        {
            #region Arrange
            var email = "smith@caes.ucdavis.edu";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.FilterCruEmail(email);
            #endregion Act

            #region Assert
            Assert.IsTrue(result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the filter cru email returns true for expected emails.
        /// </summary>
        [TestMethod]
        public void TestFilterCruEmailReturnsTrueForExpectedEmails5()
        {
            #region Arrange
            var email = "ssmith@ucdavis.edu";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.FilterCruEmail(email);
            #endregion Act

            #region Assert
            Assert.IsTrue(result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the filter cru email returns true for expected emails.
        /// </summary>
        [TestMethod]
        public void TestFilterCruEmailReturnsTrueForExpectedEmails6()
        {
            #region Arrange
            var email = "ssmith@caes.ucdavis.edu";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.FilterCruEmail(email);
            #endregion Act

            #region Assert
            Assert.IsTrue(result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the filter cru email returns true for expected emails.
        /// </summary>
        [TestMethod]
        public void TestFilterCruEmailReturnsTrueForExpectedEmails7()
        {
            #region Arrange
            var email = "urossbach@ucdavis.edu";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.FilterCruEmail(email);
            #endregion Act

            #region Assert
            Assert.IsTrue(result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the filter cru email returns true for expected emails.
        /// </summary>
        [TestMethod]
        public void TestFilterCruEmailReturnsTrueForExpectedEmails8()
        {
            #region Arrange
            var email = "hi@caes.ucdavis.edu";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.FilterCruEmail(email);
            #endregion Act

            #region Assert
            Assert.IsTrue(result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the filter cru email returns true for expected emails.
        /// </summary>
        [TestMethod]
        public void TestFilterCruEmailReturnsTrueForExpectedEmails9()
        {
            #region Arrange
            var email = "rossbach@caes.ucdavis.edu";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.FilterCruEmail(email);
            #endregion Act

            #region Assert
            Assert.IsTrue(result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the filter cru email returns false for me.
        /// </summary>
        [TestMethod]
        public void TestFilterCruEmailReturnsFalseForMe()
        {
            #region Arrange
            var email = "jsylvestre@ucdavis.edu";
            #endregion Arrange

            #region Act
            var result = TicketControllerService.FilterCruEmail(email);
            #endregion Act

            #region Assert
            Assert.IsFalse(result);
            #endregion Assert
        }
        #endregion FilterCruEmail Tests

        #region GetHelpEmail Tests


        /// <summary>
        /// Tests the get help email returns expected values.
        /// </summary>
        [TestMethod]
        public void TestGetHelpEmailReturnsExpectedValues1()
        {
            var saveConfigValue = ConfigurationManager.AppSettings["HelpDeskEmail"];
            try
            {
                #region Arrange
                const string expectedResult = "computersupport@ucdavis.edu";
                ConfigurationManager.AppSettings["HelpDeskEmail"] = expectedResult;
                var ticket = CreateValidEntities.Ticket(1);
                ticket.SupportDepartment = StaticValues.STR_ComputerSupport;
                #endregion Arrange

                #region Act
                var result = TicketControllerService.GetHelpEmail(ticket);
                #endregion Act

                #region Assert
                Assert.AreEqual(expectedResult, result);
                #endregion Assert
            }
            finally
            {
                ConfigurationManager.AppSettings["HelpDeskEmail"] = saveConfigValue;
            }		
        }

        /// <summary>
        /// Tests the get help email returns expected values.
        /// </summary>
        [TestMethod]
        public void TestGetHelpEmailReturnsExpectedValues2()
        {
            var saveConfigValue = ConfigurationManager.AppSettings["HelpDeskEmail"];
            try
            {
                #region Arrange
                const string expectedResult = "computersupport@ucdavis.edu";
                ConfigurationManager.AppSettings["HelpDeskEmail"] = expectedResult;
                var ticket = CreateValidEntities.Ticket(1);
                ticket.SupportDepartment = "notValid";
                #endregion Arrange

                #region Act
                var result = TicketControllerService.GetHelpEmail(ticket);
                #endregion Act

                #region Assert
                Assert.AreEqual(expectedResult, result);
                #endregion Assert
            }
            finally
            {
                ConfigurationManager.AppSettings["HelpDeskEmail"] = saveConfigValue;
            }
        }
        /// <summary>
        /// Tests the get help email returns expected values.
        /// </summary>
        [TestMethod]
        public void TestGetHelpEmailReturnsExpectedValues3()
        {
            var saveConfigValue = ConfigurationManager.AppSettings["AppHelpDeskEmail"];
            try
            {
                #region Arrange
                const string expectedResult = "programmingsupport@ucdavis.edu";
                ConfigurationManager.AppSettings["AppHelpDeskEmail"] = expectedResult;
                var ticket = CreateValidEntities.Ticket(1);
                ticket.SupportDepartment = StaticValues.STR_ProgrammingSupport;
                #endregion Arrange

                #region Act
                var result = TicketControllerService.GetHelpEmail(ticket);
                #endregion Act

                #region Assert
                Assert.AreEqual(expectedResult, result);
                #endregion Assert
            }
            finally
            {
                ConfigurationManager.AppSettings["AppHelpDeskEmail"] = saveConfigValue;
            }
        }

        /// <summary>
        /// Tests the get help email returns expected values.
        /// </summary>
        [TestMethod]
        public void TestGetHelpEmailReturnsExpectedValues4()
        {
            var saveConfigValue = ConfigurationManager.AppSettings["WebHelpDeskEmail"];
            try
            {
                #region Arrange
                const string expectedResult = "websupport@ucdavis.edu";
                ConfigurationManager.AppSettings["WebHelpDeskEmail"] = expectedResult;
                var ticket = CreateValidEntities.Ticket(1);
                ticket.SupportDepartment = StaticValues.STR_WebSiteSupport;
                #endregion Arrange

                #region Act
                var result = TicketControllerService.GetHelpEmail(ticket);
                #endregion Act

                #region Assert
                Assert.AreEqual(expectedResult, result);
                #endregion Assert
            }
            finally
            {
                ConfigurationManager.AppSettings["WebHelpDeskEmail"] = saveConfigValue;
            }
        }

        #endregion GetHelpEmail Tests

        #region Reflection
        #region Service Class Tests

        /// <summary>
        /// Tests the ticket controller service inherits from system object.
        /// </summary>
        [TestMethod]
        public void TestTicketControllerServiceInheritsFromSystemObject()
        {
            #region Arrange
            var serviceClass = ServiceClass;
            #endregion Arrange

            #region Act
            Assert.IsNotNull(serviceClass.BaseType);
            var result = serviceClass.BaseType.Name;
            #endregion Act

            #region Assert
            Assert.AreEqual("Object", result);
            #endregion Assert
        }

        /// <summary>
        /// Tests the interface class.
        /// </summary>
        [TestMethod] 
        public void TestInterfaceClass()
        {
            #region Arrange
            var serviceClass = InterfaceClass;
            #endregion Arrange

            #region Assert
            Assert.IsTrue(serviceClass.IsAbstract);
            Assert.IsTrue(serviceClass.IsInterface);
            #endregion Assert
        }

        /// <summary>
        /// Tests the interface class has expected number of methods.
        /// </summary>
        [TestMethod]
        public void TestInterfaceClassHasExpectedNumberOfMethods()
        {
            #region Arrange
            var serviceClass = InterfaceClass;
            #endregion Arrange

            #region Act
            var result = serviceClass.GetMethods().Where(a => a.DeclaringType == serviceClass);
            #endregion Act

            #region Assert
            Assert.AreEqual(6, result.Count(), "It looks like a method was added or removed from the Service.");
            #endregion Assert
        }

        #endregion Service Class Tests

        #region Service Method Tests

        /// <summary>
        /// Tests the Implementation contains expected number of public methods.
        /// </summary>
        [TestMethod]
        public void TestImplementationContainsExpectedNumberOfPublicMethods()
        {
            #region Arrange
            var serviceClass = ServiceClass;
            #endregion Arrange

            #region Act
            var result = serviceClass.GetMethods().Where(a => a.DeclaringType == serviceClass);
            #endregion Act

            #region Assert
            Assert.AreEqual(9, result.Count(), "It looks like a method was added or removed from the Service.");
            #endregion Assert
        }
               
        /// <summary>
        /// Tests the Implementation method check for support email addresses contains expected attributes.
        /// #1
        /// </summary>
        [TestMethod]
        public void TestImplementationMethodCheckForSupportEmailAddressesContainsExpectedAttributes()
        {
            #region Arrange
            var serviceClass = ServiceClass;
            var serviceMethod = serviceClass.GetMethod("CheckForSupportEmailAddresses");
            #endregion Arrange

            #region Act
            var allAttributes = serviceMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the Implementation method common submit validation checks contains expected attributes.
        /// #2
        /// </summary>
        [TestMethod]
        public void TestImplementationMethodCommonSubmitValidationChecksContainsExpectedAttributes()
        {
            #region Arrange
            var serviceClass = ServiceClass;
            var serviceMethod = serviceClass.GetMethod("CommonSubmitValidationChecks");
            #endregion Arrange

            #region Act
            var allAttributes = serviceMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the Implementation method load file contents contains expected attributes.
        /// #3
        /// </summary>
        [TestMethod]
        public void TestImplementationMethodLoadFileContentsContainsExpectedAttributes()
        {
            #region Arrange
            var serviceClass = ServiceClass;
            var serviceMethod = serviceClass.GetMethod("LoadFileContents");
            #endregion Arrange

            #region Act
            var allAttributes = serviceMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the Implementation method cas login contains expected attributes.
        /// #4
        /// </summary>
        [TestMethod]
        public void TestImplementationMethodCasLoginContainsExpectedAttributes()
        {
            #region Arrange
            var serviceClass = ServiceClass;
            var serviceMethod = serviceClass.GetMethod("CasLogin");
            #endregion Arrange

            #region Act
            var allAttributes = serviceMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the Implementation method find kerb user contains expected attributes.
        /// #5
        /// </summary>
        [TestMethod]
        public void TestImplementationMethodFindKerbUserContainsExpectedAttributes()
        {
            #region Arrange
            var serviceClass = ServiceClass;
            var serviceMethod = serviceClass.GetMethod("FindKerbUser");
            #endregion Arrange

            #region Act
            var allAttributes = serviceMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the Implementation method send help request contains expected attributes.
        /// #6
        /// </summary>
        [TestMethod]
        public void TestImplementationMethodSendHelpRequestContainsExpectedAttributes()
        {
            #region Arrange
            var serviceClass = ServiceClass;
            var serviceMethod = serviceClass.GetMethod("SendHelpRequest");
            #endregion Arrange

            #region Act
            var allAttributes = serviceMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the implementation method build body contains expected attributes.
        /// #7
        /// </summary>
        [TestMethod]
        public void TestImplementationMethodBuildBodyContainsExpectedAttributes()
        {
            #region Arrange
            var serviceClass = ServiceClass;
            var serviceMethod = serviceClass.GetMethod("BuildBody");
            #endregion Arrange

            #region Act
            var allAttributes = serviceMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the implementation method filter cru email contains expected attributes.
        /// #8
        /// </summary>
        [TestMethod]
        public void TestImplementationMethodFilterCruEmailContainsExpectedAttributes()
        {
            #region Arrange
            var serviceClass = ServiceClass;
            var serviceMethod = serviceClass.GetMethod("FilterCruEmail");
            #endregion Arrange

            #region Act
            var allAttributes = serviceMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the implementation method get help email contains expected attributes.
        /// #9
        /// </summary>
        [TestMethod]
        public void TestImplementationMethodGetHelpEmailContainsExpectedAttributes()
        {
            #region Arrange
            var serviceClass = ServiceClass;
            var serviceMethod = serviceClass.GetMethod("GetHelpEmail");
            #endregion Arrange

            #region Act
            var allAttributes = serviceMethod.GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        #endregion Service Method Tests

        #endregion Reflection

        #region Mocks

        public class MockHttpPostedFileBase : HttpPostedFileBase
        {
            public override int ContentLength
            {
                get
                {
                    return 5;
                }
            }
            public override string FileName
            {
                get
                {
                    return "Mocked File Name";
                }
            }
            public override Stream InputStream
            {
                get
                {
                    var memStream = new MemoryStream(new byte[] { 4, 5, 6, 7, 8 });
                    return memStream;
                }
            }

            public override string ContentType
            {
                get
                {
                    return "Mocked Type";
                }
            }
        }

        #endregion Mocks
    }
}
