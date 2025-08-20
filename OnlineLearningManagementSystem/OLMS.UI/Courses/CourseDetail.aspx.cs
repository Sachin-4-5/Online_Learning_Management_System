using System;
using OLMS.BLL.Services;

namespace OLMS.UI.Courses
{
    public partial class CourseDetail : System.Web.UI.Page
    {
        private readonly CourseService _courseService;

        public CourseDetail()
        {
            _courseService = new CourseService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCourse();
            }
        }

        private void LoadCourse()
        {
            try
            {
                if (Request.QueryString["CourseID"] != null &&
                    int.TryParse(Request.QueryString["CourseID"], out int courseId))
                {
                    var course = _courseService.GetById(courseId);
                    if (course != null)
                    {
                        lblTitle.Text = course.Title;
                        lblDescription.Text = course.Description;
                        lblCategory.Text = course.Category;
                        lblCreatedDate.Text = course.CreatedDate.ToString("dd-MMM-yyyy");

                        pnlCourse.Visible = true;
                    }
                    else
                    {
                        ShowError("Course not found.");
                    }
                }
                else
                {
                    ShowError("Invalid Course ID.");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error: {ex.Message}");
            }
        }

        private void ShowError(string message)
        {
            pnlError.Visible = true;
            lblError.Text = message;
            pnlCourse.Visible = false;
        }
    }
}