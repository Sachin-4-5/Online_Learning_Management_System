using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OLMS.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        public int Add(User user)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@FullName", user.FullName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@PasswordHash", user.PasswordHash),
                new SqlParameter("@RoleID", user.RoleID),
                new SqlParameter("@IsActive", user.IsActive)
            };
            var obj = DbHelper.ExecuteScalar("sp_User_Add", parameters);
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        public int Update(User user)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", user.UserID),
                new SqlParameter("@FullName", user.FullName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@PasswordHash", user.PasswordHash),
                new SqlParameter("@RoleID", user.RoleID),
                new SqlParameter("@IsActive", user.IsActive)
            };
            return DbHelper.ExecuteNonQuery("sp_User_Update", parameters);
        }

        public int Delete(int userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserID", userId) };
            return DbHelper.ExecuteNonQuery("sp_User_Delete", parameters);
        }

        public List<User> GetAll()
        {
            var dt = DbHelper.ExecuteDataTable("sp_User_GetAll", null);
            var list = new List<User>();

            foreach (DataRow r in dt.Rows)
            {
                list.Add(new User
                {
                    UserID = Convert.ToInt32(r["UserID"]),
                    FullName = r["FullName"].ToString(),
                    Email = r["Email"].ToString(),
                    RoleID = Convert.ToInt32(r["RoleID"]),
                    IsActive = Convert.ToBoolean(r["IsActive"]),
                    DateCreated = Convert.ToDateTime(r["DateCreated"])
                });
            }

            return list;
        }

        public User GetById(int userId)
        {
            var dt = DbHelper.ExecuteDataTable("sp_User_GetById", new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            });

            if (dt.Rows.Count == 0) return null;
            var r = dt.Rows[0];

            return new User
            {
                UserID = Convert.ToInt32(r["UserID"]),
                FullName = r["FullName"].ToString(),
                Email = r["Email"].ToString(),
                RoleID = Convert.ToInt32(r["RoleID"]),
                IsActive = Convert.ToBoolean(r["IsActive"]),
                DateCreated = Convert.ToDateTime(r["DateCreated"])
            };
        }

        public User GetByEmail(string email)
        {
            var dt = DbHelper.ExecuteDataTable("sp_User_GetByEmail", new SqlParameter[]
            {
                new SqlParameter("@Email", email)
            });

            if (dt.Rows.Count == 0) return null;
            var r = dt.Rows[0];

            return new User
            {
                UserID = Convert.ToInt32(r["UserID"]),
                FullName = r["FullName"].ToString(),
                Email = r["Email"].ToString(),
                PasswordHash = r["PasswordHash"].ToString(),
                RoleID = Convert.ToInt32(r["RoleID"]),
                IsActive = Convert.ToBoolean(r["IsActive"]),
                DateCreated = Convert.ToDateTime(r["DateCreated"])
            };
        }

    }
}