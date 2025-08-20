using System;
using OLMS.BLL.Services;
using OLMS.Models.Entities;
using OLMS.DAL.Repositories;
using OLMS.DAL.Interfaces;
using System.Configuration;

namespace OLMS.UI.Admin
{
    public partial class Trainer : System.Web.UI.Page
    {
        private readonly TrainerService _trainerService;

        public Trainer()
        {
            // Initialize repo & service once in constructor
            string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            ITrainerRepository trainerRepo = new TrainerRepository(connStr);
            _trainerService = new TrainerService(trainerRepo);
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
            gvTrainers.DataSource = _trainerService.GetAll();
            gvTrainers.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var trainer = new OLMS.Models.Entities.Trainer
                {
                    Name = txtName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Expertise = txtExpertise.Text.Trim(),
                    IsActive = chkIsActive.Checked
                };

                if (string.IsNullOrEmpty(hfTrainerId.Value))
                {
                    // New trainer
                    _trainerService.Add(trainer);
                }
                else
                {
                    // Update existing
                    trainer.TrainerID = Convert.ToInt32(hfTrainerId.Value);
                    _trainerService.Update(trainer);
                }

                BindGrid();
                ClearForm();
            }
            catch (Exception ex)
            {
                // Ideally log error (for now show alert)
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            hfTrainerId.Value = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtExpertise.Text = "";
            chkIsActive.Checked = false;
        }

        protected void gvTrainers_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int trainerId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditTrainer")
            {
                var trainer = _trainerService.GetById(trainerId);
                if (trainer != null)
                {
                    hfTrainerId.Value = trainer.TrainerID.ToString();
                    txtName.Text = trainer.Name;
                    txtEmail.Text = trainer.Email;
                    txtExpertise.Text = trainer.Expertise;
                    chkIsActive.Checked = trainer.IsActive;
                }
            }
            else if (e.CommandName == "DeleteTrainer")
            {
                _trainerService.Delete(trainerId);
                BindGrid();
            }
        }
    }
}