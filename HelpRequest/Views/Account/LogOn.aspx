<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HelpRequest.Controllers.ViewModels.GenericViewModel>" %>--%>
<%--<asp:Content Id="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Access Denied
</asp:Content>

<asp:Content Id="Content2" ContentPlaceHolderID="MainContent" runat="server">--%>

    <h2>Access Denied</h2>

    <p>
        You do not have access to view this page: <%= Html.Encode(TempData["URL"]) %>
    </p>
    <p>
        <a href="javascript:history.go(-1)" onMouseOver="self.status=document.referrer;return true">BACK</a> 
    </p>
    
<%--</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="PageHeader" runat="server">
</asp:Content>--%>

