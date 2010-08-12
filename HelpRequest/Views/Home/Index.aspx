<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HelpRequest.Controllers.ViewModels.HomeViewModel>" %>
<%@ Import Namespace="HelpRequest.Controllers" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>

    <ul>
        <li>Review the Help FAQ to see if there is already a solution to your problem or question.</li>
        <li>Select Submit Help Ticket to send an email.</li>
        <%if (Model.ReturnUrl != null){%>
        <li> <%=Html.ActionLink<HomeController>(a => a.ReturnToCallingApplication(Model.ReturnUrl), string.Format("Return to {0}", Model.ReturnAppName)) %></li>

        <%}%>
    </ul>
</asp:Content>
