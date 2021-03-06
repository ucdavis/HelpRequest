﻿using System.Linq;
using System.Web.Mvc;
using HelpRequest.Controllers;
using HelpRequest.Controllers.Filters;
using HelpRequest.Controllers.Helpers;
using HelpRequest.Core.Domain;
using HelpRequest.Tests.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UCDArch.Web.Attributes;

namespace HelpRequest.Tests.Controllers.TicketControllerTests
{
    //public partial class TicketControllerTests
    //{
    //    #region Mapping Tests
    //    /// <summary>
    //    /// Tests the log on and submit mapping.
    //    /// </summary>
    //    [TestMethod]
    //    public void TestLogOnAndSubmitMapping()
    //    {
    //        "~/Ticket/LogOnAndSubmit/?appName=Test".ShouldMapTo<TicketController>(a => a.LogOnAndSubmit("Test", "Test2"), true);
    //    }

    //    /// <summary>
    //    /// Tests the submit redirect mapping.
    //    /// </summary>
    //    [TestMethod]
    //    public void TestSubmitRedirectMapping()
    //    {
    //        "~/Ticket/SubmitRedirect/?appName=Test".ShouldMapTo<TicketController>(a => a.SubmitRedirect("Test", "SubjectLine"), true);
    //    }

    //    /// <summary>
    //    /// Tests the submit get mapping.
    //    /// </summary>
    //    [TestMethod]
    //    public void TestSubmitGetMapping()
    //    {
    //        "~/Ticket/Submit/?appName=Test".ShouldMapTo<TicketController>(a => a.Submit("Test", "SubjectLine"), true);
    //    }

    //    /// <summary>
    //    /// Tests the submit post mapping.
    //    /// </summary>
    //    [TestMethod]
    //    public void TestSubmitPostMapping()
    //    {
    //        "~/Ticket/Submit/?appName=Test".ShouldMapTo<TicketController>(a => a.Submit(new Ticket(), null, null, null, "Test", "SubjectLine", null, null), true);
    //    }

    //    /// <summary>
    //    /// Tests the public submit get mapping.
    //    /// </summary>
    //    [TestMethod]
    //    public void TestPublicSubmitGetMapping()
    //    {
    //        "~/Ticket/PublicSubmit/?appName=Test".ShouldMapTo<TicketController>(a => a.PublicSubmit("Test", "SubjectLine"), true);
    //    }

    //    /// <summary>
    //    /// Tests the public submit post mapping.
    //    /// </summary>
    //    [TestMethod]
    //    public void TestPublicSubmitPostMapping()
    //    {
    //        "~/Ticket/PublicSubmit/?appName=Test".ShouldMapTo<TicketController>(a => a.PublicSubmit(new Ticket(), null,null,null,true,null,null,null,null), true);
    //    }
    //    #endregion Mapping Tests

    //    #region Reflection
    //    #region Controller Class Tests

    //    /// <summary>
    //    /// Tests the controller inherits from application controller.
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerInheritsFromApplicationController()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        #endregion Arrange

    //        #region Act
    //        Assert.IsNotNull(controllerClass.BaseType);
    //        var result = controllerClass.BaseType.Name;
    //        #endregion Act

    //        #region Assert
    //        Assert.AreEqual("ApplicationController", result);
    //        #endregion Assert
    //    }

    //    /// <summary>
    //    /// Tests the controller has three attributes.
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerHasThreeAttributes()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        #endregion Arrange

    //        #region Act
    //        var result = controllerClass.GetCustomAttributes(true);
    //        #endregion Act

    //        #region Assert
    //        Assert.AreEqual(3, result.Count());
    //        #endregion Assert
    //    }

    //    /// <summary>
    //    /// Tests the controller has transaction attribute.
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerHasTransactionAttribute()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        #endregion Arrange

    //        #region Act
    //        var result = controllerClass.GetCustomAttributes(true).OfType<UseTransactionsByDefaultAttribute>();
    //        #endregion Act

