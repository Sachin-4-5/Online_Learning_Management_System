<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuiz.aspx.cs" Inherits="OLMS.UI.Admin.AddQuiz"
    MasterPageFile="~/MasterPages/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Add New Quiz</h2>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />

    <table class="table table-bordered">
        <tr>
            <td>Course:</td>
            <td>
                <asp:DropDownList ID="ddlCourses" runat="server" CssClass="form-select"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Quiz Title:</td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                    ErrorMessage="Title is required" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td>Total Marks:</td>
            <td>
                <asp:TextBox ID="txtTotalMarks" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvMarks" runat="server" ControlToValidate="txtTotalMarks"
                    ErrorMessage="Total Marks required" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text="Add Quiz" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>

    <hr />

    <asp:GridView ID="gvQuizzes" runat="server" AutoGenerateColumns="False" DataKeyNames="QuizID" OnRowCommand="gvQuizzes_RowCommand" CssClass="table table-striped table-hover">
        <Columns>
            <asp:BoundField DataField="QuizID" HeaderText="ID" ReadOnly="true" />
            <asp:BoundField DataField="CourseTitle" HeaderText="Course" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="TotalMarks" HeaderText="Total Marks" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditQuiz" CommandArgument='<%# Eval("QuizID") %>' CssClass="text-primary">Edit</asp:LinkButton>
                    |
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteQuiz" CommandArgument='<%# Eval("QuizID") %>' OnClientClick="return confirm('Are you sure?');" CssClass="text-danger">Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>