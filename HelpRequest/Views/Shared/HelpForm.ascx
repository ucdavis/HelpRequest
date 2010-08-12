<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<HelpRequest.Controllers.ViewModels.HelpTopicViewModel>" %>
<%@ Import Namespace="HelpRequest.Core.Domain" %>
<%@ Import Namespace="xVal.Html" %>
<%@ Import Namespace="HelpRequest.Controllers" %>

    <script src='<%= Url.Content("~/Scripts/tiny_mce/jquery.tinymce.js") %>' type="text/javascript"></script>
	<script type="text/javascript">
	    $(document).ready(function() {
	        var scriptUrl = '<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>';
	        $("textarea#HelpTopic_Answer").tinymce({
	            script_url: scriptUrl,
	            // General options
	            theme: "advanced",
	            plugins: "safari,style,save,searchreplace,print,contextmenu,paste",

	            // Theme options
	            theme_advanced_buttons1: "print,|,bold,italic,underline,|,styleselect,formatselect,fontselect,fontsizeselect",
	            theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,undo,redo",
	            theme_advanced_buttons3: "",
	            theme_advanced_toolbar_location: "top",
	            theme_advanced_toolbar_align: "left",
	            theme_advanced_statusbar_location: "bottom",
	            theme_advanced_resizing: true,

	            // dimensions stuff
	            height: "225",
	            width: "800",

	            // Example content CSS (should be your site CSS)
	            //content_css: "css/Main.css",

	            // Drop lists for link/image/media/template dialogs
	            template_external_list_url: "js/template_list.js",
	            external_link_list_url: "js/link_list.js",
	            external_image_list_url: "js/image_list.js",
	            media_external_list_url: "js/media_list.js"
	        });
	        $("input[name*=VideoName]").bt('Only name, no extension or path. <br>Video must already exist on hosted server.');
	    });
	</script>

    <%= Html.ValidationSummary("Save was unsuccessful. Please correct the errors and try again.") %>

    
        <%= Html.AntiForgeryToken() %>
        <%= Html.ClientSideValidation<HelpTopic>("*") %>
        
        <fieldset>
            <legend>Fields</legend>
            <ul>
              <li>
                <label for="Model.HelpTopic.AppFilter">Filter to specific application:</label>
                <%= Html.TextBox("HelpTopic.AppFilter")%>
                <%= Html.ValidationMessage("AppFilter")%>
            </li>
            <li>
                <label for="Model.HelpTopic.Question">Frequently Asked Question:</label>
                <%= Html.TextBox("HelpTopic.Question")%>
                <%= Html.ValidationMessage("Question", "*")%>
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
                <%= Html.ValidationMessage("Answer", "*")%>
            </li>
            </ul>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>



    <div>
        <%=Html.ActionLink<HelpController>(a => a.Index(Model.AppName), "Back to list") %>
    </div>