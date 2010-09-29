using System;
using System.Linq;
using HelpRequest.Core.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelpRequest.Tests.Interfaces
{
    [TestClass]
    public class EmailProviderTests
    {
        protected readonly Type ServiceClass = typeof(EmailProvider);
        protected readonly Type InterfaceClass = typeof(IEmailProvider);
        //protected EmailProvider EmailProvider;

        //public EmailProviderTests()
        //{
        //    EmailProvider = new EmailProvider();
        //}

        
        //I'm not going to test this, otherwise it would try to send an email...

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
            Assert.AreEqual(1, result.Count(), "It looks like a method was added or removed from the Service.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the service method check for support email addresses contains expected attributes.
        /// #1
        /// </summary>
        [TestMethod]
        public void TestServiceMethodSendEmailContainsExpectedAttributes()
        {
            #region Arrange
            var serviceClass = ServiceClass;
            var serviceMethod = serviceClass.GetMethod("SendEmail");
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
    }
}
