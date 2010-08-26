using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using HelpRequest.Core.Domain;
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
        

        #endregion Mapping Tests
        //[TestMethod]
        //public void Index()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    // Assert
        //    ViewDataDictionary viewData = result.ViewData;
        //    Assert.AreEqual("Welcome to ASP.NET MVC!", viewData["Message"]);
        //}

        //[TestMethod]
        //public void About()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.About() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //}
    }
}
