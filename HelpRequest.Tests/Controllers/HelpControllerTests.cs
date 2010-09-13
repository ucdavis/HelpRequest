using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using HelpRequest.Controllers;
using HelpRequest.Controllers.Filters;
using HelpRequest.Controllers.ViewModels;
using HelpRequest.Core.Domain;
using HelpRequest.Tests.Core.Extensions;
using HelpRequest.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.Attributes;
using MvcContrib.TestHelper;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Testing;
using UCDArch.Web.Attributes;
using Rhino.Mocks;
using Rhino;

namespace HelpRequest.Tests.Controllers
{
    [TestClass]
    public class HelpControllerTests : ControllerTestBase<HelpController>
    {
        private readonly Type _controllerClass = typeof(HelpController);
        public IRepository<HelpTopic> HelpTopicRepository;

        #region Init

        protected override void SetupController()
        {
            HelpTopicRepository = FakeRepository<HelpTopic>();
            Controller = new TestControllerBuilder().CreateController<HelpController>(HelpTopicRepository);
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
            "~/Help/Index/?appName=Test".ShouldMapTo<HelpController>(a => a.Index("Test"), true);
        }

        /// <summary>
        /// Tests the create get mapping.
        /// </summary>
        [TestMethod]
        public void TestCreateGetMapping()
        {
            "~/Help/Create/?appName=Test".ShouldMapTo<HelpController>(a => a.Create("Test"), true);
        }

        /// <summary>
        /// Tests the create post mapping.
        /// </summary>
        [TestMethod]
        public void TestCreatePostMapping()
        {
            "~/Help/Create/?appName=Test".ShouldMapTo<HelpController>(a => a.Create(new HelpTopicViewModel(), "Test"), true);
        }

        /// <summary>
        /// Tests the edit get mapping.
        /// </summary>
        [TestMethod]
        public void TestEditGetMapping()
        {
            "~/Help/Edit/5?appName=Test".ShouldMapTo<HelpController>(a => a.Edit(5, "Test"), true);
        }

        /// <summary>
        /// Tests the edit post mapping.
        /// </summary>
        [TestMethod]
        public void TestEditPostMapping()
        {
            "~/Help/Edit/5?appName=Test".ShouldMapTo<HelpController>(a => a.Edit(5, new HelpTopic(), "Test"), true);
        }
        #endregion Mapping Tests

        #region Index Tests

        /// <summary>
        /// Tests the index returns view model with expected values.
        /// </summary>
        [TestMethod]
        public void TestIndexReturnsViewModelWithExpectedValues1()
        {
            #region Arrange
            var appName = "TestAppName";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { RoleNames.Admin });
            var helpTopics = new List<HelpTopic>();
            LoadHelpTopics(helpTopics, appName);
            ControllerRecordFakes.FakeHelpTopic(0, HelpTopicRepository, helpTopics);
            #endregion Arrange

            #region Act
            var result = Controller.Index(appName)
                .AssertViewRendered()
                .WithViewData<HelpTopicViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(11, result.HelpTopics.Count());
            Assert.AreEqual("TestAppName", result.AppName);
            Assert.IsTrue(result.IsUserAdmin);
            Assert.IsTrue(result.IsUserAuthorized);
            #endregion Assert		
        }

        /// <summary>
        /// Tests the index returns view model with expected values.
        /// </summary>
        [TestMethod]
        public void TestIndexReturnsViewModelWithExpectedValues2()
        {
            #region Arrange
            string appName = null;
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { RoleNames.Admin });
            var helpTopics = new List<HelpTopic>();
            LoadHelpTopics(helpTopics, appName);
            ControllerRecordFakes.FakeHelpTopic(0, HelpTopicRepository, helpTopics);
            #endregion Arrange

