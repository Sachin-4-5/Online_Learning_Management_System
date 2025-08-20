<%@ Page Title="User Management" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="OLMS.UI.Admin.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-4">

        <!-- User Management Card -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-primary text-white text-center">
                <h4 class="mb-1"><i class="bi bi-people-fill me-2"></i> Manage Users</h4>
                <small class="text-light">Add new users or update existing ones</small>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfUserID" runat="server" />

                <div class="row g-3 align-items-end">

                    <div class="col-md-3">
                        <label class="form-label" for="txtFullName">Full Name <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Placeholder="Full Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName"
                            ErrorMessage="Full Name required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label" for="txtEmail">Email <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Email required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Invalid email format" CssClass="text-danger"
                            ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label" for="txtPassword">Password <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                            ErrorMessage="Password required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label" for="ddlRole">Role <span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-select">
                            <asp:ListItem Value="">--Select Role--</asp:ListItem>
                            <asp:ListItem Value="1">Admin</asp:ListItem>
                            <asp:ListItem Value="2">Trainer</asp:ListItem>
                            <asp:ListItem Value="3">Student</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="ddlRole"
                            InitialValue="" ErrorMessage="Role required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-2 d-flex align-items-center mt-3">
                        <div class="form-check">
                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" CssClass="form-check-input" />
                            <label class="form-check-label ms-2" for="chkIsActive">Active</label>
                        </div>
                    </div>

                    <div class="col-md-3 mt-3 d-flex gap-2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
                            CssClass="btn btn-success btn-sm flex-fill" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"
                            CssClass="btn btn-secondary btn-sm flex-fill" CausesValidation="false" />
                    </div>

                </div>
            </div>
        </div>

        <!-- Added Users Table -->
        <div class="card shadow-sm">
            <div class="card-header bg-dark text-white text-center">
                <h5 class="mb-0"><i class="bi bi-list-check me-2"></i> Added Users</h5>
            </div>
            <div class="card-body table-responsive">
                <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID"
                    OnRowCommand="gvUsers_RowCommand" CssClass="table table-hover table-bordered align-middle"
                    AllowPaging="true" PageSize="10">
                    <Columns>
                        <asp:BoundField DataField="UserID" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="RoleID" HeaderText="Role" />
                        <asp:CheckBoxField DataField="IsActive" HeaderText="Active" />
                        <asp:BoundField DataField="DateCreated" HeaderText="Created On" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditUser"
                                    CommandArgument='<%# Eval("UserID") %>' CssClass="btn btn-sm btn-warning me-2" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteUser"
                                    CommandArgument='<%# Eval("UserID") %>' CssClass="btn btn-sm btn-danger"
                                    OnClientClick="return confirm('Are you sure you want to delete this user?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>

</asp:Content>