﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HelpRequest.Controllers.ViewModels.GenericViewModel>" %>
<%@ Import Namespace="HelpRequest.Core.Resources" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About</h2>
    <ul>
        <%--<li>Version <%=Html.Encode(System.Reflection.Assembly.GetAssembly(ViewContext.Controller.GetType()).GetName().Version.ToString())%></li>--%>
        <li>Version <%: ViewData[StaticValues.VersionKey]%></li>
        <li>Developed By The College Of Agricultural And Environmental Science Dean's Office</li>
    </ul>
</asp:Content>
