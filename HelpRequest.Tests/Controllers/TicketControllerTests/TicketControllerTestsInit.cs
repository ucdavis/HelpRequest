using System;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using HelpRequest.Controllers;
using HelpRequest.Controllers.Helpers;
using HelpRequest.Core.Abstractions;
using HelpRequest.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Rhino.Mocks;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Testing;

namespace HelpRequest.Tests.Controllers.TicketControllerTests
{
    [TestClass]
    public partial class TicketControllerTests : ControllerTestBase<TicketController>
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
                    const string url = "http://www.Sample.com/somefolder/getStuff.aspx?id=1&var2=abc&var3=55";
                    var uri = new Uri(url);

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
    }
}
