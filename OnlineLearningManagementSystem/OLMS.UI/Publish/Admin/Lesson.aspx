<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lesson.aspx.cs" Inherits="OLMS.UI.Admin.Lesson" MasterPageFile="~/MasterPages/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Lessons</h2>

    <div class="mb-3">
        <asp:Label ID="lblModule" runat="server" Text="Select Module:" CssClass="form-label"></asp:Label>
        <asp:DropDownList ID="ddlModules" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlModules_SelectedIndexChanged"></asp:DropDownList>
    </div>

    <asp:GridView ID="gvLessons" runat="server" AutoGenerateColumns="false" DataKeyNames="LessonID"
        CssClass="table table-striped table-bordered"
        OnRowEditing="gvLessons_RowEditing" OnRowUpdating="gvLessons_RowUpdating"
        OnRowCancelingEdit="gvLessons_RowCancelingEdit" OnRowDeleting="gvLessons_RowDeleting">
        <Columns>
            <asp:BoundField DataField="LessonID" HeaderText="ID" ReadOnly="true" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Content" HeaderText="Content" />
            <asp:BoundField DataField="VideoUrl" HeaderText="Video URL" />
            <asp:BoundField DataField="SortOrder" HeaderText="Sort Order" />
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>

    <h3 class="mt-4">Add New Lesson</h3>
    <div class="mb-2">
        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Placeholder="Title"></asp:TextBox>
    </div>
    <div class="mb-2">
        <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" Placeholder="Content"></asp:TextBox>
    </div>
    <div class="mb-2">
        <asp:TextBox ID="txtVideoUrl" runat="server" CssClass="form-control" Placeholder="Video URL"></asp:TextBox>
    </div>
    <div class="mb-2">
        <asp:TextBox ID="txtSortOrder" runat="server" CssClass="form-control" Placeholder="Sort Order"></asp:TextBox>
    </div>
    <asp:Button ID="btnAdd" runat="server" Text="Add Lesson" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
</asp:Content>