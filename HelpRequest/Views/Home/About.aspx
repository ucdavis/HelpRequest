<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HelpRequest.Controllers.ViewModels.GenericViewModel>" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About</h2>
    <ul>
        <li>Created July 12, 2010</li>
        <li>Created By Jason Sylvestre</li>
        <li>Version <%=Html.Encode(System.Reflection.Assembly.GetAssembly(ViewContext.Controller.GetType()).GetName().Version.ToString())%></li>
    </ul>
</asp:Content>
