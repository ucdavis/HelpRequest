<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HelpRequest.Controllers.ViewModels.HelpTopicViewModel>" %>
<%@ Import Namespace="HelpRequest.Controllers" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Frequently Asked Question 
    <%if (!string.IsNullOrEmpty(Model.HelpTopic.AppFilter)){%>
    For Application: <%=Html.Encode(Model.HelpTopic.AppFilter) %> 
    <%}%>
    </h2>
    <h3><%=Html.Encode(Model.HelpTopic.Question) %></h3>

    <div id="Answer">
    <%=Html.HtmlEncode(Model.HelpTopic.Answer) %>
    </div>

    <p>
        <%= Html.ActionLink<HelpController>(a => a.Index(Model.AppName), "back to List") %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>