<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HelpRequest.Controllers.ViewModels.TicketViewModel>" %>
<%@ Import Namespace="HelpRequest.Controllers" %>
<%@ Import Namespace="HelpRequest.Core.Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Submit Help Ticket
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Submit Help Ticket to College of Agricultural and Environmental Sciences Dean's Office</h2>
    <h3>Computing Resources Unit</h3>
    
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <%= Html.ClientSideValidation<Ticket>("Ticket") %>
    
    <% using (Html.BeginForm("PublicSubmit", "Ticket", FormMethod.Post, new { @enctype = "multipart/form-data", @id = "SubmitForm" }))
       {%>
        <%= Html.AntiForgeryToken() %>
        
        <fieldset>
            <legend>Help Ticket Details</legend>
            <div id="nobullets">
            <ul>
            <li>
                <label for="Ticket.FromEmail">Your Email:</label>
                <%= Html.TextBox("Ticket.FromEmail", string.Empty, new { style = "width: 300px", @class="required" })%>
                <%= Html.ValidationMessage("Ticket.FromEmail")%>
                <span class="EmailWarning" style="color:Orange">&nbsp</span>
                <%=Html.ActionLink<TicketController>(a => a.LogOnAndSubmit(Model.AppName, Model.PassedSubject), "Use Kerberos login to populate information.") %>
            </li>   
            <% Html.RenderPartial("~/Views/Shared/SubmitForm.ascx"); %>
            </ul>
            </div>
            <fieldset>
                <legend>If you don't see a recaptcha field here, you may need to enable it <br/>for your browser or use your Kerberous login instead.</legend>

                    <div class="form-group">
                        <div class="g-recaptcha" data-sitekey='<%=ConfigurationManager.AppSettings["NewRecaptchaPublicKey"] %>'></div>
                    </div>
            </fieldset>
            <p>
                <input type="submit" value="Send Ticket" />
            </p>
        </fieldset>

    <% } %>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
	    $(document).ready(function() {
	        $("input#Ticket_FromEmail").blur(function(event){
                if($(this).hasClass("warning")){
                    $(this).removeClass("warning");
                    $("span.EmailWarning").text("");
                }
                var emailVal = $(this).val().toLowerCase();                                    
                if(emailVal != null && emailVal != "" && emailVal.match(<%=StaticValues.EmailWarningRegEx %>) == null){
                    $(this).addClass("warning");
                    $("span.EmailWarning").text("This may be invalid");
                }
                    
            });
	    });
	</script>
    <script src="https://www.google.com/recaptcha/api.js"></script>
</asp:Content>


