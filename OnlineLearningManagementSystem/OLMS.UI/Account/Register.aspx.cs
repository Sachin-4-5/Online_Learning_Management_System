using OLMS.BLL.Services;
using OLMS.Models.Entities;
using System;
using System.Collections.Generic;

namespace OLMS.UI.Account
{
    public partial class Register : System.Web.UI.Page
    {
        private readonly UserService _userService;
        private readonly RoleService _roleService;

        public Register()
        {
            _userService = new UserService();
            _roleService = new RoleService(new OLMS.DAL.Repositories.RoleRepository());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRoles();
            }
        }

        private void LoadRoles()
        {
            try
            {
                List<Role> roles = _roleService.GetAll();
                ddlRole.DataSource = roles;
                ddlRole.DataTextField = "RoleName";
                ddlRole.DataValueField = "RoleID";
                ddlRole.DataBind();

                // Default role to Student
                ddlRole.SelectedValue = "3"; // 3 = Student
            }
            catch (Exception ex)
            {
                litMessage.Text = $"<div class='alert alert-danger'>Error loading roles: {ex.Message}</div>";
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                User newUser = new User
                {
                    FullName = txtFullName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    RoleID = Convert.ToInt32(ddlRole.SelectedValue),
                    IsActive = true,
                    DateCreated = DateTime.Now
                };

                _userService.Register(newUser, txtPassword.Text.Trim());

                litMessage.Text = "<div class='alert alert-success'>Registration successful! You can now <a href='Login.aspx'>login</a>.</div>";
                btnRegister.Enabled = false;
            }
            catch (Exception ex)
            {
                litMessage.Text = $"<div class='alert alert-danger'>Registration failed: {ex.Message}</div>";
            }
        }
    }
}