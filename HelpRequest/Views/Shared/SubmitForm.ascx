<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<HelpRequest.Controllers.ViewModels.TicketViewModel>" %>
<%@ Import Namespace="HelpRequest.Core.Resources" %>

	<script type="text/javascript">
	    $(document).ready(function() {

	        $("img#availableDatesAddButton").click(function(event) {
	            var input = $("<input>").attr("id", "avDates").attr("name", "avDates").val($("input#availableDatesInput").val());
	            input.attr("type", "text");
	            input.css("cursor", "pointer");
	            input.dblclick(function(event) { $(this).remove(); });
	            $("div#availableDatesContainer").append(input);

	            // blank the input
	            $("input#availableDatesInput").val("");
	        });

	        //	        $("input#avDates").click(function(event) {
	        //	            $(this).remove();
	        //	        });
	        $("input#availableDatesInput").bt('You must click the plus button to add more dates. Double click the added item to remove it.', {positions: 'top' });
	        $("input#avDates").click(function(event) {
	            $(this).remove();
	        });

	        $("img#emailCCsInputAddButton").click(function(event) {
	            var input = $("<input>").attr("id", "emailCCs").attr("name", "emailCCs").val($("input#emailCCsInput").val());
	            input.attr("type", "text");
	            input.css("cursor", "pointer");
	            input.dblclick(function(event) { $(this).remove(); });
	            $("div#emailCCsContainer").append(input);

	            // blank the input
	            $("input#emailCCsInput").val("");
	        });

	        $("input#emailCCsInput").bt('You must click the plus button to add more email CC. Double click the added item to remove it.', {positions: 'top' });
	        $("input#emailCCs").dblclick(function(event) {
	            $(this).remove();
	        });

	        $("select#Ticket_UrgencyLevel").bt('<b>Non-Critical Issue:</b> Annoyances or other low priority requests.<br/><b>Scheduled Requests:</b> Heads up for future action.<br/><b>Workaround Available:</b> Alternative solutions exist to technical problem.<br/><b>Work Stoppages:</b> A technical problem preventing you from getting your job done.<br/><b>Critical:</b> A work stoppage for more than one person. ', { width: '550px' });
	        <%if(string.IsNullOrEmpty(Model.AppName)) {%>
	            $("select#Ticket_SupportDepartment").bt('<b>Computer Support:</b> (Tom Pomroy, Shuka Smith, Uwe Rossbach, Student Assistants)<br/><b>Web Site Support:</b> (Tyler Randles and Trish Ang)<br/><b>Programming Support:</b> (Scott Kirkland, Alan Lai, Ken Taylor, Jason Sylvestre)', { width: '550px' });
	         <%} %>
            <%else{%>
	            $("select#Ticket_SupportDepartment").bt('<b>Programming Support:</b> (Scott Kirkland, Alan Lai, Ken Taylor, Jason Sylvestre)', { width: '550px' });
	        <%
           }%>
	        $("input#Ticket_ForWebSite").bt('You need the http:// or https:// at the start for a valid URL. For example: http://www.ucdavis.edu/index.html');
	        $("input#uploadAttachment").bt('The maximum attachment size is 4 Meg.');

	        if ($("select#Ticket_SupportDepartment").val() == "Web Site Support") {
	            $("span#ForWebSite").show();
	        }
	        if ($("select#Ticket_SupportDepartment").val() == "Programming Support") {
	            $("span#ForApplication").show();
	        }

	        $("select#Ticket_SupportDepartment").change(function(event) {
	            if ($("select#Ticket_SupportDepartment").val() == "Web Site Support") {
	                $("span#ForWebSite").show();
	            }
	            else {
	                $("input#Ticket_ForWebSite").val("");
	                $("span#ForWebSite").hide();
	            }
	            if ($("select#Ticket_SupportDepartment").val() == "Programming Support") {
	                $("span#ForApplication").show();
	            }
	            else {
	                $("input#ForApplication").val("");
	                $("span#ForApplication").hide();
	            }
	        });
	        
//	        $("form").validate({
//                errorElement:"span",            // set the tag for the item that contains the message
//                errorClass:"field-validation-error",            // set the class on that tag of the notification tag
//                success:function(label){        // function to execute on passing
//                    label.addClass("passed");   // add the passed class
//                }
//            });

//            // validate the controls on blur
//            $("input").blur(function(){
//                $("form").validate().element(this);
//            });
	        
            $("input#Ticket_FromEmail").blur(function(event){
                if($(this).hasClass("warning")){
                    $(this).removeClass("warning");
                    $("span.field-validation-warning").text("");
                }
                var emailVal = $(this).val().toLowerCase();                                    
                if(emailVal != null && emailVal != "" && emailVal.match(<%=StaticValues.EmailWarningRegEx %>) == null){
                    $(this).addClass("warning");
                    $("span.field-validation-warning").text("This may be invalid");
                }
                    
            });

	    });
	    
	    
	</script>
	
	        <li>
                <label for="Ticket.UrgencyLevel">Urgency Level:</label>  
                <%= this.Select("Ticket.UrgencyLevel").Options(Model.Urgency).FirstOption("--Select a Urgency Level--")
                        .Selected(Model.Ticket != null ? Model.Ticket.UrgencyLevel : string.Empty)%>
                <%= Html.ValidationMessage("Ticket.UrgencyLevel", "*")%>
            </li>
            <%if(string.IsNullOrEmpty(Model.AppName)) {%>
            <li>
                <label for="Ticket.SupportDepartment">Support Department:</label>  
                <%= this.Select("Ticket.SupportDepartment").Options(Model.SupportDepartment).FirstOption("--Select a Support Department--")
                    .Selected(Model.Ticket != null ? Model.Ticket.SupportDepartment : string.Empty) %>
                <%= Html.ValidationMessage("Ticket.SupportDepartment", "*")%>
            </li>
            <%} %>
            <%else {%>
            <li>
                <label for="Ticket.SupportDepartment">Support Department:</label>  
                <%= this.Select("Ticket.SupportDepartment").Options(Model.SupportDepartment)
                                                                 .Selected("Application Support")%>
                <%= Html.ValidationMessage("Ticket.SupportDepartment", "*")%>
            </li>            
            <%} %>
            <span id="ForWebSite" style="display: none">
                <li>
                    <label for="Ticket.ForWebSite">Web Site Address:</label>
                    <%= Html.TextBox("Ticket.ForWebSite", string.Empty, new { style = "width: 500px" })%>
                    <%= Html.ValidationMessage("Ticket.ForWebSite", "*")%>
                </li>
            </span>
            <span id="ForApplication" style="display: none">
                <li>
                    <label for="Ticket.ForApplication">For Application:</label>
                    <%= Html.TextBox("Ticket.ForApplication", Model.AppName, new { style = "width: 500px" })%>
                    <%= Html.ValidationMessage("Ticket.ForApplication", "*")%>
                </li>
            </span>
            <li>
            <label for="availableDatesInput">Your Available Dates and Times:</label>            
            <input type="text" id="availableDatesInput" name="availableDatesInput"/>  <img id="availableDatesAddButton" src="<%= Url.Content("~/Images/plus.png") %>" style="height:15px; width: 15px" />
            </li>
            <li>
            <div id="availableDatesContainer">
            
                <% if (Model.Ticket != null){
                    foreach (var avDate in Model.Ticket.Availability){ %>
                        <input type="text" id="avDates" name="avDates" style="cursor:pointer" value='<%= avDate %>' />
                    <% }
                } %>
            
            </div>
            </li>
            <li>
            <label for="emailCCsInput">Carbon Copies:</label>            
            <input type="text" id="emailCCsInput" name="emailCCsInput" style = "width: 300px"/>  <img id="emailCCsInputAddButton" src="<%= Url.Content("~/Images/plus.png") %>" style="height:15px; width: 15px" />
            </li>
            <li>
            <div id="emailCCsContainer">
            
                <% if (Model.Ticket != null){
                    foreach (var emailCC in Model.Ticket.EmailCCs){ %>
                        <input type="text" id="emailCCs" name="emailCCs" style="cursor:pointer; width:300px" value='<%= emailCC %>' />
                    <% }
                } %>
            
            </div>
            </li>
            <li >
                <%=this.FileUpload("uploadAttachment").Label("Add Attachment:")%>
            </li>
            <li>
                <label for="Ticket.Subject">Subject:</label>
                <%= Html.TextBox("Ticket.Subject",string.Empty, new { style = "width: 500px" })%>
                <%= Html.ValidationMessage("Ticket.Subject", "*")%>
            </li>
            <li>
                <label for="Ticket.MessageBody">MessageBody:</label>
                <%= Html.TextArea("Ticket.MessageBody", new { style = "height:225px; width: 700px" })%>
                <%= Html.ValidationMessage("Ticket.MessageBody", "*")%>
            </li>

            