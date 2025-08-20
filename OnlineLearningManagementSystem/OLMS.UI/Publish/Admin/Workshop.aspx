<%@ Page Title="Workshop Management" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Workshop.aspx.cs" Inherits="OLMS.UI.Admin.Workshop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-4">

        <!-- Header -->
        <div class="text-center mb-4">
            <h2><i class="bi bi-calendar2-event-fill me-2"></i>Workshop Management</h2>
        </div>

        <!-- Workshops Grid -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0"><i class="bi bi-card-list me-2"></i>Existing Workshops</h5>
            </div>
            <div class="card-body table-responsive">
                <asp:GridView ID="gvWorkshops" runat="server" AutoGenerateColumns="False" DataKeyNames="WorkshopID"
                    OnRowDeleting="gvWorkshops_RowDeleting"
                    CssClass="table table-striped table-hover table-bordered align-middle" AllowPaging="true" PageSize="10">
                    <Columns>
                        <asp:BoundField DataField="WorkshopID" HeaderText="ID" ReadOnly="true" />
                        <asp:BoundField DataField="Title" HeaderText="Title" />
                        <asp:BoundField DataField="TrainerID" HeaderText="Trainer ID" />
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="Location" HeaderText="Location" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:CheckBoxField DataField="IsActive" HeaderText="Active" />
                        <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Add / Update Workshop Form -->
        <div class="card shadow-sm">
            <div class="card-header bg-primary text-white text-center">
                <h5 class="mb-0"><i class="bi bi-plus-square-fill me-2"></i>Add / Update Workshop</h5>
                <small>Fill details and click Add Workshop</small>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfWorkshopID" runat="server" />

                <div class="row g-3 align-items-end">

                    <div class="col-md-4">
                        <label for="txtTitle" class="form-label">Title <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Placeholder="Workshop Title"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label for="txtTrainerID" class="form-label">Trainer ID <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtTrainerID" runat="server" CssClass="form-control" Placeholder="Trainer ID"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label for="txtLocation" class="form-label">Location</label>
                        <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" Placeholder="Location"></asp:TextBox>
                    </div>

                    <div class="col-md-12">
                        <label for="txtDescription" class="form-label">Description</label>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" Placeholder="Brief description"></asp:TextBox>
                    </div>

                    <div class="col-md-3">
                        <label for="txtStartDate" class="form-label">Start Date <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>

                    <div class="col-md-3">
                        <label for="txtEndDate" class="form-label">End Date <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>

                    <div class="col-md-2 d-flex align-items-center">
                        <div class="form-check">
                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" CssClass="form-check-input" />
                            <label class="form-check-label ms-2" for="chkIsActive">Active</label>
                        </div>
                    </div>

                    <div class="col-md-12 d-flex gap-2 mt-3">
                        <asp:Button ID="btnAdd" runat="server" Text="Add Workshop" CssClass="btn btn-success flex-fill" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-secondary flex-fill" OnClick="btnClear_Click" CausesValidation="false" />
                    </div>

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

    </div>

</asp:Content>