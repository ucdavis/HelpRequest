<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<HelpRequest.Controllers.ViewModels.HelpTopicViewModel>" %>
<%@ Import Namespace="HelpRequest.Core.Domain" %>
<%@ Import Namespace="xVal.Html" %>
<%@ Import Namespace="HelpRequest.Controllers" %>

    <script src='<%= Url.Content("~/Scripts/tiny_mce/jquery.tinymce.js") %>' type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jquery.enableTinyMce.js") %>" type="text/javascript"></script>
	<script type="text/javascript">
	    $(document).ready(function() {
	        var scriptUrl = '<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>';
	        $("textarea#HelpTopic_Answer").enableTinyMce({ script_location: scriptUrl, overrideHeight: '255', overrideWidth: '700' });
	        $("input[name*=VideoName]").bt('Only name, no extension or path. <br>Video must already exist on hosted server.');
	    });
	</script>

    <%= Html.ValidationSummary("Save was unsuccessful. Please correct the errors and try again.") %>

    
        <%= Html.AntiForgeryToken() %>
        <%= Html.ClientSideValidation<HelpTopic>("HelpTopic") %>
        
        <fieldset>            
            <legend>Help Topic Details</legend>
            <div id="nobullets">
                <ul>
                <li>
                    <label for="Model.HelpTopic.Question">Frequently Asked Question:</label>
                    <%= Html.TextBox("HelpTopic.Question", Model.HelpTopic != null ? Model.HelpTopic.Question : string.Empty, new { style = "width: 500px" })%>
                    <%= Html.ValidationMessage("Question")%>
                </li>
                <li>
                    <label for="Model.HelpTopic.AppFilter">Filter to specific application:</label>
                    <%= Html.TextBox("HelpTopic.AppFilter")%>
                    <%= Html.ValidationMessage("AppFilter")%>
                </li>
                <li>
                    <label for="Model.HelpTopic.IsActive">Is Active: </label>
                    <%= Html.CheckBox("HelpTopic.IsActive", Model.HelpTopic != null ? Model.HelpTopic.IsActive : false)%>
                    <%= Html.ValidationMessage("IsActive")%>
                </li>
                <li>
                    <label for="Model.HelpTopic.AvailableToPublic">Available To Public:</label>
                    <%= Html.CheckBox("HelpTopic.AvailableToPublic", Model.HelpTopic != null ? Model.HelpTopic.AvailableToPublic : false)%>
                    <%= Html.ValidationMessage("AvailableToPublic") %>
                </li>
                <li>
                    <label for="Model.HelpTopic.IsVideo">Video:</label>
                    <%= Html.CheckBox("HelpTopic.IsVideo", Model.HelpTopic != null ? Model.HelpTopic.IsVideo : false)%>
                    <%= Html.ValidationMessage("IsVideo")%>
                </li>
                <li>
                    <label for="Model.HelpTopic.VideoName">Name of video:</label>
                    <%= Html.TextBox("HelpTopic.VideoName")%>
                    <%= Html.ValidationMessage("VideoName")%>
                </li>
                <li>
                    <label for="Model.HelpTopic.NumberOfReads">Number Of Reads:</label>
                    <%= Html.TextBox("HelpTopic.NumberOfReads")%>
                    <%= Html.ValidationMessage("NumberOfReads")%>
                </li>
                <li>
                    <label for="Model.HelpTopic.Answer">Answer:</label>
                    <%= Html.TextArea("HelpTopic.Answer")%>
                    <%= Html.ValidationMessage("Answer")%>
                </li>
                </ul>
            </div>
            <p>
                <input type="submit" value="Save" />
            </p>           
        </fieldset>
        


    <div>
        <%=Html.ActionLink<HelpController>(a => a.Index(Model.AppName), "Back to list") %>
    </div>