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
	            $("select#Ticket_SupportDepartment").bt('<b>Computer Support:</b> (Shuka Smith, Jacqueline Emerson, Darrell Joe, Student Assistants)<br/><b>Web Site Support:</b> (Calvin Doval, Student Assistants)<br/><b>Programming Support:</b> (Scott Kirkland, John Knoll, Ken Taylor, Jason Sylvestre)', { width: '550px' });
	        <%} %>
            <%else{%>
	            $("select#Ticket_SupportDepartment").bt('<b>Programming Support:</b> (Scott Kirkland, John Knoll, Ken Taylor, Jason Sylvestre)', { width: '550px' });
                $("input#Ticket_ForApplication").attr('readonly', true).addClass("ReadOnly");
                <%if(!string.IsNullOrWhiteSpace(Model.PassedSubject)) {%>                    
                    $("input#Ticket_Subject").attr('readonly', true).addClass("ReadOnly");
                <%}%>
	        <%}%>
	        $("input#Ticket_ForWebSite").bt('You need the http:// or https:// at the start for a valid URL. For example: http://www.ucdavis.edu/index.html');
	        $("input#Ticket_YourPhoneNumber").bt('Call back phone number so we can contact you directly.', {positions: 'right' });
	        $("input#Ticket_LocationOfProblem").bt('The location of the problem in case we need to physically investigate', {positions: 'right' });
	        $("input#uploadAttachment").bt('The maximum attachment size is 4 Meg.');
            
	            switch ($("select#Ticket_SupportDepartment").val()) {
	            case "Web Site Support":
	                $("span#ForApplication").hide();
	                $("span#ForComputerSupport").hide();
	                $("span#ForWebSite").show();
	                $("input#Ticket_ForWebSite").addClass("required");
	                $("input#Ticket_YourPhoneNumber").removeClass("required");
	                $("span#available-dates-container").show();
	                break;
	            case "Programming Support":
	                $("span#ForWebSite").hide();
	                $("span#ForComputerSupport").hide();
	                $("span#ForApplication").show();
	                $("input#Ticket_ForWebSite").removeClass("required");
	                $("input#Ticket_YourPhoneNumber").removeClass("required");
	                $("span#available-dates-container").hide();
	                break;
	            case "Computer Support":
	                $("span#ForWebSite").hide();
	                $("span#ForApplication").hide();
	                $("span#ForComputerSupport").show();
	                $("input#Ticket_ForWebSite").removeClass("required");
	                //$("input#Ticket_YourPhoneNumber").addClass("required");
	                $("span#available-dates-container").show();
	                break;
	            default:
	                $("span#ForWebSite").hide();
	                $("span#ForApplication").hide();
	                $("span#ForComputerSupport").hide();
	                $("input#Ticket_ForWebSite").removeClass("required");
	                $("input#Ticket_YourPhoneNumber").removeClass("required");
	                $("span#available-dates-container").show();
	                break;	                
	            }
	        $("select#Ticket_SupportDepartment").change(function(event) {
	            switch ($("select#Ticket_SupportDepartment").val()) {
	            case "Web Site Support":
	                $("span#ForApplication").hide();
	                $("span#ForComputerSupport").hide();
	                $("span#ForWebSite").show();
	                $("input#Ticket_ForWebSite").addClass("required");
	                $("input#Ticket_YourPhoneNumber").removeClass("required");
	                $("span#available-dates-container").show();
	                break;
	            case "Programming Support":
	                $("span#ForWebSite").hide();
	                $("span#ForComputerSupport").hide();
	                $("span#ForApplication").show();
	                $("input#Ticket_ForWebSite").removeClass("required");
	                $("input#Ticket_YourPhoneNumber").removeClass("required");
	                $("span#available-dates-container").hide();
	                break;
	            case "Computer Support":
	                $("span#ForWebSite").hide();
	                $("span#ForApplication").hide();
	                $("span#ForComputerSupport").show();
	                $("input#Ticket_ForWebSite").removeClass("required");
	                //$("input#Ticket_YourPhoneNumber").addClass("required");
	                $("span#available-dates-container").show();
	                break;
	            default:
	                $("span#ForWebSite").hide();
	                $("span#ForApplication").hide();
	                $("span#ForComputerSupport").hide();
	                $("input#Ticket_ForWebSite").removeClass("required");
	                $("input#Ticket_YourPhoneNumber").removeClass("required");
	                $("span#available-dates-container").show();
	                break;	                
	            }
	            	            
	        });
	        
            // do some client side validation on the dynamic fields
            $("form#SubmitForm").validate({
                errorElement:"span",            // set the tag for the item that contains the message
                errorClass:"failed",            // set the class on that tag of the notification tag
                success:function(label){        // function to execute on passing
                    label.addClass("passed");   // add the passed class
                }
            });

            // validate the controls on blur
            $("input").blur(function(){
                $("form#SubmitForm").validate().element(this);
            });
	        
	        $("input#Ticket_ForWebSite").blur(function(event){
	            if ($("select#Ticket_SupportDepartment").val() == "Web Site Support") {
                    if($(this).hasClass("warning")){
                        $(this).removeClass("warning");
                        $("span.WebSiteWarning").text("");
                    }
                    var webVal = $(this).val().toLowerCase();                                
                    if(webVal != null && webVal != "" && webVal.match(/<%=StaticValues.WebSiteRegEx %>/) == null){
                        $(this).addClass("warning");
                        $("span.WebSiteWarning").text("This may be invalid");
                    }
                }                   
	        });   	    
        });   	    
	</script>
            <%= Html.HiddenFor(a => a.PassedSubject) %>
            <%= Html.HiddenFor(a => a.AppName) %>
	        <li>
                <label for="Ticket.UrgencyLevel">Urgency Level:</label>  
                <%= this.Select("Ticket.UrgencyLevel").Options(Model.Urgency)
                        .Selected(Model.Ticket != null ? Model.Ticket.UrgencyLevel : string.Empty)%>
                <%= Html.ValidationMessage("Ticket.UrgencyLevel")%>
            </li>
            <%if(string.IsNullOrEmpty(Model.AppName)) {%>
            <li>
                <label for="Ticket.SupportDepartment">Support Department:</label>  
                <%= this.Select("Ticket.SupportDepartment").Options(Model.SupportDepartment).FirstOption("--Select a Support Department--")
                    .Selected(Model.Ticket != null ? Model.Ticket.SupportDepartment : string.Empty) %>
                <%= Html.ValidationMessage("Ticket.SupportDepartment")%>
            </li>
            <%} %>
            <%else {%>
            <li>                
                <label for="Ticket.SupportDepartment">Support Department:</label>  
                <%= this.Select("Ticket.SupportDepartment").Options(Model.SupportDepartment)
                                                                 .Selected("Application Support")%>
                <%= Html.ValidationMessage("Ticket.SupportDepartment")%>
            </li>            
            <%} %>
            <span id="ForWebSite" style="display: none">
                <li>
                    <label for="Ticket.ForWebSite">Web Site Address:</label>
                    <%= Html.TextBox("Ticket.ForWebSite", string.Empty, new { style = "width: 500px" })%>                 
                    <%= Html.ValidationMessage("Ticket.ForWebSite")%>
                    <span class="WebSiteWarning" style="color:Orange">&nbsp</span>
                </li>
            </span>
            <span id="ForApplication" style="display: none">

                <div style="width: 400px; padding-left: 250px; margin-top: -23px;">If the Application that you need help with is not listed below, choose <strong>Computer Support</strong> instead. Otherwise a response to your ticket may be delayed until your ticket can be routed to the correct department.</div>
          
                <li>
                    <label for="Ticket.ForApplication">For Application:</label>
                    <%--<%= Html.TextBox("Ticket.ForApplication", Model.AppName, new { style = "width: 500px" })%>--%>
                    <%= this.Select("Ticket.ForApplication").Options(Model.ProgrammingSupportApps).FirstOption("--Select a Program--")
                        .Selected(Model.Ticket != null ? Model.Ticket.ForApplication : string.Empty)%>
                    <%= Html.ValidationMessage("Ticket.ForApplication")%>
                </li>
            </span>
            
            <span id="ForComputerSupport" style="display: none">
                 <li>
                    <label for="Ticket.Subject">Your Phone Number:</label>
                    <%=Html.TextBox("Ticket.YourPhoneNumber", Model != null && Model.Ticket != null ? Model.Ticket.YourPhoneNumber : string.Empty)%>
                    <%= Html.ValidationMessage("Ticket.ForComputerSupport")%>
                </li>
                 <li>
                    <label for="Ticket.Subject">Location:</label>
                    <%=Html.TextBox("Ticket.LocationOfProblem", Model != null && Model.Ticket != null ? Model.Ticket.LocationOfProblem : string.Empty, new {style = "width: 500px"})%>
                    <%= Html.ValidationMessage("Ticket.LocationOfProblem")%>
                </li>
            </span>
            
            <span id="available-dates-container">
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
            </span>

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
                <%--<%=Html.LabelFor(a => a.Ticket.Subject, DisplayOptions.HumanizeAndColon) %>--%>
                <%=Html.TextBox("Ticket.Subject", Model != null && Model.Ticket != null ? Model.Ticket.Subject : string.Empty , new {style = "width: 500px"})%>
                <%= Html.ValidationMessage("Ticket.Subject")%>
            </li>
            <li>
                <label for="Ticket.MessageBody">MessageBody:</label>
                <%= Html.TextArea("Ticket.MessageBody", new { style = "height:225px; width: 700px" })%>
                <br/>
                <%= Html.ValidationMessage("Ticket.MessageBody")%>
            </li>

            