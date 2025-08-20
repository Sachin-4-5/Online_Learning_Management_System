<%@ Page Title="Enrollment" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
    AutoEventWireup="true" CodeBehind="Enrollment.aspx.cs" Inherits="OLMS.UI.Admin.Enrollment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-4">

        <!-- Header -->
        <div class="text-center mb-4">
            <h2 class="mb-1"><i class="bi bi-person-check-fill me-2"></i>Manage Enrollments</h2>
            <p class="text-muted mb-0">Enroll a student into a course and manage existing enrollments</p>
        </div>

        <!-- Enroll Form -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-primary text-white">
                <h5 class="mb-0"><i class="bi bi-plus-square me-2"></i>Enroll Student to Course</h5>
            </div>
            <div class="card-body">
                <asp:ValidationSummary ID="vsEnroll" runat="server" CssClass="alert alert-danger" />
                <div class="row g-3 align-items-end">

                    <div class="col-md-5">
                        <label for="ddlCourses" class="form-label">Course <span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlCourses" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>

                    <div class="col-md-5">
                        <label for="ddlStudents" class="form-label">Student <span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlStudents" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>

                    <div class="col-md-2 d-grid">
                        <asp:Button ID="btnEnroll" runat="server" Text="Enroll" CssClass="btn btn-success"
                            OnClick="btnEnroll_Click" />
                    </div>

                    <div class="col-12">
                        <asp:Label ID="lblMessage" runat="server" CssClass="fw-semibold"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <!-- Existing Enrollments -->
        <div class="card shadow-sm">
            <div class="card-header bg-dark text-white d-flex justify-content-between align-items-center">
                <h5 class="mb-0"><i class="bi bi-card-list me-2"></i>Existing Enrollments</h5>
            </div>
            <div class="card-body table-responsive">
                <asp:GridView ID="gvEnrollments" runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="EnrollmentID"
                    CssClass="table table-hover table-bordered align-middle"
                    AllowPaging="true" PageSize="10"
                    OnPageIndexChanging="gvEnrollments_PageIndexChanging"
                    OnRowDeleting="gvEnrollments_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="EnrollmentID" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="CourseName" HeaderText="Course" />
                        <asp:BoundField DataField="StudentName" HeaderText="Student" />
                        <asp:BoundField DataField="EnrolledDate" HeaderText="Enrolled On" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>

</asp:Content>