using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpRequest.Controllers.Helpers;
using UCDArch.Web.Controller;

namespace HelpRequest.Controllers
{
    [LocServiceMessage("HelpRequest", ViewDataKey = "ServiceMessages", MessageServiceAppSettingsKey = "MessageServer")]
    public class ApplicationController : SuperController { }
}
