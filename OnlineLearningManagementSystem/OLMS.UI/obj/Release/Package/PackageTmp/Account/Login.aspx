<%@ Page Title="Login" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OLMS.UI.Account.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-5">
        <div class="row justify-content-center">
            <div class="col-lg-5 col-md-7">

                <!-- Modern Card with gradient header -->
                <div class="card shadow-lg border-0">
                    <div class="card-header text-center text-white" style="background: linear-gradient(135deg,#3498db,#2980b9);">
                        <h3 class="mb-0"><i class="bi bi-box-arrow-in-right me-2"></i>Login</h3>
                        <p class="small mb-0">Enter your credentials to access your account</p>
                    </div>
                    <div class="card-body p-4">

                        <asp:Literal ID="litMessage" runat="server"></asp:Literal>

                        <!-- Email -->
                        <div class="mb-3">
                            <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                            <div class="input-group">
                                <span class="input-group-text bg-white"><i class="bi bi-envelope-fill"></i></span>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Enter your email"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Password -->
                        <div class="mb-4">
                            <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="form-label"></asp:Label>
                            <div class="input-group">
                                <span class="input-group-text bg-white"><i class="bi bi-lock-fill"></i></span>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Enter your password"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Login Button -->
                        <div class="d-grid mb-3">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-lg" OnClick="btnLogin_Click" />
                        </div>

                        <!-- Optional link to register -->
                        <div class="text-center">
                            <small>Don't have an account? <a href="Register.aspx" class="text-primary">Register here</a></small>
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