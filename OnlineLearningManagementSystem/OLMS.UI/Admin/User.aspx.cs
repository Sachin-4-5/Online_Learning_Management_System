using System;
using OLMS.BLL.Services;
using OLMS.Models.Entities;

namespace OLMS.UI.Admin
{
    public partial class User : System.Web.UI.Page
    {
        private readonly UserService _userService;

        public User()
        {
            _userService = new UserService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUserGrid();
            }
        }

        private void BindUserGrid()
        {
            gvUsers.DataSource = _userService.GetAll(); // now using service, not repo directly
            gvUsers.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = string.IsNullOrEmpty(hfUserID.Value) ? 0 : Convert.ToInt32(hfUserID.Value);

                var user = new OLMS.Models.Entities.User
                {
                    UserID = userId,
                    FullName = txtFullName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    RoleID = Convert.ToInt32(ddlRole.SelectedValue),
                    IsActive = chkIsActive.Checked,
                    DateCreated = DateTime.Now
                };

                if (userId == 0)
                {
                    // Register new user
                    _userService.Register(user, txtPassword.Text.Trim());
                }
                else
                {
                    // Update existing user
                    _userService.Update(user);
                }

                ClearForm();
                BindUserGrid();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            hfUserID.Value = "";
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            ddlRole.SelectedIndex = 0;
            chkIsActive.Checked = true;
        }

        protected void gvUsers_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int userId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditUser")
            {
                var user = _userService.GetById(userId);
                if (user != null)
                {
                    hfUserID.Value = user.UserID.ToString();
                    txtFullName.Text = user.FullName;
                    txtEmail.Text = user.Email;
                    ddlRole.SelectedValue = user.RoleID.ToString();
                    chkIsActive.Checked = user.IsActive;

                    // leave password blank for security
                    txtPassword.Text = "";
                }
            }
            else if (e.CommandName == "DeleteUser")
            {
                try
                {
                    _userService.Delete(userId);
                    BindUserGrid();
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Error: {ex.Message}');</script>");
                }
            }
        }
    }
}