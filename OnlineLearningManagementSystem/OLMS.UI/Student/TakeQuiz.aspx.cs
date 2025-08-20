using OLMS.BLL.Services;
using OLMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OLMS.UI.Student
{
    public partial class TakeQuiz : System.Web.UI.Page
    {
        private QuizService _quizService = new QuizService();
        private QuestionService _questionService = new QuestionService();
        private StudentQuizService _studentQuizService = new StudentQuizService();
        private EnrollmentService _enrollmentService = new EnrollmentService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindQuizzes();
            }
        }

        private void BindQuizzes()
        {
            if (Session["UserID"] == null)
            {
                ddlQuizzes.Items.Clear();
                ddlQuizzes.Items.Add(new ListItem("-- Not logged in --", "0"));
                return;
            }

            int studentId = Convert.ToInt32(Session["UserID"]);

            // 1️⃣ Get enrolled active courses for student
            var enrollments = _enrollmentService.GetByUserId(studentId);
            var courseIds = enrollments
                            .Where(e => e.Status == "Active")
                            .Select(e => e.CourseID)
                            .ToList();

            if (courseIds.Count == 0)
            {
                ddlQuizzes.Items.Clear();
                ddlQuizzes.Items.Add(new ListItem("-- No active course enrollment found --", "0"));
                return;
            }

            // 2️⃣ Get quizzes for those courses
            var quizzes = new List<Quiz>();
            foreach (int courseId in courseIds)
            {
                var courseQuizzes = _quizService.GetByCourseId(courseId);
                if (courseQuizzes != null && courseQuizzes.Any())
                    quizzes.AddRange(courseQuizzes);
            }

            // 3️⃣ Bind dropdown
            if (quizzes.Count > 0)
            {
                ddlQuizzes.DataSource = quizzes;
                ddlQuizzes.DataTextField = "Title";
                ddlQuizzes.DataValueField = "QuizID";
                ddlQuizzes.DataBind();
                ddlQuizzes.Items.Insert(0, new ListItem("-- Select Quiz --", "0"));
            }
            else
            {
                ddlQuizzes.Items.Clear();
                ddlQuizzes.Items.Add(new ListItem("-- No quizzes found for enrolled courses --", "0"));
            }
        }

        protected void ddlQuizzes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int quizId = Convert.ToInt32(ddlQuizzes.SelectedValue);
            if (quizId > 0)
            {
                var questions = _questionService.GetByQuizId(quizId);
                RenderQuestions(questions);
                pnlQuestions.Visible = true;
            }
            else
            {
                pnlQuestions.Visible = false;
                phQuestions.Controls.Clear();
            }
        }

        private void RenderQuestions(List<Question> questions)
        {
            phQuestions.Controls.Clear();

            foreach (var q in questions)
            {
                Label lblQ = new Label { Text = q.QuestionText, ID = "lblQ_" + q.QuestionID };
                TextBox txtA = new TextBox { ID = "txtQ_" + q.QuestionID, Width = 400 };
                phQuestions.Controls.Add(lblQ);
                phQuestions.Controls.Add(new Literal { Text = "<br/>" });
                phQuestions.Controls.Add(txtA);
                phQuestions.Controls.Add(new Literal { Text = "<br/><br/>" });
            }
        }

        protected void btnSubmitQuiz_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                lblMessage.Text = "You must be logged in as a student to submit quiz.";
                return;
            }

            int studentId = Convert.ToInt32(Session["UserID"]);
            int quizId = Convert.ToInt32(ddlQuizzes.SelectedValue);

            var questions = _questionService.GetByQuizId(quizId);

            int score = 0;
            int studentQuizId = _studentQuizService.Add(
                new StudentQuiz { StudentID = studentId, QuizID = quizId, Score = 0 }
            );

            foreach (var q in questions)
            {
                TextBox txt = phQuestions.FindControl("txtQ_" + q.QuestionID) as TextBox;
                if (txt == null) continue;

                string answer = txt.Text.Trim();
                bool isCorrect = string.Equals(answer, q.CorrectAnswer, StringComparison.OrdinalIgnoreCase);

                if (isCorrect) score += q.Marks;

                _studentQuizService.AddAnswer(studentQuizId, q.QuestionID, answer, isCorrect);
            }

            _studentQuizService.UpdateScore(studentQuizId, score);

            lblMessage.Text = $"✅ Quiz submitted successfully! Your score: {score}/{questions.Sum(q => q.Marks)}";
            pnlQuestions.Visible = false;
        }
    }
}