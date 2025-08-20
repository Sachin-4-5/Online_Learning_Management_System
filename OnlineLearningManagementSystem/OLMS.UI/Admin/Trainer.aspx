<%@ Page Title="Trainer Management" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Trainer.aspx.cs" Inherits="OLMS.UI.Admin.Trainer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-4">

        <!-- Trainer Management Form -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-primary text-white text-center">
                <h4 class="mb-0"><i class="bi bi-person-badge-fill me-2"></i> Trainer Management</h4>
                <small>Fill in the details to add or update a trainer</small>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfTrainerId" runat="server" />

                <div class="row g-3 align-items-end">

                    <div class="col-md-3">
                        <label class="form-label" for="txtName">Name <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="Trainer name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                            ErrorMessage="Name is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label" for="txtEmail">Email <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Trainer email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Email is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Invalid email format" CssClass="text-danger"
                            ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label" for="txtExpertise">Expertise</label>
                        <asp:TextBox ID="txtExpertise" runat="server" CssClass="form-control" Placeholder="Trainer expertise"></asp:TextBox>
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

        <!-- Added Trainers Table -->
        <div class="card shadow-sm">
            <div class="card-header bg-dark text-white text-center">
                <h5 class="mb-0"><i class="bi bi-card-list me-2"></i> Added Trainers</h5>
            </div>
            <div class="card-body table-responsive">
                <asp:GridView ID="gvTrainers" runat="server" AutoGenerateColumns="False" DataKeyNames="TrainerID"
                    OnRowCommand="gvTrainers_RowCommand"
                    CssClass="table table-hover table-bordered align-middle" AllowPaging="true" PageSize="10">
                    <Columns>
                        <asp:BoundField DataField="TrainerID" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Expertise" HeaderText="Expertise" />
                        <asp:CheckBoxField DataField="IsActive" HeaderText="Active" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditTrainer"
                                    CommandArgument='<%# Eval("TrainerID") %>' CssClass="btn btn-sm btn-warning me-1" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteTrainer"
                                    CommandArgument='<%# Eval("TrainerID") %>' CssClass="btn btn-sm btn-danger"
                                    OnClientClick="return confirm('Are you sure you want to delete this trainer?');" />
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