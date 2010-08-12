<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HelpRequest.Controllers.ViewModels.HelpTopicViewModel>" %>
<%@ Import Namespace="HelpRequest.Controllers" %>
<%--<%@ Import Namespace="CRP.Core.Resources" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Help Topics</h2>
    <% if (Model.IsUserAdmin){%>
    <p>
        <%=Html.ActionLink<HelpController>(a => a.Create(Model.AppName), "Create New")%>
    </p>
    <%}%>
    <% using (Html.BeginForm()) { %>
                <% Html.Grid(Model.HelpTopics)
                   .Transactional()
                   .Name("Help Topics")
                   .PrefixUrlParameters(false) 
                   .CellAction(cell =>
                    {
                        switch (cell.Column.Member)
                        {
                            case "AvailableToPublic":
                                cell.Text = cell.DataItem.AvailableToPublic ? "x" : string.Empty;
                                break;
                            case "IsActive":
                                cell.Text = cell.DataItem.IsActive ? "x" : string.Empty;
                                break;
                        }
                    })                                   
                   .Columns(col =>
                                {
                                    col.Template(x =>
                                                { %>
                                                    <% if (x.IsVideo){%>
                                                        <%= Html.ActionLink<HelpController>(a => a.WatchVideo(x.Id, Model.AppName), "Watch")%>
                                                    <%}else{%>
                                                        <%= Html.ActionLink<HelpController>(a => a.Details(x.Id, Model.AppName), "View")%>
                                                    <%}%>                                    
                                                    <% if (Model.IsUserAdmin){%>|
                                                    <%=Html.ActionLink<HelpController>(a => a.Edit(x.Id, Model.AppName), "Edit")%> 
                                                    <%}%>
                                                <% });                                    
                                    col.Bound(x => x.IsActive).Visible(Model.IsUserAdmin).Title("Active");
                                    col.Bound(x => x.AvailableToPublic).Visible(Model.IsUserAdmin).Title("Public");
                                    col.Bound(x => x.NumberOfReads).Visible(Model.IsUserAdmin).Title("Reads");
                                    col.Bound(x => x.AppFilter).Visible(Model.IsUserAdmin).Title("For Application");
                                    col.Bound(x => x.Question).Title("Frequently Asked Question");
                                })                  
                    .Pageable(x=>x.PageSize(20))
                    .Sortable()
                    .Render(); %>
    <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>