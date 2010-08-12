<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HelpRequest.Controllers.ViewModels.HelpTopicViewModel>" %>
<%@ Import Namespace="HelpRequest.Controllers.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	WatchVideo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>WatchVideo
    <%if (!string.IsNullOrEmpty(Model.HelpTopic.AppFilter)){%>
    For Application: <%=Html.Encode(Model.HelpTopic.AppFilter) %> 
    <%}%>
    </h2>
    <%=Html.HtmlEncode(Model.HelpTopic.Answer) %>
    
    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" id="single1" name="HelpRequest how to" width="720" height="576">
    <param name="movie" value="http://v.caes.ucdavis.edu/JWPlayer/player.swf">
    <param name="allowfullscreen" value="true">
    <param name="allowscriptaccess" value="always">
    <param name="wmode" value="transparent">
    <param name="flashvars" value="author=HelpRequest&amp;file=http://v.caes.ucdavis.edu/HR/Video/<%=Html.Encode(Model.HelpTopic.VideoName)%>.flv&amp;
    image=http://v.caes.ucdavis.edu/HR/Video<%=Html.Encode(Model.HelpTopic.VideoName)%>.jpg&amp;
    plugins=captions-1,audiodescription-1">
    <embed type="application/x-shockwave-flash" id="single2" name="single2" src="http://v.caes.ucdavis.edu/JWPlayer/player.swf" bgcolor="undefined" allowscriptaccess="always" allowfullscreen="true" wmode="transparent" flashvars="author=ASI&amp;file=http://v.caes.ucdavis.edu/HR/Video/<%=Html.Encode(Model.HelpTopic.VideoName)%>.flv&amp;
    image=http://v.caes.ucdavis.edu/HR/Video/<%=Html.Encode(Model.HelpTopic.VideoName)%>.jpg&amp;
    plugins=captions-1,audiodescription-1" width="720" height="576">
    </object>

    <p>
    <a href="javascript:history.go(-1)" onMouseOver="self.status=document.referrer;return true">BACK</a> 
    </p>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>