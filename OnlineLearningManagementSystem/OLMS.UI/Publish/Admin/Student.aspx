<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="OLMS.UI.Admin.Student" MasterPageFile="~/MasterPages/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-group {
            margin-bottom: 10px;
        }
        .gridview {
            margin-top: 20px;
        }
    </style>

    <h2>Manage Students</h2>

    <div class="form-group">
        <asp:Label ID="lblWorkshop" runat="server" Text="Workshop:"></asp:Label>
        <asp:DropDownList ID="ddlWorkshops" runat="server"></asp:DropDownList>
    </div>

    <div class="form-group">
        <asp:TextBox ID="txtFirstName" runat="server" Placeholder="First Name"></asp:TextBox>
        <asp:TextBox ID="txtLastName" runat="server" Placeholder="Last Name"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email"></asp:TextBox>
        <asp:TextBox ID="txtPhone" runat="server" Placeholder="Phone"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:Button ID="btnAdd" runat="server" Text="Add Student" OnClick="btnAdd_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update Student" OnClick="btnUpdate_Click" CssClass="btn btn-success" Visible="false" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-secondary" Visible="false" />
    </div>

    <asp:Literal ID="litMessage" runat="server"></asp:Literal>

    <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="false" CssClass="gridview" DataKeyNames="StudentID"
        OnRowEditing="gvStudents_RowEditing" OnRowDeleting="gvStudents_RowDeleting" OnRowCancelingEdit="gvStudents_RowCancelingEdit">
        <Columns>
            <asp:BoundField DataField="StudentID" HeaderText="ID" ReadOnly="true" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" />
            <asp:BoundField DataField="WorkshopName" HeaderText="Workshop" />
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>