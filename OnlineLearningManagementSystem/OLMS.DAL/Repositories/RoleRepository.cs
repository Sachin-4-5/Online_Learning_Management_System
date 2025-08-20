using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public int Add(Role role)
        {
            object obj = DbHelper.ExecuteScalar("sp_Role_Add",
                new SqlParameter[]
                {
                    new SqlParameter("@RoleName", role.RoleName)
                });

            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        public int Update(Role role)
        {
            return DbHelper.ExecuteNonQuery("sp_Role_Update",
                new SqlParameter[]
                {
                    new SqlParameter("@RoleID", role.RoleID),
                    new SqlParameter("@RoleName", role.RoleName)
                });
        }

        public int Delete(int roleId)
        {
            return DbHelper.ExecuteNonQuery("sp_Role_Delete",
                new SqlParameter[]
                {
                    new SqlParameter("@RoleID", roleId)
                });
        }

        public DataTable GetAll()
        {
            return DbHelper.ExecuteDataTable("sp_Role_GetAll", null);
        }

        public DataTable GetById(int roleId)
        {
            return DbHelper.ExecuteDataTable("sp_Role_GetById",
                new SqlParameter[]
                {
                    new SqlParameter("@RoleID", roleId)
                });
        }
    }
}