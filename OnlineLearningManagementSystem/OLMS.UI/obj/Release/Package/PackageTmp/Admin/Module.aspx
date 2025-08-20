<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Module.aspx.cs" Inherits="OLMS.UI.Admin.Module" MasterPageFile="~/MasterPages/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Modules</h2>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />

    <!-- Course Selection -->
    <asp:DropDownList ID="ddlCourses" runat="server" AutoPostBack="true" 
        OnSelectedIndexChanged="ddlCourses_SelectedIndexChanged">
    </asp:DropDownList>
    <br /><br />

    <!-- Module List -->
    <asp:GridView ID="gvModules" runat="server" AutoGenerateColumns="false" DataKeyNames="ModuleID"
        OnRowEditing="gvModules_RowEditing" OnRowUpdating="gvModules_RowUpdating"
        OnRowCancelingEdit="gvModules_RowCancelingEdit" OnRowDeleting="gvModules_RowDeleting">
        <Columns>
            <asp:BoundField DataField="ModuleID" HeaderText="Module ID" ReadOnly="true" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="SortOrder" HeaderText="Sort Order" />
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>

    <h3>Add New Module</h3>
    <asp:TextBox ID="txtTitle" runat="server" Placeholder="Title"></asp:TextBox><br />
    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" Placeholder="Description"></asp:TextBox><br />
    <asp:TextBox ID="txtSortOrder" runat="server" Placeholder="Sort Order"></asp:TextBox><br />
    <asp:Button ID="btnAdd" runat="server" Text="Add Module" OnClick="btnAdd_Click" />
</asp:Content>