    //        #region Assert
    //        Assert.IsTrue(result.Count() > 0, "UseTransactionsByDefaultAttribute not found.");
    //        #endregion Assert
    //    }

    //    /// <summary>
    //    /// Tests the controller has anti forgery token attribute.
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerHasAntiForgeryTokenAttribute()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        #endregion Arrange

    //        #region Act
    //        var result = controllerClass.GetCustomAttributes(true).OfType<UseAntiForgeryTokenOnPostByDefault>();
    //        #endregion Act

    //        #region Assert
    //        Assert.IsTrue(result.Count() > 0, "UseAntiForgeryTokenOnPostByDefault not found.");
    //        #endregion Assert
    //    }

    //    [TestMethod]
    //    public void TestControllerHasLocServiceMessageAttribute()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        #endregion Arrange

    //        #region Act
    //        var result = controllerClass.GetCustomAttributes(true).OfType<LocServiceMessageAttribute>();
    //        #endregion Act

    //        #region Assert
    //        Assert.IsTrue(result.Count() > 0, "LocServiceMessageAttribute not found.");
    //        #endregion Assert
    //    }

    //    #endregion Controller Class Tests

    //    #region Controller Method Tests

    //    /// <summary>
    //    /// Tests the controller contains expected number of public methods.
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerContainsExpectedNumberOfPublicMethods()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        #endregion Arrange

    //        #region Act
    //        var result = controllerClass.GetMethods().Where(a => a.DeclaringType == controllerClass);
    //        #endregion Act

    //        #region Assert
    //        Assert.AreEqual(6, result.Count(), "It looks like a method was added or removed from the controller.");
    //        #endregion Assert
    //    }

    //    /// <summary>
    //    /// Tests the controller method log on and submit contains expected attributes.
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerMethodLogOnAndSubmitContainsExpectedAttributes()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        var controllerMethod = controllerClass.GetMethod("LogOnAndSubmit");
    //        #endregion Arrange

    //        #region Act
    //        var allAttributes = controllerMethod.GetCustomAttributes(true);
    //        #endregion Act

    //        #region Assert
    //        Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
    //        #endregion Assert
    //    }

    //    /// <summary>
    //    /// Tests the controller method submit redirect contains expected attributes.
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerMethodSubmitRedirectContainsExpectedAttributes()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        var controllerMethod = controllerClass.GetMethod("SubmitRedirect");
    //        #endregion Arrange

    //        #region Act
    //        var allAttributes = controllerMethod.GetCustomAttributes(true);
    //        #endregion Act

    //        #region Assert
    //        Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
    //        #endregion Assert
    //    }

    //    /// <summary>
    //    /// Tests the controller method submit get contains expected attributes.
    //    /// #3
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerMethodSubmitGetContainsExpectedAttributes()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Submit");
    //        #endregion Arrange

    //        #region Act
    //        var expectedAttribute = controllerMethod.ElementAt(0).GetCustomAttributes(true).OfType<AuthorizeAttribute>();
    //        var allAttributes = controllerMethod.ElementAt(0).GetCustomAttributes(true);
    //        #endregion Act

    //        #region Assert
    //        Assert.AreEqual(1, expectedAttribute.Count(), "AuthorizeAttribute not found");
    //        Assert.AreEqual(1, allAttributes.Count(), "More than expected custom attributes found.");
    //        #endregion Assert
    //    }

    //    /// <summary>
    //    /// Tests the controller method submit post contains expected attributes.
    //    /// #4
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerMethodSubmitPostContainsExpectedAttributes1()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Submit");
    //        #endregion Arrange

    //        #region Act
    //        var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<AuthorizeAttribute>();
    //        var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
    //        #endregion Act

    //        #region Assert
    //        Assert.AreEqual(1, expectedAttribute.Count(), "AuthorizeAttribute not found");
    //        Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
    //        #endregion Assert
    //    }