            #region Act
            var result = Controller.Index(appName)
                .AssertViewRendered()
                .WithViewData<HelpTopicViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(11, result.HelpTopics.Count());
            Assert.AreEqual(null, result.AppName);
            Assert.IsTrue(result.IsUserAdmin);
            Assert.IsTrue(result.IsUserAuthorized);
            #endregion Assert
        }

        /// <summary>
        /// Tests the index returns view model with expected values.
        /// </summary>
        [TestMethod]
        public void TestIndexReturnsViewModelWithExpectedValues3()
        {
            #region Arrange
            var appName = "TestAppName";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { RoleNames.User });
            var helpTopics = new List<HelpTopic>();
            LoadHelpTopics(helpTopics, appName);
            ControllerRecordFakes.FakeHelpTopic(0, HelpTopicRepository, helpTopics);
            #endregion Arrange

            #region Act
            var result = Controller.Index(appName)
                .AssertViewRendered()
                .WithViewData<HelpTopicViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.HelpTopics.Count());
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[0]));
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[1]));
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[2]));
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[4]));
            Assert.AreEqual("TestAppName", result.AppName);
            Assert.IsFalse(result.IsUserAdmin);
            Assert.IsTrue(result.IsUserAuthorized);
            #endregion Assert
        }

        /// <summary>
        /// Tests the index returns view model with expected values.
        /// </summary>
        [TestMethod]
        public void TestIndexReturnsViewModelWithExpectedValues4()
        {
            #region Arrange
            string appName = null;
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { RoleNames.User });
            var helpTopics = new List<HelpTopic>();
            LoadHelpTopics(helpTopics, appName);
            ControllerRecordFakes.FakeHelpTopic(0, HelpTopicRepository, helpTopics);
            #endregion Arrange

            #region Act
            var result = Controller.Index(appName)
                .AssertViewRendered()
                .WithViewData<HelpTopicViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.HelpTopics.Count());
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[0]));
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[1]));
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[2]));
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[4]));
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[8]));
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[10]));
            Assert.AreEqual(null, result.AppName);
            Assert.IsFalse(result.IsUserAdmin);
            Assert.IsTrue(result.IsUserAuthorized);
            #endregion Assert
        }

        /// <summary>
        /// Tests the index returns view model with expected values.
        /// </summary>
        [TestMethod]
        public void TestIndexReturnsViewModelWithExpectedValues5()
        {
            #region Arrange
            var appName = "TestAppName";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var helpTopics = new List<HelpTopic>();
            LoadHelpTopics(helpTopics, appName);
            ControllerRecordFakes.FakeHelpTopic(0, HelpTopicRepository, helpTopics);
            #endregion Arrange

            #region Act
            var result = Controller.Index(appName)
                .AssertViewRendered()
                .WithViewData<HelpTopicViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.HelpTopics.Count());
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[0]));
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[1]));
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[2]));
            Assert.AreEqual("TestAppName", result.AppName);
            Assert.IsFalse(result.IsUserAdmin);
            Assert.IsFalse(result.IsUserAuthorized);
            #endregion Assert
        }

        /// <summary>
        /// Tests the index returns view model with expected values.
        /// </summary>
        [TestMethod]
        public void TestIndexReturnsViewModelWithExpectedValues6()
        {
            #region Arrange
            string appName = null;
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { "" });
            var helpTopics = new List<HelpTopic>();
            LoadHelpTopics(helpTopics, appName);
            ControllerRecordFakes.FakeHelpTopic(0, HelpTopicRepository, helpTopics);
            #endregion Arrange

            #region Act
            var result = Controller.Index(appName)
                .AssertViewRendered()
                .WithViewData<HelpTopicViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.HelpTopics.Count());
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[0]));
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[1]));
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[2]));
            Assert.IsTrue(result.HelpTopics.Contains(helpTopics[10]));
            Assert.AreEqual(null, result.AppName);
            Assert.IsFalse(result.IsUserAdmin);
            Assert.IsFalse(result.IsUserAuthorized);
            #endregion Assert
        }


        #endregion Index Tests

        #region Create Tests

        #region Get Tests

        /// <summary>
        /// Tests the create get returns view model.
        /// </summary>
        [TestMethod]
        public void TestCreateGetReturnsViewModel1()
        {
            #region Arrange
            string appName = null;
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { RoleNames.Admin });
            var helpTopics = new List<HelpTopic>();
            LoadHelpTopics(helpTopics, appName);
            ControllerRecordFakes.FakeHelpTopic(0, HelpTopicRepository, helpTopics);
            #endregion Arrange

            #region Act
            var result = Controller.Create(appName)
                .AssertViewRendered()
                .WithViewData<HelpTopicViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(11, result.HelpTopics.Count());
            Assert.IsNotNull(result.HelpTopic);
            Assert.AreEqual(0, result.HelpTopic.Id);
            Assert.AreEqual(appName, result.HelpTopic.AppFilter);
            #endregion Assert		
        }

        /// <summary>
        /// Tests the create get returns view model.
        /// </summary>
        [TestMethod]
        public void TestCreateGetReturnsViewModel2()
        {
            #region Arrange
            string appName = "Test";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { RoleNames.Admin });
            var helpTopics = new List<HelpTopic>();
            LoadHelpTopics(helpTopics, appName);
            ControllerRecordFakes.FakeHelpTopic(0, HelpTopicRepository, helpTopics);
            #endregion Arrange

            #region Act
            var result = Controller.Create(appName)
                .AssertViewRendered()
                .WithViewData<HelpTopicViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(11, result.HelpTopics.Count());
            Assert.IsNotNull(result.HelpTopic);
            Assert.AreEqual(0, result.HelpTopic.Id);
            Assert.AreEqual(appName, result.HelpTopic.AppFilter);
            #endregion Assert
        }

        #endregion Get Tests

        #region Post Tests

        /// <summary>
        /// Tests the create post copies expected values and saves when valid.
        /// </summary>
        [TestMethod]
        public void TestCreatePostCopiesExpectedValuesAndSavesWhenValid()
        {
            #region Arrange
            var helpTopic = CreateValidEntities.HelpTopic(1);
            helpTopic.Answer = "NewAnswer";
            helpTopic.AppFilter = "NewAppFilter";
            helpTopic.AvailableToPublic = false;
            helpTopic.IsActive = false;
            helpTopic.IsVideo = true;
            helpTopic.NumberOfReads = 99;
            helpTopic.Question = "NewQuestion";
            helpTopic.VideoName = "NewVideoName";
            helpTopic.SetIdTo(99); //This will not get copied
            var viewModel = new HelpTopicViewModel();
            viewModel.HelpTopic = helpTopic;
            #endregion Arrange

            #region Act
            Controller.Create(viewModel, "NewAppFilter");
            #endregion Act

            #region Assert
            HelpTopicRepository.AssertWasCalled(a => a.EnsurePersistent(Arg<HelpTopic>.Is.Anything));
            var args = (HelpTopic)HelpTopicRepository.GetArgumentsForCallsMadeOn(a => a.EnsurePersistent(Arg<HelpTopic>.Is.Anything))[0][0];
            Assert.IsNotNull(args);
            Assert.AreEqual(args.Answer, "NewAnswer");
            Assert.AreEqual(args.AppFilter, "NewAppFilter");
            Assert.IsFalse(args.AvailableToPublic);
            Assert.IsFalse(args.IsActive);
            Assert.IsTrue(args.IsVideo);
            Assert.AreEqual(99, args.NumberOfReads);
            Assert.AreEqual(args.Question, "NewQuestion");
            Assert.AreEqual(args.VideoName, "NewVideoName");
            Assert.AreNotEqual(99, args.Id);
            #endregion Assert		
        }


        /// <summary>
        /// Tests the create post with invalid data does not save.
        /// </summary>
        [TestMethod]
        public void TestCreatePostWithInvalidDataDoesNotSave()
        {
            #region Arrange
            var helpTopic = CreateValidEntities.HelpTopic(1);
            helpTopic.Answer = "NewAnswer";
            helpTopic.AppFilter = "NewAppFilter";
            helpTopic.AvailableToPublic = false;
            helpTopic.IsActive = false;
            helpTopic.IsVideo = true;
            helpTopic.NumberOfReads = 99;
            helpTopic.Question = "NewQuestion";
            helpTopic.VideoName = null;
            var viewModel = new HelpTopicViewModel();
            viewModel.HelpTopic = helpTopic;
            #endregion Arrange
            
            #region Act
            var result = Controller.Create(viewModel, "NewAppFilter")
                .AssertViewRendered()
                .WithViewData<HelpTopicViewModel>();
            #endregion Act

            #region Assert
            HelpTopicRepository.AssertWasNotCalled(a => a.EnsurePersistent(Arg<HelpTopic>.Is.Anything));
            Controller.ModelState.AssertErrorsAre("IsVideoNeedsVideoName: VideoName required when IsVideo selected");
            Assert.IsNull(Controller.Message);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.HelpTopic.Question, "NewQuestion");
            #endregion Assert		
        }


        /// <summary>
        /// Tests the create post redirects when saves.
        /// </summary>
        [TestMethod]
        public void TestCreatePostRedirectsWhenSaves()
        {
            #region Arrange
            var helpTopic = CreateValidEntities.HelpTopic(1);
            helpTopic.Answer = "NewAnswer";
            helpTopic.AppFilter = "NewAppFilter";
            helpTopic.AvailableToPublic = false;
            helpTopic.IsActive = false;
            helpTopic.IsVideo = false;
            helpTopic.NumberOfReads = 99;
            helpTopic.Question = "NewQuestion";
            helpTopic.VideoName = null;
            var viewModel = new HelpTopicViewModel();
            viewModel.HelpTopic = helpTopic;
            #endregion Arrange

            #region Act
            var result = Controller.Create(viewModel, "NewAppFilter")
                .AssertActionRedirect()
                .ToAction<HelpController>(a => a.Index("NewAppFilter"));
            #endregion Act

            #region Assert
            Assert.AreEqual("Help Topic created", Controller.Message);
            HelpTopicRepository.AssertWasCalled(a => a.EnsurePersistent(Arg<HelpTopic>.Is.Anything));
            Assert.IsNotNull(result);
            Assert.AreEqual("NewAppFilter", result.RouteValues["appName"]);
            #endregion Assert		
        }
        #endregion Post Tests
        #endregion Create Tests

        #region Edit Tests

        #region Get Tests

        /// <summary>
        /// Tests the edit get redirects to index if help topic not found.
        /// </summary>
        [TestMethod] 
        public void TestEditGetRedirectsToIndexIfHelpTopicNotFound()
        {
            #region Arrange
            string appName = "SomeApp";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { RoleNames.Admin });
            var helpTopics = new List<HelpTopic>();
            LoadHelpTopics(helpTopics, appName);
            ControllerRecordFakes.FakeHelpTopic(0, HelpTopicRepository, helpTopics);
            #endregion Arrange

            #region Act
            var result = Controller.Edit(helpTopics.Count+1, appName)
                .AssertActionRedirect()
                .ToAction<HelpController>(a => a.Index(appName));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            Assert.AreEqual("Help Topic not found", Controller.Message);
            #endregion Assert		
        }


        /// <summary>
        /// Tests the edit get returns view when help topic is found.
        /// </summary>
        [TestMethod]
        public void TestEditGetReturnsViewWhenHelpTopicIsFound()
        {
            #region Arrange
            string appName = "SomeApp";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { RoleNames.Admin });
            var helpTopics = new List<HelpTopic>();
            LoadHelpTopics(helpTopics, appName);
            ControllerRecordFakes.FakeHelpTopic(0, HelpTopicRepository, helpTopics);
            #endregion Arrange

            #region Act
            var result = Controller.Edit(1, appName)
                .AssertViewRendered()
                .WithViewData<HelpTopicViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreSame(helpTopics[0], result.HelpTopic);
            #endregion Assert		
        }
        #endregion Get Tests

        #region Post Tests
        /// <summary>
        /// Tests the edit post redirects to index if help topic not found.
        /// </summary>
        [TestMethod]
        public void TestEditPostRedirectsToIndexIfHelpTopicNotFound()
        {
            #region Arrange
            string appName = "SomeApp";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { RoleNames.Admin });
            var helpTopics = new List<HelpTopic>();
            LoadHelpTopics(helpTopics, appName);
            ControllerRecordFakes.FakeHelpTopic(0, HelpTopicRepository, helpTopics);
            #endregion Arrange

            #region Act
            var result = Controller.Edit(helpTopics.Count + 1, new HelpTopic(), appName)
                .AssertActionRedirect()
                .ToAction<HelpController>(a => a.Index(appName));
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            Assert.AreEqual("Help Topic not found", Controller.Message);
            #endregion Assert
        }


        /// <summary>
        /// Tests the edit post copies expected values.
        /// </summary>
        [TestMethod]
        public void TestEditPostCopiesExpectedValues()
        {
            #region Arrange
            string appName = "SomeApp";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { RoleNames.Admin });
            var helpTopics = new List<HelpTopic>();
            LoadHelpTopics(helpTopics, appName);
            ControllerRecordFakes.FakeHelpTopic(0, HelpTopicRepository, helpTopics);
            var compareHelpTopic = new HelpTopic();
            var helpTopic = new HelpTopic();
            helpTopic.Answer = "NewAnswer";
            helpTopic.AppFilter = "NewAppFilter";
            compareHelpTopic.AvailableToPublic = helpTopics[0].AvailableToPublic;
            compareHelpTopic.IsActive = helpTopics[0].IsActive;
            compareHelpTopic.IsVideo = helpTopics[0].IsVideo;
            helpTopic.AvailableToPublic = !helpTopics[0].AvailableToPublic;
            helpTopic.IsActive = !helpTopics[0].IsActive;
            helpTopic.IsVideo = !helpTopics[0].IsVideo;
            helpTopic.NumberOfReads = 99;
            helpTopic.Question = "NewQuestion";
            helpTopic.VideoName = "NewVideoName";
            #endregion Arrange

            #region Act
            var result = Controller.Edit(1, helpTopic, appName)
                .AssertActionRedirect()
                .ToAction<HelpController>(a => a.Index(appName));
            #endregion Act

            #region Assert
            HelpTopicRepository.AssertWasCalled(a => a.EnsurePersistent(Arg<HelpTopic>.Is.Anything));
            var args = (HelpTopic)HelpTopicRepository.GetArgumentsForCallsMadeOn(a => a.EnsurePersistent(Arg<HelpTopic>.Is.Anything))[0][0];
            Assert.IsNotNull(args);
            Assert.AreEqual(args.Answer, "NewAnswer");
            Assert.AreEqual(args.AppFilter, "NewAppFilter");
            Assert.AreNotEqual(args.AvailableToPublic, compareHelpTopic.AvailableToPublic);
            Assert.AreNotEqual(args.IsActive, compareHelpTopic.IsActive);
            Assert.AreNotEqual(args.IsVideo, compareHelpTopic.IsVideo);
            Assert.AreEqual(99, args.NumberOfReads);
            Assert.AreEqual(args.Question, "NewQuestion");
            Assert.AreEqual(args.VideoName, "NewVideoName");
            Assert.IsNotNull(result);
            Assert.AreEqual(appName, result.RouteValues["appName"]);
            Assert.AreEqual("Help Topic saved", Controller.Message);
            HelpTopicRepository.AssertWasCalled(a=>a.EnsurePersistent(helpTopics[0]));
            #endregion Assert		
        }


        /// <summary>
        /// Tests the edit post does not save if invalid data.
        /// </summary>
        [TestMethod]
        public void TestEditPostDoesNotSaveIfInvalidData()
        {
            #region Arrange
            string appName = "SomeApp";
            Controller.ControllerContext.HttpContext = new MockHttpContext(0, new[] { RoleNames.Admin });
            var helpTopics = new List<HelpTopic>();
            LoadHelpTopics(helpTopics, appName);
            ControllerRecordFakes.FakeHelpTopic(0, HelpTopicRepository, helpTopics);
                        var compareHelpTopic = new HelpTopic();
            var helpTopic = new HelpTopic();
            helpTopic.Answer = "NewAnswer";
            helpTopic.AvailableToPublic = !helpTopics[0].AvailableToPublic;
            helpTopic.IsActive = !helpTopics[0].IsActive;
            helpTopic.IsVideo = true;
            helpTopic.NumberOfReads = 99;
            helpTopic.Question = "NewQuestion";
            helpTopic.VideoName = string.Empty;
            #endregion Arrange

            #region Act
            var result = Controller.Edit(1, helpTopic, appName)
                .AssertViewRendered()
                .WithViewData<HelpTopicViewModel>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            HelpTopicRepository.AssertWasNotCalled(a=>a.EnsurePersistent(Arg<HelpTopic>.Is.Anything));
            Controller.ModelState.AssertErrorsAre("IsVideoNeedsVideoName: VideoName required when IsVideo selected");
            Assert.IsNull(Controller.Message);
            #endregion Assert		
        }
        #endregion Post Tests
        #endregion Edit Tests

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
            //Assert.AreEqual(1, result.Count(), "It looks like a method was added or removed from the controller.");
            Assert.AreEqual(5, result.Count(), "Still need to add tests.");
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
        /// Tests the controller method create get contains expected attributes.
        /// #2
        /// </summary>
        [TestMethod]
        public void TestControllerMethodCreateGetContainsExpectedAttributes()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Create");
            #endregion Arrange

            #region Act
            var expectedAttribute = controllerMethod.ElementAt(0).GetCustomAttributes(true).OfType<AdminOnlyAttribute>();
            var allAttributes = controllerMethod.ElementAt(0).GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(1, expectedAttribute.Count(), "AdminOnlyAttribute not found");
            Assert.AreEqual(1, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the controller method create post contains expected attributes.
        /// #3
        /// </summary>
        [TestMethod]
        public void TestControllerMethodCreatePostContainsExpectedAttributes1()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Create");
            #endregion Arrange

            #region Act
            var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<AdminOnlyAttribute>();
            var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(1, expectedAttribute.Count(), "AdminOnlyAttribute not found");
            Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the controller method create post contains expected attributes.
        /// #3
        /// </summary>
        [TestMethod]
        public void TestControllerMethodCreatePostContainsExpectedAttributes2()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Create");
            #endregion Arrange

            #region Act
            var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<AcceptPostAttribute>();
            var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(1, expectedAttribute.Count(), "AcceptPostAttribute not found");
            Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the controller method create post contains expected attributes.
        /// #3
        /// </summary>
        [TestMethod]
        public void TestControllerMethodCreatePostContainsExpectedAttributes3()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Create");
            #endregion Arrange

            #region Act
            var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<ValidateInputAttribute>();
            var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(1, expectedAttribute.Count(), "ValidateInputAttribute not found");
           Assert.IsFalse(expectedAttribute.ElementAt(0).EnableValidation);
            Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the controller method edit get contains expected attributes.
        /// #4
        /// </summary>
        [TestMethod]
        public void TestControllerMethodEditGetContainsExpectedAttributes()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Edit");
            #endregion Arrange

            #region Act
            var expectedAttribute = controllerMethod.ElementAt(0).GetCustomAttributes(true).OfType<AdminOnlyAttribute>();
            var allAttributes = controllerMethod.ElementAt(0).GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(1, expectedAttribute.Count(), "AdminOnlyAttribute not found");
            Assert.AreEqual(1, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        /// <summary>
        /// Tests the controller method edit post contains expected attributes1.
        /// #5
        /// </summary>
        [TestMethod]
        public void TestControllerMethodEditPostContainsExpectedAttributes1()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Edit");
            #endregion Arrange

            #region Act
            var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<AdminOnlyAttribute>();
            var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(1, expectedAttribute.Count(), "AdminOnlyAttribute not found");
            Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }


        /// <summary>
        /// Tests the controller method edit post contains expected attributes2.
        /// #5
        /// </summary>
        [TestMethod]
        public void TestControllerMethodEditPostContainsExpectedAttributes2()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Edit");
            #endregion Arrange

            #region Act
            var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<AcceptPostAttribute>();
            var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(1, expectedAttribute.Count(), "AcceptPostAttribute not found");
            Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }


        /// <summary>
        /// Tests the controller method edit post contains expected attributes3.
        /// #5
        /// </summary>
        [TestMethod]
        public void TestControllerMethodEditPostContainsExpectedAttributes3()
        {
            #region Arrange
            var controllerClass = _controllerClass;
            var controllerMethod = controllerClass.GetMethods().Where(a => a.Name == "Create");
            #endregion Arrange

            #region Act
            var expectedAttribute = controllerMethod.ElementAt(1).GetCustomAttributes(true).OfType<ValidateInputAttribute>();
            var allAttributes = controllerMethod.ElementAt(1).GetCustomAttributes(true);
            #endregion Act

            #region Assert
            Assert.AreEqual(1, expectedAttribute.Count(), "ValidateInputAttribute not found");
            Assert.IsFalse(expectedAttribute.ElementAt(0).EnableValidation);
            Assert.AreEqual(3, allAttributes.Count(), "More than expected custom attributes found.");
            #endregion Assert
        }

        #endregion Controller Method Tests

        #endregion Reflection

        #region mocks
        /// <summary>
        /// Mock the Identity. Used for getting the current user name
        /// </summary>
        public class MockIdentity : IIdentity
        {
            public string AuthenticationType
            {
                get
                {
                    return "MockAuthentication";
                }
            }

            public bool IsAuthenticated
            {
                get
                {
                    return true;
                }
            }

            public string Name
            {
                get
                {
                    return "UserName";
                }
            }
        }


        /// <summary>
        /// Mock the Principal. Used for getting the current user name
        /// </summary>
        public class MockPrincipal : IPrincipal
        {
            IIdentity _identity;
            public bool RoleReturnValue { get; set; }
            public string[] UserRoles { get; set; }

            public MockPrincipal(string[] userRoles)
            {
                UserRoles = userRoles;
            }

            public IIdentity Identity
            {
                get { return _identity ?? (_identity = new MockIdentity()); }
            }

            public bool IsInRole(string role)
            {
                if (UserRoles.Contains(role))
                {
                    return true;
                }
                return false;
            }
        }


        /// <summary>
        /// Mock the HTTPContext. Used for getting the current user name
        /// </summary>
        public class MockHttpContext : HttpContextBase
        {
            private IPrincipal _user;
            private readonly int _count;
            public string[] UserRoles { get; set; }
            public MockHttpContext(int count, string[] userRoles)
            {
                _count = count;
                UserRoles = userRoles;
            }

            public override IPrincipal User
            {
                get { return _user ?? (_user = new MockPrincipal(UserRoles)); }
                set
                {
                    _user = value;
                }
            }

            public override HttpRequestBase Request
            {
                get
                {
                    return new MockHttpRequest(_count);
                }

            }
        }

        public class MockHttpRequest : HttpRequestBase
        {
            MockHttpFileCollectionBase Mocked { get; set; }

            public MockHttpRequest(int count)
            {
                Mocked = new MockHttpFileCollectionBase(count);
            }
            public override HttpFileCollectionBase Files
            {
                get
                {
                    return Mocked;
                }
            }
            //This will get past the code, but not allow an openId to be assigned to the transaction.
            public override HttpCookieCollection Cookies
            {
                get
                {
                    try
                    {
                        return new HttpCookieCollection();
                    }
                    catch (Exception)
                    {
                        return null;
                    }

                }
            }
            //This is for viewModel.SuccessLink = String.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, url.Action("PaymentSuccess", "Transaction"));
            public override Uri Url
            {
                get
                {
                    string url = "http://www.Sample.com/somefolder/getStuff.aspx?id=1&var2=abc&var3=55";
                    Uri uri = new Uri(url);

                    return uri;
                }
            }

        }


        public class MockHttpFileCollectionBase : HttpFileCollectionBase
        {
            public int Counter { get; set; }

            public MockHttpFileCollectionBase(int count)
            {
                Counter = count;
                for (int i = 0; i < count; i++)
                {
                    BaseAdd("Test" + (i + 1), new byte[] { 4, 5, 6, 7, 8 });
                }

            }

            public override int Count
            {
                get
                {
                    return Counter;
                }
            }
            public override HttpPostedFileBase Get(string name)
            {
                return new MockHttpPostedFileBase();
            }
            public override HttpPostedFileBase this[string name]
            {
                get
                {
                    return new MockHttpPostedFileBase();
                }
            }
            public override HttpPostedFileBase this[int index]
            {
                get
                {
                    return new MockHttpPostedFileBase();
                }
            }
        }

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
        }

        #endregion

        #region Helpers

        private void LoadHelpTopics(List<HelpTopic> helpTopics, string appName)
        {
            helpTopics.Add(CreateValidEntities.HelpTopic(1));
            helpTopics[0].AvailableToPublic = true;
            helpTopics[0].IsActive = true;
            helpTopics[0].AppFilter = null;

            helpTopics.Add(CreateValidEntities.HelpTopic(2));
            helpTopics[1].AvailableToPublic = true;
            helpTopics[1].IsActive = true;
            helpTopics[1].AppFilter = string.Empty;

            helpTopics.Add(CreateValidEntities.HelpTopic(3));
            helpTopics[2].AvailableToPublic = true;
            helpTopics[2].IsActive = true;
            helpTopics[2].AppFilter = appName;

            helpTopics.Add(CreateValidEntities.HelpTopic(4));
            helpTopics[3].AvailableToPublic = true;
            helpTopics[3].IsActive = true;
            helpTopics[3].AppFilter = "OtherApp";

            helpTopics.Add(CreateValidEntities.HelpTopic(5));
            helpTopics[4].AvailableToPublic = false;
            helpTopics[4].IsActive = true;
            helpTopics[4].AppFilter = appName;

            helpTopics.Add(CreateValidEntities.HelpTopic(6));
            helpTopics[5].AvailableToPublic = true;
            helpTopics[5].IsActive = false;
            helpTopics[5].AppFilter = appName;

            helpTopics.Add(CreateValidEntities.HelpTopic(7));
            helpTopics[6].AvailableToPublic = false;
            helpTopics[6].IsActive = false;
            helpTopics[6].AppFilter = appName;

            helpTopics.Add(CreateValidEntities.HelpTopic(8));
            helpTopics[7].AvailableToPublic = false;
            helpTopics[7].IsActive = false;
            helpTopics[7].AppFilter = null;

            helpTopics.Add(CreateValidEntities.HelpTopic(9));
            helpTopics[8].AvailableToPublic = false;
            helpTopics[8].IsActive = true;
            helpTopics[8].AppFilter = "HelpRequest";

            helpTopics.Add(CreateValidEntities.HelpTopic(10));
            helpTopics[9].AvailableToPublic = false;
            helpTopics[9].IsActive = false;
            helpTopics[9].AppFilter = "HelpRequest";

            helpTopics.Add(CreateValidEntities.HelpTopic(11));
            helpTopics[10].AvailableToPublic = true;
            helpTopics[10].IsActive = true;
            helpTopics[10].AppFilter = "HelpRequest";
        }


        #endregion Helpers
    }
}
