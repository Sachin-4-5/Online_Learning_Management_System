<%@ Page Title="Course Detail" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeBehind="CourseDetail.aspx.cs" Inherits="OLMS.UI.Courses.CourseDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Course Detail</h2>
    <asp:Panel ID="pnlCourse" runat="server" Visible="false">
        <h3><asp:Label ID="lblTitle" runat="server" /></h3>
        <p><asp:Label ID="lblDescription" runat="server" /></p>
        <p><b>Category:</b> <asp:Label ID="lblCategory" runat="server" /></p>
        <p><b>Created On:</b> <asp:Label ID="lblCreatedDate" runat="server" /></p>
    </asp:Panel>

    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger">
        <asp:Label ID="lblError" runat="server" />
    </asp:Panel>
</asp:Content>