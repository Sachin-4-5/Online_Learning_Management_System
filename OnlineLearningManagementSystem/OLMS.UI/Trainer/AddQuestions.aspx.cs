using OLMS.BLL.Services;
using OLMS.Models.Entities;
using System;
using System.Web.UI.WebControls;

namespace OLMS.UI.Trainer
{
    public partial class AddQuestions : System.Web.UI.Page
    {
        private readonly QuestionService _questionService = new QuestionService();
        private readonly QuizService _quizService = new QuizService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindQuizzes();
            }
        }

        private void BindQuizzes()
        {
            var quizzes = _quizService.GetAll(); // Better than GetByCourseId(0)
            ddlQuizzes.DataSource = quizzes;
            ddlQuizzes.DataTextField = "Title";
            ddlQuizzes.DataValueField = "QuizID";
            ddlQuizzes.DataBind();

            if (ddlQuizzes.Items.Count > 0)
                BindGrid();
        }

        private void BindGrid()
        {
            int quizId = Convert.ToInt32(ddlQuizzes.SelectedValue);
            gvQuestions.DataSource = _questionService.GetByQuizId(quizId);
            gvQuestions.DataBind();
        }

        protected void ddlQuizzes_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMarks.Text.Trim(), out int marks))
            {
                return; // handled by validator, but extra safety
            }

            var question = new Question
            {
                QuizID = Convert.ToInt32(ddlQuizzes.SelectedValue),
                QuestionText = txtQuestionText.Text.Trim(),
                Marks = marks
            };

            _questionService.Add(question);
            ClearForm();
            BindGrid();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMarks.Text.Trim(), out int marks))
            {
                return;
            }

            var question = new Question
            {
                QuestionID = Convert.ToInt32(ViewState["QuestionID"]),
                QuizID = Convert.ToInt32(ddlQuizzes.SelectedValue),
                QuestionText = txtQuestionText.Text.Trim(),
                Marks = marks
            };

            _questionService.Update(question);
            ClearForm();

            btnAdd.Visible = true;
            btnUpdate.Visible = false;
            BindGrid();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void gvQuestions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditQuestion")
            {
                int questionId = Convert.ToInt32(e.CommandArgument);
                var question = _questionService.GetById(questionId); // cleaner

                txtQuestionText.Text = question.QuestionText;
                txtMarks.Text = question.Marks.ToString();
                ViewState["QuestionID"] = questionId;

                btnAdd.Visible = false;
                btnUpdate.Visible = true;
            }
            else if (e.CommandName == "DeleteQuestion")
            {
                int questionId = Convert.ToInt32(e.CommandArgument);
                _questionService.Delete(questionId);
                BindGrid();
            }
        }

        private void ClearForm()
        {
            txtQuestionText.Text = string.Empty;
            txtMarks.Text = string.Empty;
            ViewState["QuestionID"] = null;
        }
    }
}