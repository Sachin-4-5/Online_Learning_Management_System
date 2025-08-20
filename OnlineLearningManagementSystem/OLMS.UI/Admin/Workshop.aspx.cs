using OLMS.BLL.Services;
using OLMS.Models.Entities;
using System;

namespace OLMS.UI.Admin
{
    public partial class Workshop : System.Web.UI.Page
    {
        private WorkshopService _workshopService;

        protected void Page_Init(object sender, EventArgs e)
        {
            _workshopService = new WorkshopService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gvWorkshops.DataSource = _workshopService.GetAll();
            gvWorkshops.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtTrainerID.Text.Trim(), out int trainerId))
                return;

            if (!DateTime.TryParse(txtStartDate.Text, out DateTime startDate) ||
                !DateTime.TryParse(txtEndDate.Text, out DateTime endDate))
                return;

            var workshop = new OLMS.Models.Entities.Workshop
            {
                Title = txtTitle.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                TrainerID = trainerId,
                StartDate = startDate,
                EndDate = endDate,
                Location = txtLocation.Text.Trim(),
                IsActive = chkIsActive.Checked
            };

            _workshopService.Add(workshop);
            ClearForm();   // Use the new method
            BindGrid();
        }

        protected void gvWorkshops_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = (int)gvWorkshops.DataKeys[e.RowIndex].Value;
            _workshopService.Delete(id);
            BindGrid();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        /// <summary>
        /// Clears all input fields and resets the form
        /// </summary>
        private void ClearForm()
        {
            hfWorkshopID.Value = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtTrainerID.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtLocation.Text = "";
            chkIsActive.Checked = true;
        }
    }
}