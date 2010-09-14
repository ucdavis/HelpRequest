<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HelpRequest.Controllers.ViewModels.TicketViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Submit Help Ticket
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Submit Help Ticket</h2>
    
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <%= Html.ClientSideValidation<Ticket>("*") %>
    <% using (Html.BeginForm("Submit", "Ticket", FormMethod.Post, new { @enctype = "multipart/form-data", @id = "SubmitForm" }))
       {%>
        <%= Html.AntiForgeryToken() %>
        <fieldset>
            <legend>Help Ticket Details</legend>
            <div id="nobullets">
            <ul>
            <% Html.RenderPartial("~/Views/Shared/SubmitForm.ascx"); %>
            </ul>
            </div>
            <p>
                <input type="submit" value="Send Ticket" />
            </p>
        </fieldset>

    <% } %>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>


