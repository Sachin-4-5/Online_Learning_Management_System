using OLMS.BLL.Services;
using System;
using System.Web;

namespace OLMS.UI.Admin
{
    public partial class Course : System.Web.UI.Page
    {
        private readonly CourseService _courseService;

        public Course()
        {
            _courseService = new CourseService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null || Convert.ToInt32(Session["RoleID"]) != 1)
                {
                    Response.Redirect("~/Account/Login.aspx");
                    return;
                }

                LoadCourses();
            }
        }

        private void LoadCourses()
        {
            gvCourses.DataSource = _courseService.GetAll();
            gvCourses.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var course = new OLMS.Models.Entities.Course
                {
                    Title = txtTitle.Text.Trim(),
                    Description = HttpUtility.HtmlEncode(txtDescription.Text.Trim()),
                    Category = txtCategory.Text.Trim(),
                    CreatedBy = Convert.ToInt32(Session["UserID"]),
                    CreatedDate = DateTime.Now,
                    IsActive = chkIsActive.Checked
                };

                if (!string.IsNullOrEmpty(hfCourseID.Value))
                {
                    course.CourseID = Convert.ToInt32(hfCourseID.Value);
                    _courseService.Update(course);
                }
                else
                {
                    _courseService.Add(course);
                }

                ClearForm();
                LoadCourses();
            }
            catch (Exception ex)
            {
                // Display error using ValidationSummary
                vsSummary.HeaderText = "Error: " + ex.Message;
                vsSummary.Visible = true;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            hfCourseID.Value = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtCategory.Text = "";
            chkIsActive.Checked = true;
        }

        protected void gvCourses_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int courseId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditCourse")
            {
                var course = _courseService.GetById(courseId);
                if (course != null)
                {
                    hfCourseID.Value = course.CourseID.ToString();
                    txtTitle.Text = course.Title;
                    txtDescription.Text = HttpUtility.HtmlDecode(course.Description);
                    txtCategory.Text = course.Category;
                    chkIsActive.Checked = course.IsActive;
                }
            }
            else if (e.CommandName == "DeleteCourse")
            {
                _courseService.Delete(courseId);
                LoadCourses();
            }
        }

        protected void gvCourses_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvCourses.PageIndex = e.NewPageIndex;
            LoadCourses();
        }
    }
}