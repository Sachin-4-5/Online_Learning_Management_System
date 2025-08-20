using System;

namespace OLMS.Models.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RoleID { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; } // Added for Role display
    }
}
