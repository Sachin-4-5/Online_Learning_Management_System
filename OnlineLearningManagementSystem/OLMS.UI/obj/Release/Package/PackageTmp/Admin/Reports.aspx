<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" 
    Inherits="OLMS.UI.Admin.Reports"
    MasterPageFile="~/MasterPages/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Reports</h2>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="upReports" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
                <tr>
                    <td>Course:</td>
                    <td>
                        <asp:DropDownList ID="ddlCourses" runat="server" 
                            AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlCourses_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Workshop:</td>
                    <td>
                        <asp:DropDownList ID="ddlWorkshops" runat="server" 
                            AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlWorkshops_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
            </table>

            <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="true"></asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>