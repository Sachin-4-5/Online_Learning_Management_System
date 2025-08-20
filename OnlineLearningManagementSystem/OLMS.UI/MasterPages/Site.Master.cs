using System;

namespace OLMS.UI.MasterPages
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMenus();
            }
        }

        private void BindMenus()
        {
            if (Session["UserID"] != null)
            {
                phLoggedOut.Visible = false;
                phLoggedIn.Visible = true;

                lblUserName.Text = "Hello, " + (Session["FullName"] ?? "User");

                int roleId = Convert.ToInt32(Session["RoleID"]);

                // Admin
                phAdminMenu.Visible = roleId == 1;

                // Trainer
                phTrainerMenu.Visible = roleId == 2;

                // Student
                phStudentMenu.Visible = roleId == 3;
            }
            else
            {
                phLoggedOut.Visible = true;
                phLoggedIn.Visible = false;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Account/Login.aspx");
        }
    }
}