    //    /// <summary>
    //    /// Tests the controller method submit post contains expected attributes.
    //    /// #4
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerMethodSubmitPostContainsExpectedAttributes2()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Submit");
    //        #endregion Arrange

    //        #region Act
    //        var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<HttpPostAttribute>();
    //        var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
    //        #endregion Act

    //        #region Assert
    //        Assert.AreEqual(1, expectedAttribute.Count(), "HttpPostAttribute not found");
    //        Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
    //        #endregion Assert
    //    }

    //    /// <summary>
    //    /// Tests the controller method submit post contains expected attributes.
    //    /// #4
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerMethodSubmitPostContainsExpectedAttributes3()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Submit");
    //        #endregion Arrange

    //        #region Act
    //        var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<ValidateInputAttribute>();
    //        var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
    //        #endregion Act

    //        #region Assert
    //        Assert.AreEqual(1, expectedAttribute.Count(), "ValidateInputAttribute not found");
    //        Assert.IsFalse(expectedAttribute.ElementAt(0).EnableValidation);
    //        Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
    //        #endregion Assert
    //    }

    //    /// <summary>
    //    /// Tests the controller method public submit get contains expected attributes.
    //    /// #5
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerMethodPublicSubmitGetContainsExpectedAttributes()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "PublicSubmit");
    //        #endregion Arrange

    //        #region Act
    //        //var expectedAttribute = controllerMethod.ElementAt(0).GetCustomAttributes(true).OfType<AuthorizeAttribute>();
    //        var allAttributes = controllerMethod.ElementAt(0).GetCustomAttributes(true);
    //        #endregion Act

    //        #region Assert
    //        //Assert.AreEqual(1, expectedAttribute.Count(), "AuthorizeAttribute not found");
    //        Assert.AreEqual(0, allAttributes.Count(), "More than expected custom attributes found.");
    //        #endregion Assert
    //    }

    //    /// <summary>
    //    /// Tests the controller method public submit post contains expected attributes.
    //    /// #6
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerMethodPublicSubmitPostContainsExpectedAttributes1()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "PublicSubmit");
    //        #endregion Arrange

    //        #region Act
    //        var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<ValidateInputAttribute>();
    //        var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
    //        #endregion Act

    //        #region Assert
    //        Assert.AreEqual(1, expectedAttribute.Count(), "ValidateInputAttribute not found");
    //        Assert.IsFalse(expectedAttribute.ElementAt(0).EnableValidation);
    //        Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
    //        #endregion Assert
    //    }

    //    /// <summary>
    //    /// Tests the controller method public submit post contains expected attributes.
    //    /// #6
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerMethodPublicSubmitPostContainsExpectedAttributes2()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "PublicSubmit");
    //        #endregion Arrange

    //        #region Act
    //        var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<HttpPostAttribute>();
    //        var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
    //        #endregion Act

    //        #region Assert
    //        Assert.AreEqual(1, expectedAttribute.Count(), "HttpPostAttribute not found");
    //        Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
    //        #endregion Assert
    //    }

    //    /// <summary>
    //    /// Tests the controller method public submit post contains expected attributes.
    //    /// #6
    //    /// </summary>
    //    [TestMethod]
    //    public void TestControllerMethodPublicSubmitPostContainsExpectedAttributes3()
    //    {
    //        #region Arrange
    //        var controllerClass = ControllerClass;
    //        var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "PublicSubmit");
    //        #endregion Arrange

    //        #region Act
    //        var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<CaptchaValidatorAttribute>();
    //        var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
    //        #endregion Act

    //        #region Assert
    //        Assert.AreEqual(1, expectedAttribute.Count(), "CaptchaValidatorAttribute not found");
    //        Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
    //        #endregion Assert
    //    }
    //    #endregion Controller Method Tests

    //    #endregion Reflection
    //}
}
