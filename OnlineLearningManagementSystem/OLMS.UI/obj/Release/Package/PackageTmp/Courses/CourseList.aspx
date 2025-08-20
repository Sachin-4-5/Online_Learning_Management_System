<%@ Page Title="Courses" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeBehind="CourseList.aspx.cs" Inherits="OLMS.UI.Courses.CourseList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Available Courses</h2>
    <asp:Repeater ID="rptCourses" runat="server">
        <ItemTemplate>
            <div class="card mb-3">
                <div class="card-body">
                    <h5 class="card-title"><%# Eval("Title") %></h5>
                    <p class="card-text"><%# Eval("Description") %></p>
                    <a href='CourseDetail.aspx?CourseID=<%# Eval("CourseID") %>' class="btn btn-primary">View Course</a>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>