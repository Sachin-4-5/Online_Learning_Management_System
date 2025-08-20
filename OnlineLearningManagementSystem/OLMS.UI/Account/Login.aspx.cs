using OLMS.BLL.Services;
using System;
using System.Web;

namespace OLMS.UI.Account
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly UserService _userService;

        public Login()
        {
            _userService = new UserService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Clear();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var user = _userService.Login(txtEmail.Text.Trim(), txtPassword.Text.Trim());

                if (user != null)
                {
                    Session["UserID"] = user.UserID;
                    Session["FullName"] = user.FullName;
                    Session["RoleID"] = user.RoleID;

                    switch (user.RoleID)
                    {
                        case 1: // Admin
                            Response.Redirect("~/Admin/User.aspx");
                            break;
                        case 2: // Trainer
                            Response.Redirect("~/Trainer/AddQuestions.aspx");
                            break;
                        case 3: // Student
                            Response.Redirect("~/Student/TakeQuiz.aspx");
                            break;
                        default:
                            Response.Redirect("~/Default.aspx");
                            break;
                    }
                }
                else
                {
                    litMessage.Text = "<div class='alert alert-danger'>Invalid email or password.</div>";
                }
            }
            catch (Exception ex)
            {
                litMessage.Text = $"<div class='alert alert-danger'>Login failed: {ex.Message}</div>";
            }
        }
    }
}