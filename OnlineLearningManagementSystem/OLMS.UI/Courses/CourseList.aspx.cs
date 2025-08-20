using OLMS.BLL.Services;
using System;

namespace OLMS.UI.Courses
{
    public partial class CourseList : System.Web.UI.Page
    {
        private readonly CourseService _courseService;

        public CourseList()
        {
            _courseService = new CourseService(); // Use only BLL
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCourses();
            }
        }

        private void BindCourses()
        {
            try
            {
                var courses = _courseService.GetAll();

                if (courses != null && courses.Count > 0)
                {
                    rptCourses.DataSource = courses;
                    rptCourses.DataBind();
                }
                else
                {
                    rptCourses.Controls.Add(new System.Web.UI.LiteralControl(
                        "<div class='alert alert-info'>No courses available at the moment.</div>"));
                }
            }
            catch (Exception ex)
            {
                rptCourses.Controls.Add(new System.Web.UI.LiteralControl(
                    $"<div class='alert alert-danger'>Error loading courses: {ex.Message}</div>"));
            }
        }
    }
}