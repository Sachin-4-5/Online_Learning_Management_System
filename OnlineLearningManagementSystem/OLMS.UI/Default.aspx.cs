using System;
using System.Web.UI;

namespace OLMS.UI
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserInfo();
            }
        }

        private void LoadUserInfo()
        {
            if (Session["UserID"] != null)
            {
                string fullName = Session["FullName"]?.ToString() ?? "User";
                int roleId = Convert.ToInt32(Session["RoleID"]);

                litGreeting.Text = $"<h4 class='text-success'>Hello, {fullName}!</h4>";

                // Show quick links panel
                pnlQuickLinks.Visible = true;

                // Show role-based quick links
                pnlAdmin.Visible = roleId == 1;
                pnlTrainer.Visible = roleId == 2;
                pnlStudent.Visible = roleId == 3;
            }
            else
            {
                litGreeting.Text = "<p class='text-muted'>Login to access personalized features.</p>";
                pnlQuickLinks.Visible = false;
            }
        }
    }
}