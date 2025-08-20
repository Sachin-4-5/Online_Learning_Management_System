<%@ Page Title="Register" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="OLMS.UI.Account.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-5">
        <div class="row justify-content-center">
            <div class="col-lg-5 col-md-7">

                <!-- Modern Card with gradient header -->
                <div class="card shadow-lg border-0">
                    <div class="card-header text-center text-white" style="background: linear-gradient(135deg,#1abc9c,#16a085);">
                        <h3 class="mb-0"><i class="bi bi-person-plus-fill me-2"></i>Create Account</h3>
                        <p class="small mb-0">Fill in the details to register</p>
                    </div>
                    <div class="card-body p-4">

                        <asp:Literal ID="litMessage" runat="server"></asp:Literal>

                        <!-- Full Name -->
                        <div class="mb-3">
                            <asp:Label ID="lblFullName" runat="server" Text="Full Name" CssClass="form-label"></asp:Label>
                            <div class="input-group">
                                <span class="input-group-text bg-white"><i class="bi bi-person-fill"></i></span>
                                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Placeholder="Your full name"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Email -->
                        <div class="mb-3">
                            <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                            <div class="input-group">
                                <span class="input-group-text bg-white"><i class="bi bi-envelope-fill"></i></span>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Your email address"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Password -->
                        <div class="mb-3">
                            <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="form-label"></asp:Label>
                            <div class="input-group">
                                <span class="input-group-text bg-white"><i class="bi bi-lock-fill"></i></span>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Create password"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Role -->
                        <div class="mb-4">
                            <asp:Label ID="lblRole" runat="server" Text="Select Role" CssClass="form-label"></asp:Label>
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>

                        <!-- Register Button -->
                        <div class="d-grid mb-3">
                            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-success btn-lg" OnClick="btnRegister_Click" />
                        </div>

                        <!-- Login link -->
                        <div class="text-center">
                            <small>Already registered? <a href="Login.aspx" class="text-primary">Login here</a></small>
                        </div>

                    </div>
                </div>

                <!-- Optional footer -->
                <div class="text-center mt-3">
                    <small class="text-muted">&copy; 2025 OLMS. All rights reserved.</small>
                </div>

            </div>
        </div>
    </div>

</asp:Content>