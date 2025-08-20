<%@ Page Title="Home" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OLMS.UI.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center my-5">
        <h1>Welcome to OLMS</h1>
        <asp:Literal ID="litGreeting" runat="server"></asp:Literal>
        <p class="lead">Learn anytime, anywhere with interactive courses.</p>
        <a class="btn btn-primary btn-lg" href="Courses/CourseList.aspx">Browse Courses</a>
    </div>

    <!-- Features Section -->
    <div class="row my-5">
        <div class="col-md-4">
            <div class="card shadow-sm">
                <div class="card-body">
                    <h5 class="card-title">Interactive Lessons</h5>
                    <p class="card-text">Engaging multimedia lessons including videos and PDFs.</p>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="card shadow-sm">
                <div class="card-body">
                    <h5 class="card-title">Quizzes & Tests</h5>
                    <p class="card-text">Evaluate your learning with quizzes after every module.</p>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="card shadow-sm">
                <div class="card-body">
                    <h5 class="card-title">Certificates</h5>
                    <p class="card-text">Earn certificates for completed courses to showcase your skills.</p>
                </div>
            </div>
        </div>
    </div>

    <!-- Role-specific Quick Actions -->
    <asp:Panel ID="pnlQuickLinks" runat="server" CssClass="my-5" Visible="false">
        <h3>Quick Links</h3>
        <div class="row">
            <!-- Student -->
            <asp:Panel ID="pnlStudent" runat="server" CssClass="col-md-4" Visible="false">
                <div class="card border-success">
                    <div class="card-body">
                        <h5 class="card-title">Student Dashboard</h5>
                        <p class="card-text">Continue learning and track your progress.</p>
                        <a href="Student/TakeQuiz.aspx" class="btn btn-success">Take Quiz</a>
                    </div>
                </div>
            </asp:Panel>

            <!-- Trainer -->
            <asp:Panel ID="pnlTrainer" runat="server" CssClass="col-md-4" Visible="false">
                <div class="card border-info">
                    <div class="card-body">
                        <h5 class="card-title">Trainer Panel</h5>
                        <p class="card-text">Create lessons and manage course content.</p>
                        <a href="Trainer/AddQuestions.aspx" class="btn btn-info">Add Questions</a>
                    </div>
                </div>
            </asp:Panel>

            <!-- Admin -->
            <asp:Panel ID="pnlAdmin" runat="server" CssClass="col-md-4" Visible="false">
                <div class="card border-danger">
                    <div class="card-body">
                        <h5 class="card-title">Admin Dashboard</h5>
                        <p class="card-text">Manage users, approvals, and reports.</p>
                        <a href="Admin/User.aspx" class="btn btn-danger">Manage Users</a>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:Content>