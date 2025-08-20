<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="OLMS.UI.Admin.Approval"
    MasterPageFile="~/MasterPages/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Approvals</h2>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" CssClass="mb-3" />

    <table class="table table-bordered">
        <tr>
            <td>Student:</td>
            <td>
                <asp:DropDownList ID="ddlStudents" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStudents_SelectedIndexChanged" CssClass="form-select"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Workshop:</td>
            <td>
                <asp:DropDownList ID="ddlWorkshops" runat="server" CssClass="form-select"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Material:</td>
            <td>
                <asp:DropDownList ID="ddlMaterials" runat="server" CssClass="form-select"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Status:</td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                    <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                    <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Comments:</td>
            <td>
                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text="Add Approval" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update Approval" CssClass="btn btn-success" OnClick="btnUpdate_Click" Visible="false" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>

    <hr />

    <asp:GridView ID="gvApprovals" runat="server" AutoGenerateColumns="False" DataKeyNames="ApprovalID" OnRowCommand="gvApprovals_RowCommand" CssClass="table table-striped table-hover">
        <Columns>
            <asp:BoundField DataField="ApprovalID" HeaderText="ID" ReadOnly="true" />
            <asp:BoundField DataField="StudentName" HeaderText="Student" />
            <asp:BoundField DataField="WorkshopTitle" HeaderText="Workshop" />
            <asp:BoundField DataField="MaterialTitle" HeaderText="Material" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="Comments" HeaderText="Comments" />
            <asp:BoundField DataField="ApprovalDate" HeaderText="Approval Date" DataFormatString="{0:dd-MMM-yyyy}" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditApproval" CommandArgument='<%# Eval("ApprovalID") %>' CssClass="text-primary">Edit</asp:LinkButton>
                    |
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteApproval" CommandArgument='<%# Eval("ApprovalID") %>' OnClientClick="return confirm('Are you sure?');" CssClass="text-danger">Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>