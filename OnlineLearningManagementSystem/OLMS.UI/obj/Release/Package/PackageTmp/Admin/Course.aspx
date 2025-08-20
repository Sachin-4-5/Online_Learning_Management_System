<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Course.aspx.cs" 
    Inherits="OLMS.UI.Admin.Course" 
    MasterPageFile="~/MasterPages/Site.Master" 
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-4">

        <!-- Course Management Horizontal Form -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-primary text-white text-center">
                <h4 class="mb-0"><i class="bi bi-journal-bookmark-fill me-2"></i> Manage Courses</h4>
                <small>Fill in the details to add or update a course</small>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfCourseID" runat="server" />

                <!-- Validation Summary -->
                <asp:ValidationSummary ID="vsSummary" runat="server" 
                    CssClass="alert alert-danger mb-3" 
                    HeaderText="Please fix the following errors:" 
                    DisplayMode="BulletList" />

                <div class="row g-3 align-items-end">

                    <div class="col-md-3">
                        <label class="form-label" for="txtTitle">Title <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Placeholder="Course title"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                            ErrorMessage="Title is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label" for="txtCategory">Category</label>
                        <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control" Placeholder="Category"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label" for="txtDescription">Description</label>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="1"
                                     CssClass="form-control" Placeholder="Brief course description" ValidateRequestMode="Disabled"></asp:TextBox>
                    </div>

                    <div class="col-md-2 d-flex align-items-center">
                        <div class="form-check">
                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" CssClass="form-check-input" />
                            <label class="form-check-label ms-2" for="chkIsActive">Active</label>
                        </div>
                    </div>

                    <div class="col-md-12 d-flex gap-2 mt-2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" 
                                    CssClass="btn btn-success flex-fill" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" 
                                    CssClass="btn btn-secondary flex-fill" CausesValidation="false" />
                    </div>

                </div>
            </div>
        </div>

        <!-- Courses List Table -->
        <div class="card shadow-sm">
            <div class="card-header bg-dark text-white text-center">
                <h5 class="mb-0"><i class="bi bi-card-list me-2"></i> Added Courses</h5>
            </div>
            <div class="card-body table-responsive">
                <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="False" DataKeyNames="CourseID"
                    OnRowCommand="gvCourses_RowCommand" OnPageIndexChanging="gvCourses_PageIndexChanging"
                    CssClass="table table-hover table-bordered align-middle" AllowPaging="true" PageSize="10">
                    <Columns>
                        <asp:BoundField DataField="CourseID" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="Title" HeaderText="Title" />
                        <asp:BoundField DataField="Category" HeaderText="Category" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:CheckBoxField DataField="IsActive" HeaderText="Active" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditCourse"
                                    CommandArgument='<%# Eval("CourseID") %>' CssClass="btn btn-sm btn-warning me-1"
                                    CausesValidation="false">Edit</asp:LinkButton>

                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteCourse"
                                    CommandArgument='<%# Eval("CourseID") %>' CssClass="btn btn-sm btn-danger"
                                    CausesValidation="false"
                                    OnClientClick="return confirm('Are you sure you want to delete this course?');">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>

    <!-- Checkbox styling -->
    <style>
        .form-check-input {
            width: 1.2rem;
            height: 1.2rem;
            box-shadow: none !important;
            border: 1px solid #ced4da;
            margin-top: 0.25rem;
        }
        .form-check-label {
            margin-left: 0.5rem;
        }
    </style>

</asp:Content>