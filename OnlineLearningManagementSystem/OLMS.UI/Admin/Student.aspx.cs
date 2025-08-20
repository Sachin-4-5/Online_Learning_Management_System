using OLMS.BLL.Services;
using System;
using System.Web.UI.WebControls;
using OLMS.Models.Entities;

namespace OLMS.UI.Admin
{
    public partial class Student : System.Web.UI.Page
    {
        private readonly StudentService _studentService;
        private readonly WorkshopService _workshopService;

        public Student()
        {
            _studentService = new StudentService();
            _workshopService = new WorkshopService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindWorkshops();
                BindGrid();
            }
        }

        private void BindWorkshops()
        {
            ddlWorkshops.DataSource = _workshopService.GetAll();
            ddlWorkshops.DataTextField = "Title";
            ddlWorkshops.DataValueField = "WorkshopID";
            ddlWorkshops.DataBind();
            ddlWorkshops.Items.Insert(0, new ListItem("-- Select Workshop --", "0"));
        }

        private void BindGrid()
        {
            gvStudents.DataSource = _studentService.GetAll();
            gvStudents.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var student = new OLMS.Models.Entities.Student
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                WorkshopID = Convert.ToInt32(ddlWorkshops.SelectedValue)
            };

            _studentService.Add(student); // ✅ Corrected (was Update)
            ClearForm();
            BindGrid();
            litMessage.Text = "<div class='alert alert-success'>Student added successfully.</div>";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ViewState["StudentID"] == null) return;

            int studentId = Convert.ToInt32(ViewState["StudentID"]);
            var student = new OLMS.Models.Entities.Student
            {
                StudentID = studentId,
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                WorkshopID = Convert.ToInt32(ddlWorkshops.SelectedValue)
            };

            _studentService.Update(student);
            ClearForm();
            BindGrid();
            litMessage.Text = "<div class='alert alert-success'>Student updated successfully.</div>";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            ddlWorkshops.SelectedIndex = 0;
            ViewState["StudentID"] = null;
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
            btnCancel.Visible = false;
        }

        protected void gvStudents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int studentId = Convert.ToInt32(gvStudents.DataKeys[e.NewEditIndex].Value);
            var student = _studentService.GetById(studentId);

            txtFirstName.Text = student.FirstName;
            txtLastName.Text = student.LastName;
            txtEmail.Text = student.Email;
            txtPhone.Text = student.Phone;
            ddlWorkshops.SelectedValue = student.WorkshopID.ToString();

            ViewState["StudentID"] = studentId;
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;
        }

        protected void gvStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int studentId = Convert.ToInt32(gvStudents.DataKeys[e.RowIndex].Value);
            _studentService.Delete(studentId);
            BindGrid();
            litMessage.Text = "<div class='alert alert-success'>Student deleted successfully.</div>";
        }

        protected void gvStudents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ClearForm();
        }
    }
}