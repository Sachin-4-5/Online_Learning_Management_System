using OLMS.BLL.Services;
using OLMS.Models.Entities;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OLMS.UI.Admin
{
    public partial class Material : System.Web.UI.Page
    {
        private MaterialService _materialService;
        private WorkshopService _workshopService;
        private EmailService _emailService;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _materialService = new MaterialService();
            _workshopService = new WorkshopService();
            _emailService = new EmailService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindWorkshops();
                if (ddlWorkshops.Items.Count > 0)
                    BindGrid();
            }
        }

        private void BindWorkshops()
        {
            ddlWorkshops.DataSource = _workshopService.GetAll();
            ddlWorkshops.DataTextField = "Title";
            ddlWorkshops.DataValueField = "WorkshopID";
            ddlWorkshops.DataBind();
        }

        private void BindGrid()
        {
            if (ddlWorkshops.SelectedIndex > -1)
            {
                int selectedWorkshopId = Convert.ToInt32(ddlWorkshops.SelectedValue);
                gvMaterials.DataSource = _materialService.GetByWorkshopId(selectedWorkshopId);
                gvMaterials.DataBind();
            }
        }

        protected void ddlWorkshops_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            rfvFileUpload.Enabled = true; // require file on Add

            if (!Page.IsValid) return;

            string filePath = SaveUploadedFile();

            var material = new OLMS.Models.Entities.Material
            {
                WorkshopID = Convert.ToInt32(ddlWorkshops.SelectedValue),
                Title = txtTitle.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                FilePath = filePath,
                UploadDate = DateTime.Now // ✅ always set
            };

            _materialService.Add(material);
            ClearForm();
            BindGrid();

            SendEmailNotification(material.Title, ddlWorkshops.SelectedItem.Text, "added");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            rfvFileUpload.Enabled = false; // file not mandatory on Update

            int materialId = Convert.ToInt32(ViewState["MaterialID"]);
            var existingMaterial = _materialService.GetById(materialId);
            string filePath = existingMaterial.FilePath;

            if (fuMaterial.HasFile)
                filePath = SaveUploadedFile();

            var material = new OLMS.Models.Entities.Material
            {
                MaterialID = materialId,
                WorkshopID = Convert.ToInt32(ddlWorkshops.SelectedValue),
                Title = txtTitle.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                FilePath = filePath,
                UploadDate = DateTime.Now // ✅ update timestamp
            };

            _materialService.Update(material);
            ClearForm();
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
            BindGrid();

            SendEmailNotification(material.Title, ddlWorkshops.SelectedItem.Text, "updated");
        }


        private string SaveUploadedFile()
        {
            if (!fuMaterial.HasFile) return "";

            string folderPath = Server.MapPath("~/Uploads/Materials/");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fileName = Guid.NewGuid() + "_" + fuMaterial.FileName;
            fuMaterial.SaveAs(Path.Combine(folderPath, fileName));
            return "~/Uploads/Materials/" + fileName;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
            rfvFileUpload.Enabled = false;
        }

        protected void gvMaterials_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int materialId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditMaterial")
            {
                var material = _materialService.GetById(materialId);

                ddlWorkshops.SelectedValue = material.WorkshopID.ToString();
                txtTitle.Text = material.Title;
                txtDescription.Text = material.Description;

                ViewState["MaterialID"] = materialId;
                btnAdd.Visible = false;
                btnUpdate.Visible = true;
            }
            else if (e.CommandName == "DeleteMaterial")
            {
                _materialService.Delete(materialId);
                BindGrid();
            }
        }

        private void ClearForm()
        {
            ddlWorkshops.SelectedIndex = 0;
            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            fuMaterial.Attributes.Clear();
        }

        private void SendEmailNotification(string materialTitle, string workshopTitle, string action)
        {
            try
            {
                string subject = $"Material {action}: {materialTitle}";
                string body = $"Dear Student,<br/>Material <b>{materialTitle}</b> has been <b>{action}</b> for workshop: <b>{workshopTitle}</b>.<br/>Please check the portal.";
                _emailService.SendEmail("sachinkumarrana1005@gmail.com", subject, body);
            }
            catch (Exception ex)
            {
                // log exception if needed
                Console.WriteLine("Email failed: " + ex.Message);
            }
        }
    }
}