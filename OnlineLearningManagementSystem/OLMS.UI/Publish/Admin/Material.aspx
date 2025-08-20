<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Material.aspx.cs" 
    Inherits="OLMS.UI.Admin.Material" 
    MasterPageFile="~/MasterPages/Site.Master" 
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-4">

        <!-- Material Management Form -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0"><i class="bi bi-folder-plus me-2"></i> Manage Materials</h4>
                <small>Fill in the details to add or update workshop materials</small>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfMaterialID" runat="server" />

                <!-- Validation Summary -->
                <asp:ValidationSummary ID="vsSummary" runat="server" 
                    CssClass="alert alert-danger mb-3" 
                    HeaderText="Please fix the following errors:" 
                    DisplayMode="BulletList" />

                <div class="row g-3">
                    
                    <!-- Workshop -->
                    <div class="col-md-6">
                        <label class="form-label fw-bold" for="ddlWorkshops">Workshop <span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlWorkshops" runat="server" CssClass="form-select" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlWorkshops_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <!-- Title -->
                    <div class="col-md-6">
                        <label class="form-label fw-bold" for="txtTitle">Title <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Placeholder="Material title"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                            ErrorMessage="Title is required" CssClass="text-danger small" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <!-- Upload -->
                    <div class="col-md-6">
                        <label class="form-label fw-bold" for="fuMaterial">Upload File <span class="text-danger">*</span></label>
                        <asp:FileUpload ID="fuMaterial" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvFileUpload" runat="server" ControlToValidate="fuMaterial"
                            ErrorMessage="Please select a file" CssClass="text-danger small" Enabled="false" Display="Dynamic" />
                        <small class="form-text text-muted">Required for new material, optional when updating.</small>
                    </div>

                    <!-- Description -->
                    <div class="col-md-6">
                        <label class="form-label fw-bold" for="txtDescription">Description</label>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3"
                                     CssClass="form-control" Placeholder="Brief description"></asp:TextBox>
                    </div>

                </div>

                <!-- Buttons -->
                <div class="d-flex gap-2 mt-4">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" 
                                CssClass="btn btn-success px-4" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" 
                                CssClass="btn btn-warning px-4" Visible="false" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" 
                                CssClass="btn btn-secondary px-4" CausesValidation="false" />
                </div>

            </div>
        </div>

        <!-- Materials List Table -->
        <div class="card shadow-sm">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0"><i class="bi bi-collection me-2"></i> Uploaded Materials</h5>
            </div>
            <div class="card-body">
                <asp:GridView ID="gvMaterials" runat="server" AutoGenerateColumns="False" DataKeyNames="MaterialID"
                    OnRowCommand="gvMaterials_RowCommand"
                    CssClass="table table-striped table-bordered align-middle text-center">
                    <Columns>
                        <asp:BoundField DataField="MaterialID" HeaderText="ID" ReadOnly="true" />
                        <asp:BoundField DataField="WorkshopTitle" HeaderText="Workshop" />
                        <asp:BoundField DataField="Title" HeaderText="Title" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:TemplateField HeaderText="File">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlFile" runat="server"
                                    Text='<%# string.IsNullOrEmpty(Eval("FilePath").ToString()) ? "No File" : System.IO.Path.GetFileName(Eval("FilePath").ToString()) %>'
                                    NavigateUrl='<%# string.IsNullOrEmpty(Eval("FilePath").ToString()) ? "" : Eval("FilePath") %>'
                                    Target="_blank" 
                                    CssClass="btn btn-sm btn-outline-primary" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditMaterial"
                                    CommandArgument='<%# Eval("MaterialID") %>' 
                                    CssClass="btn btn-sm btn-warning me-1" CausesValidation="false">Edit</asp:LinkButton>

                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteMaterial"
                                    CommandArgument='<%# Eval("MaterialID") %>' 
                                    CssClass="btn btn-sm btn-danger" CausesValidation="false"
                                    OnClientClick="return confirm('Are you sure you want to delete this material?');">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>

</asp:Content>