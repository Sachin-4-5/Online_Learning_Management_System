using OLMS.BLL.Services;
using OLMS.Models.Entities;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OLMS.UI.Admin
{
    public partial class AddQuiz : Page
    {
        private QuizService _quizService;
        private CourseService _courseService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Session & Role Check
                if (Session["UserID"] == null || Convert.ToInt32(Session["RoleID"]) != 1)
                {
                    Response.Redirect("~/Account/Login.aspx");
                    return;
                }

                _quizService = new QuizService();
                _courseService = new CourseService();

                BindCourses();
                BindGrid();
            }
        }

        private void BindCourses()
        {
            var courses = _courseService.GetAll();
            ddlCourses.DataSource = courses;
            ddlCourses.DataTextField = "Title";
            ddlCourses.DataValueField = "CourseID";
            ddlCourses.DataBind();

            ddlCourses.Items.Insert(0, new ListItem("-- Select Course --", "0"));
        }

        private void BindGrid()
        {
            int courseId = Convert.ToInt32(ddlCourses.SelectedValue);
            gvQuizzes.DataSource = courseId > 0 ? _quizService.GetByCourseId(courseId) : null;
            gvQuizzes.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int courseId = Convert.ToInt32(ddlCourses.SelectedValue);
            if (courseId <= 0)
            {
                // show error
                return;
            }

            var quiz = new Quiz
            {
                CourseID = courseId,
                Title = txtTitle.Text.Trim(),
                TotalMarks = Convert.ToInt32(txtTotalMarks.Text.Trim())
            };

            _quizService.Add(quiz);
            ClearForm();
            BindGrid();
        }

        protected void gvQuizzes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int courseId = Convert.ToInt32(ddlCourses.SelectedValue);

            if (e.CommandName == "EditQuiz")
            {
                int quizId = Convert.ToInt32(e.CommandArgument);
                var quiz = _quizService.GetByCourseId(courseId).FirstOrDefault(q => q.QuizID == quizId);

                if (quiz != null)
                {
                    ddlCourses.SelectedValue = quiz.CourseID.ToString();
                    txtTitle.Text = quiz.Title;
                    txtTotalMarks.Text = quiz.TotalMarks.ToString();
                    ViewState["QuizID"] = quizId;
                }
            }
            else if (e.CommandName == "DeleteQuiz")
            {
                int quizId = Convert.ToInt32(e.CommandArgument);
                _quizService.Delete(quizId);
                BindGrid();
            }
        }

        private void ClearForm()
        {
            txtTitle.Text = string.Empty;
            txtTotalMarks.Text = string.Empty;
            ddlCourses.SelectedIndex = 0;
            ViewState["QuizID"] = null;
        }
    }
}