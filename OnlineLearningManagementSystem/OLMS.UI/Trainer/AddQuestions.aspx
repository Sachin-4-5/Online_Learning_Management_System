<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuestions.aspx.cs" Inherits="OLMS.UI.Trainer.AddQuestions"
    MasterPageFile="~/MasterPages/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-4">

        <!-- Header -->
        <div class="text-center mb-4">
            <h2><i class="bi bi-question-square-fill me-2"></i>Add Questions to Quiz</h2>
        </div>

        <!-- Validation Summary -->
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />

        <!-- Question Form Card -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-primary text-white">
                <h5 class="mb-0">Question Details</h5>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfQuestionID" runat="server" />

                <div class="row g-3">

                    <!-- Quiz Selection -->
                    <div class="col-md-4">
                        <label for="ddlQuizzes" class="form-label">Select Quiz <span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlQuizzes" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlQuizzes_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <!-- Marks -->
                    <div class="col-md-2">
                        <label for="txtMarks" class="form-label">Marks <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtMarks" runat="server" CssClass="form-control" Placeholder="e.g. 5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMarks" runat="server" ControlToValidate="txtMarks"
                            ErrorMessage="Marks required" CssClass="text-danger" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revMarks" runat="server" ControlToValidate="txtMarks"
                            ValidationExpression="^\d+$" ErrorMessage="Enter valid number" CssClass="text-danger" Display="Dynamic" />
                    </div>

                    <!-- Question Text -->
                    <div class="col-md-6">
                        <label for="txtQuestionText" class="form-label">Question <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtQuestionText" runat="server" CssClass="form-control"
                            TextMode="MultiLine" Rows="3" Placeholder="Enter question text"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvQuestionText" runat="server" ControlToValidate="txtQuestionText"
                            ErrorMessage="Question is required" CssClass="text-danger" Display="Dynamic" />
                    </div>

                </div>

                <!-- Buttons -->
                <div class="mt-3 d-flex gap-2">
                    <asp:Button ID="btnAdd" runat="server" Text="Add Question" CssClass="btn btn-success flex-fill" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update Question" CssClass="btn btn-warning flex-fill" OnClick="btnUpdate_Click" Visible="false" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary flex-fill" OnClick="btnCancel_Click" CausesValidation="false" />
                </div>
            </div>
        </div>

        <!-- Questions List -->
        <div class="card shadow-sm">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0"><i class="bi bi-list-check me-2"></i>Added Questions</h5>
            </div>
            <div class="card-body table-responsive">
                <asp:GridView ID="gvQuestions" runat="server" AutoGenerateColumns="False" DataKeyNames="QuestionID"
                    OnRowCommand="gvQuestions_RowCommand" CssClass="table table-striped table-hover table-bordered align-middle"
                    AllowPaging="true" PageSize="10">
                    <Columns>
                        <asp:BoundField DataField="QuestionID" HeaderText="ID" ReadOnly="true" />
                        <asp:BoundField DataField="QuestionText" HeaderText="Question" />
                        <asp:BoundField DataField="Marks" HeaderText="Marks" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditQuestion"
                                    CommandArgument='<%# Eval("QuestionID") %>' CssClass="btn btn-sm btn-warning me-1">Edit</asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteQuestion"
                                    CommandArgument='<%# Eval("QuestionID") %>' CssClass="btn btn-sm btn-danger"
                                    OnClientClick="return confirm('Are you sure?');">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>

</asp:Content>