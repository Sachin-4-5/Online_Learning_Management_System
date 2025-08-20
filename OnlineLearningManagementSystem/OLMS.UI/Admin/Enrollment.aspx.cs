using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using OLMS.BLL.Services;
using OLMS.DAL.Repositories;
using OLMS.Models.Entities;

namespace OLMS.UI.Admin
{
    public partial class Enrollment : System.Web.UI.Page
    {
        private EnrollmentService _enrollmentService;
        private CourseService _courseService;
        private UserService _userService;

        // simple view model for the grid
        private class EnrollmentVM
        {
            public int EnrollmentID { get; set; }
            public string CourseName { get; set; }
            public string StudentName { get; set; }
            public DateTime EnrolledDate { get; set; }
            public string Status { get; set; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // Services (no interface changes needed)
            _enrollmentService = new EnrollmentService();
            _courseService = new CourseService();
            _userService = new UserService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCourses();
                BindStudents();
                BindEnrollments();
            }
        }

        private void BindCourses()
        {
            var courses = _courseService.GetAll() ?? new List<OLMS.Models.Entities.Course>();
            // Only active courses shown (if you prefer all, remove the Where)
            var data = courses.Where(c => c.IsActive).OrderBy(c => c.Title).ToList();

            ddlCourses.DataSource = data;
            ddlCourses.DataTextField = "Title";
            ddlCourses.DataValueField = "CourseID";
            ddlCourses.DataBind();
            ddlCourses.Items.Insert(0, new ListItem("-- Select Course --", ""));
        }

        private void BindStudents()
        {
            const int StudentRoleId = 3;
            var allUsers = _userService.GetAll();
            var students = allUsers
                .Where(u => u.RoleID == StudentRoleId && u.IsActive)
                .OrderBy(u => u.FullName)
                .ToList();

            ddlStudents.DataSource = students;
            ddlStudents.DataTextField = "FullName";
            ddlStudents.DataValueField = "UserID";
            ddlStudents.DataBind();
            ddlStudents.Items.Insert(0, new ListItem("-- Select Student --", ""));
        }

        private void BindEnrollments()
        {
            // Build a view from existing service data (no direct DbHelper use in UI)
            var allCourses = _courseService.GetAll() ?? new List<OLMS.Models.Entities.Course>();
            var courseMap = allCourses.ToDictionary(c => c.CourseID, c => c.Title);

            const int StudentRoleId = 3;
            var allUsers = _userService.GetAll();
            var students = allUsers.Where(u => u.RoleID == StudentRoleId).ToDictionary(u => u.UserID, u => u.FullName);

            var rows = new List<EnrollmentVM>();

            // For each student, fetch their enrollments
            foreach (var kv in students)
            {
                int userId = kv.Key;
                string name = kv.Value;

                var enrollments = _enrollmentService.GetByUserId(userId);
                foreach (var e in enrollments)
                {
                    rows.Add(new EnrollmentVM
                    {
                        EnrollmentID = e.EnrollmentID,
                        CourseName = courseMap.ContainsKey(e.CourseID) ? courseMap[e.CourseID] : $"Course #{e.CourseID}",
                        StudentName = name,
                        EnrolledDate = e.EnrolledDate,
                        Status = e.Status
                    });
                }
            }

            gvEnrollments.DataSource = rows
                .OrderByDescending(r => r.EnrolledDate)
                .ThenBy(r => r.CourseName)
                .ToList();
            gvEnrollments.DataBind();
        }

        protected void btnEnroll_Click(object sender, EventArgs e)
        {
            lblMessage.CssClass = "fw-semibold";
            lblMessage.Text = "";

            if (string.IsNullOrEmpty(ddlCourses.SelectedValue))
            {
                lblMessage.CssClass = "text-danger fw-semibold";
                lblMessage.Text = "Please select a course.";
                return;
            }
            if (string.IsNullOrEmpty(ddlStudents.SelectedValue))
            {
                lblMessage.CssClass = "text-danger fw-semibold";
                lblMessage.Text = "Please select a student.";
                return;
            }

            int courseId = Convert.ToInt32(ddlCourses.SelectedValue);
            int userId = Convert.ToInt32(ddlStudents.SelectedValue);

            // Prevent duplicate enrollment
            var existing = _enrollmentService.GetByUserId(userId)
                .Any(x => x.CourseID == courseId && (x.Status ?? "").ToLower() != "deleted");
            if (existing)
            {
                lblMessage.CssClass = "text-warning fw-semibold";
                lblMessage.Text = "This student is already enrolled in the selected course.";
                return;
            }

            try
            {
                _enrollmentService.Enroll(new OLMS.Models.Entities.Enrollment
                {
                    CourseID = courseId,
                    UserID = userId,
                    Status = "Active"
                });

                lblMessage.CssClass = "text-success fw-semibold";
                lblMessage.Text = "Student enrolled successfully.";
                BindEnrollments();
            }
            catch (Exception ex)
            {
                lblMessage.CssClass = "text-danger fw-semibold";
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void gvEnrollments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int enrollmentId = Convert.ToInt32(gvEnrollments.DataKeys[e.RowIndex].Value);
                _enrollmentService.Delete(enrollmentId);

                lblMessage.CssClass = "text-success fw-semibold";
                lblMessage.Text = "Enrollment deleted successfully.";
                BindEnrollments();
            }
            catch (Exception ex)
            {
                lblMessage.CssClass = "text-danger fw-semibold";
                lblMessage.Text = "Delete failed: " + ex.Message;
            }
        }

        protected void gvEnrollments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEnrollments.PageIndex = e.NewPageIndex;
            BindEnrollments();
        }
    }
}
