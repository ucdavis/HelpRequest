<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HelpRequest.Controllers.ViewModels.TicketViewModel>" %>
<%@ Import Namespace="HelpRequest.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Submit Help Ticket
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Submit Help Ticket</h2>
    
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    
    <% using (Html.BeginForm("PublicSubmit", "Ticket", FormMethod.Post, new { @enctype = "multipart/form-data" }))
       {%>
        <%= Html.AntiForgeryToken() %>
        <fieldset>
            <legend>Help Ticket Details</legend>
            <div id="nobullets">
            <ul>
            <li>
                <label for="Ticket.FromEmail">Your Email:</label>
                <%= Html.TextBox("Ticket.FromEmail", string.Empty, new { style = "width: 300px" })%>
                <%= Html.ValidationMessage("Ticket.FromEmail", "*")%>
                <%=Html.ActionLink<TicketController>(a => a.LogOnAndSubmit(Model.AppName), "Use Kerberos login to populate information.") %>
            </li>   
            <% Html.RenderPartial("~/Views/Shared/SubmitForm.ascx"); %>
            </ul>
            </div>
            <p>
                <%= Html.GenerateCaptcha() %>
            </p>
            <p>
                <input type="submit" value="Send Ticket" />
            </p>
        </fieldset>

    <% } %>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>


