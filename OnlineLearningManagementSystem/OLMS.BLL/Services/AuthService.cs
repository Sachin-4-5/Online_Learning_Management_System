// In BLL -> Services -> AuthService.cs
using System;
using System.Web;

namespace OLMS.BLL.Services
{
    public class AuthService
    {
        public static void EnsureAdmin()
        {
            if (HttpContext.Current.Session["RoleID"] == null || HttpContext.Current.Session["RoleID"].ToString() != "1") // Assuming 1 = Admin
            {
                HttpContext.Current.Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}