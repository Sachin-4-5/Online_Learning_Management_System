using OLMS.BLL.Services;
using OLMS.Models.Entities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OLMS.UI.Admin
{
    public partial class Approval : Page
    {
        private readonly ApprovalService _approvalService;
        private readonly StudentService _studentService;
        private readonly WorkshopService _workshopService;
        private readonly MaterialService _materialService;

        public Approval()
        {
            _approvalService = new ApprovalService();
            _studentService = new StudentService();
            _workshopService = new WorkshopService();
            _materialService = new MaterialService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Admin-only access
                if (Session["UserID"] == null || Convert.ToInt32(Session["RoleID"]) != 1)
                {
                    Response.Redirect("~/Account/Login.aspx");
                    return;
                }

                BindStudents();
                BindWorkshops();
                BindMaterials();
                BindGrid();
            }
        }

        private void BindStudents()
        {
            var students = _studentService.GetAll();
            ddlStudents.DataSource = students;
            ddlStudents.DataTextField = "FullName";
            ddlStudents.DataValueField = "StudentID";
            ddlStudents.DataBind();
            ddlStudents.Items.Insert(0, new ListItem("--Select Student--", "0"));
        }

        private void BindWorkshops()
        {
            var workshops = _workshopService.GetAll();
            ddlWorkshops.DataSource = workshops;
            ddlWorkshops.DataTextField = "Title";
            ddlWorkshops.DataValueField = "WorkshopID";
            ddlWorkshops.DataBind();
            ddlWorkshops.Items.Insert(0, new ListItem("--Select Workshop--", "0"));
        }

        private void BindMaterials()
        {
            var materials = _materialService.GetAll();
            ddlMaterials.DataSource = materials;
            ddlMaterials.DataTextField = "Title";
            ddlMaterials.DataValueField = "MaterialID";
            ddlMaterials.DataBind();
            ddlMaterials.Items.Insert(0, new ListItem("--Select Material--", "0"));
        }

        private void BindGrid()
        {
            gvApprovals.DataSource = _approvalService.GetAll();
            gvApprovals.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var approval = new OLMS.Models.Entities.Approval
            {
                StudentID = Convert.ToInt32(ddlStudents.SelectedValue),
                WorkshopID = Convert.ToInt32(ddlWorkshops.SelectedValue),
                MaterialID = ddlMaterials.SelectedValue != "0" ? (int?)Convert.ToInt32(ddlMaterials.SelectedValue) : null,
                Status = ddlStatus.SelectedValue,
                Comments = txtComments.Text.Trim(),
                CreatedOn = DateTime.Now
            };

            _approvalService.Add(approval);
            ClearForm();
            BindGrid();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int approvalId = Convert.ToInt32(ViewState["ApprovalID"]);
            var approval = _approvalService.GetById(approvalId);

            approval.StudentID = Convert.ToInt32(ddlStudents.SelectedValue);
            approval.WorkshopID = Convert.ToInt32(ddlWorkshops.SelectedValue);
            approval.MaterialID = ddlMaterials.SelectedValue != "0" ? (int?)Convert.ToInt32(ddlMaterials.SelectedValue) : null;
            approval.Status = ddlStatus.SelectedValue;
            approval.Comments = txtComments.Text.Trim();

            _approvalService.Update(approval);
            ClearForm();
            BindGrid();
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            ddlStudents.SelectedIndex = 0;
            ddlWorkshops.SelectedIndex = 0;
            ddlMaterials.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            txtComments.Text = string.Empty;
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void gvApprovals_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int approvalId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "EditApproval")
            {
                var approval = _approvalService.GetById(approvalId);
                ddlStudents.SelectedValue = approval.StudentID.ToString();
                ddlWorkshops.SelectedValue = approval.WorkshopID.ToString();
                ddlMaterials.SelectedValue = approval.MaterialID.HasValue ? approval.MaterialID.Value.ToString() : "0";
                ddlStatus.SelectedValue = approval.Status;
                txtComments.Text = approval.Comments;

                ViewState["ApprovalID"] = approvalId;
                btnAdd.Visible = false;
                btnUpdate.Visible = true;
            }
            else if (e.CommandName == "DeleteApproval")
            {
                _approvalService.Delete(approvalId);
                BindGrid();
            }
        }

        protected void ddlStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional: filter workshops/materials based on student selection if needed
        }
    }
}