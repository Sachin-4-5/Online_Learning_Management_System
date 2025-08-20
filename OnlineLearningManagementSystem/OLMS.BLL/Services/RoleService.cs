using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System.Data;
using System;
using System.Collections.Generic;

namespace OLMS.BLL.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _repo;

        public RoleService(IRoleRepository repo)
        {
            _repo = repo;
        }

        public List<Role> GetAll()
        {
            DataTable dt = _repo.GetAll();
            List<Role> roles = new List<Role>();
            foreach (DataRow row in dt.Rows)
            {
                roles.Add(new Role
                {
                    RoleID = Convert.ToInt32(row["RoleID"]),
                    RoleName = row["RoleName"].ToString()
                });
            }
            return roles;
        }

        public Role GetById(int roleId)
        {
            DataTable dt = _repo.GetById(roleId);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            return new Role
            {
                RoleID = Convert.ToInt32(row["RoleID"]),
                RoleName = row["RoleName"].ToString()
            };
        }

        /// <summary>
        /// Convenience method to fetch only RoleName by RoleID
        /// </summary>
        public string GetRoleNameById(int roleId)
        {
            var role = GetById(roleId);
            return role != null ? role.RoleName : string.Empty;
        }

        public void Add(Role role)
        {
            int result = _repo.Add(role);
            if (result <= 0) throw new Exception("Failed to add role.");
        }

        public void Update(Role role)
        {
            int result = _repo.Update(role);
            if (result <= 0) throw new Exception("Failed to update role.");
        }

        public void Delete(int roleId)
        {
            int result = _repo.Delete(roleId);
            if (result <= 0) throw new Exception("Failed to delete role.");
        }
    }
}