using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpRequest.Controllers.Services;
using HelpRequest.Core.Domain;
using HelpRequest.Core.Resources;
using HelpRequest.Tests.Core.Extensions;
using HelpRequest.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelpRequest.Tests.Interfaces
{
    [TestClass]
    public class TicketControllerServiceTests
    {
        protected readonly Type ServiceClass = typeof(TicketControllerService);
        protected readonly Type InterfaceClass = typeof(ITicketControllerService);
        protected TicketControllerService TicketControllerService;

        public TicketControllerServiceTests()
        {
            TicketControllerService = new TicketControllerService();
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
            Assert.AreEqual(5, result.Count(), "It looks like a method was added or removed from the Service.");
            #endregion Assert
        }

        #endregion Service Class Tests

        #region Service Method Tests

        /// <summary>
        /// Tests the controller contains expected number of public methods.
        /// </summary>
        [TestMethod]
        public void TestControllerContainsExpectedNumberOfPublicMethods()
        {
            #region Arrange
            var serviceClass = ServiceClass;
            #endregion Arrange

            #region Act
            var result = serviceClass.GetMethods().Where(a => a.DeclaringType == serviceClass);
            #endregion Act

            #region Assert
            Assert.AreEqual(5, result.Count(), "It looks like a method was added or removed from the Service.");
            #endregion Assert
        }
               
        /// <summary>
        /// Tests the service method check for support email addresses contains expected attributes.
        /// #1
        /// </summary>
        [TestMethod]
        public void TestServiceMethodCheckForSupportEmailAddressesContainsExpectedAttributes()
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
        /// Tests the service method common submit validation checks contains expected attributes.
        /// #2
        /// </summary>
        [TestMethod]
        public void TestServiceMethodCommonSubmitValidationChecksContainsExpectedAttributes()
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
        /// Tests the service method load file contents contains expected attributes.
        /// #3
        /// </summary>
        [TestMethod]
        public void TestServiceMethodLoadFileContentsContainsExpectedAttributes()
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
        /// Tests the service method cas login contains expected attributes.
        /// #4
        /// </summary>
        [TestMethod]
        public void TestServiceMethodCasLoginContainsExpectedAttributes()
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
        /// Tests the service method find kerb user contains expected attributes.
        /// #5
        /// </summary>
        [TestMethod]
        public void TestServiceMethodFindKerbUserContainsExpectedAttributes()
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
