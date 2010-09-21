using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelpRequest.Core.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelpRequest.Tests.Interfaces
{
    [TestClass]
    public class EmailProviderTests
    {
        protected readonly Type ServiceClass = typeof(EmailProvider);
        protected readonly Type InterfaceClass = typeof(IEmailProvider);
        protected EmailProvider EmailProvider;

        public EmailProviderTests()
        {
            EmailProvider = new EmailProvider();
        }

        [TestMethod]
        public void TestAddTests()
        {
            #region Arrange

            Assert.Inconclusive("Need to add email provider tests");

            #endregion Arrange

            #region Act

            #endregion Act

            #region Assert

            #endregion Assert		
        }

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
            Assert.AreEqual(1, result.Count(), "It looks like a method was added or removed from the Service.");
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
            Assert.AreEqual(0, result.Count(), "It looks like a method was added or removed from the Service.");
            #endregion Assert
        }

        ///// <summary>
        ///// Tests the service method check for support email addresses contains expected attributes.
        ///// #1
        ///// </summary>
        //[TestMethod]
        //public void TestServiceMethodCheckForSupportEmailAddressesContainsExpectedAttributes()
        //{
        //    #region Arrange
        //    var serviceClass = ServiceClass;
        //    var serviceMethod = serviceClass.GetMethod("CheckForSupportEmailAddresses");
        //    #endregion Arrange

        //    #region Act
        //    var allAttributes = serviceMethod.GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}

        ///// <summary>
        ///// Tests the service method common submit validation checks contains expected attributes.
        ///// #2
        ///// </summary>
        //[TestMethod]
        //public void TestServiceMethodCommonSubmitValidationChecksContainsExpectedAttributes()
        //{
        //    #region Arrange
        //    var serviceClass = ServiceClass;
        //    var serviceMethod = serviceClass.GetMethod("CommonSubmitValidationChecks");
        //    #endregion Arrange

        //    #region Act
        //    var allAttributes = serviceMethod.GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}

        ///// <summary>
        ///// Tests the service method load file contents contains expected attributes.
        ///// #3
        ///// </summary>
        //[TestMethod]
        //public void TestServiceMethodLoadFileContentsContainsExpectedAttributes()
        //{
        //    #region Arrange
        //    var serviceClass = ServiceClass;
        //    var serviceMethod = serviceClass.GetMethod("LoadFileContents");
        //    #endregion Arrange

        //    #region Act
        //    var allAttributes = serviceMethod.GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}

        ///// <summary>
        ///// Tests the service method cas login contains expected attributes.
        ///// #4
        ///// </summary>
        //[TestMethod]
        //public void TestServiceMethodCasLoginContainsExpectedAttributes()
        //{
        //    #region Arrange
        //    var serviceClass = ServiceClass;
        //    var serviceMethod = serviceClass.GetMethod("CasLogin");
        //    #endregion Arrange

        //    #region Act
        //    var allAttributes = serviceMethod.GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}

        ///// <summary>
        ///// Tests the service method find kerb user contains expected attributes.
        ///// #5
        ///// </summary>
        //[TestMethod]
        //public void TestServiceMethodFindKerbUserContainsExpectedAttributes()
        //{
        //    #region Arrange
        //    var serviceClass = ServiceClass;
        //    var serviceMethod = serviceClass.GetMethod("FindKerbUser");
        //    #endregion Arrange

        //    #region Act
        //    var allAttributes = serviceMethod.GetCustomAttributes(true);
        //    #endregion Act

        //    #region Assert
        //    Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
        //    #endregion Assert
        //}

        #endregion Service Method Tests

        #endregion Reflection
    }
}
