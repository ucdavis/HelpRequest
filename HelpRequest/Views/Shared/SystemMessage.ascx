﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%
    var messages = ViewData["ServiceMessages"] as ServiceMessage[];
    var showIt = messages.Count() > 0 ? true : false;
 %>       
        <% if (showIt){%> 
        <div id="system_wide_status">
            <div id="system_wide_status_text">
                <% foreach(var message in messages.OrderByDescending(a => a.Critical).ThenBy(a => a.Global)){%>
                    <% var isCritical = message.Critical ? "critical" : "notCritical";%>
                    <div class="<%=isCritical %>" ><%= message.Message%></div>                    
                <%}%>  
            </div>
        </div>
        <% }%>

