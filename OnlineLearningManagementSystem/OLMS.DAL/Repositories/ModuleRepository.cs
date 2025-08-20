using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        public int Add(Module module)
        {
            var obj = DbHelper.ExecuteScalar("sp_Module_Add", new SqlParameter[]
            {
                new SqlParameter("@CourseID", module.CourseID),
                new SqlParameter("@Title", module.Title),
                new SqlParameter("@Description", module.Description ?? (object)DBNull.Value),
                new SqlParameter("@SortOrder", module.SortOrder)
            });
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        public int Update(Module module)
        {
            return DbHelper.ExecuteNonQuery("sp_Module_Update", new SqlParameter[]
            {
                new SqlParameter("@ModuleID", module.ModuleID),
                new SqlParameter("@Title", module.Title),
                new SqlParameter("@Description", module.Description ?? (object)DBNull.Value),
                new SqlParameter("@SortOrder", module.SortOrder)
            });
        }

        public int Delete(int moduleId) => DbHelper.ExecuteNonQuery("sp_Module_Delete", new SqlParameter[] { new SqlParameter("@ModuleID", moduleId) });

        public DataTable GetAllByCourse(int courseId) => DbHelper.ExecuteDataTable("sp_Module_GetByCourse", new SqlParameter[] { new SqlParameter("@CourseID", courseId) });

        public DataTable GetById(int moduleId) => DbHelper.ExecuteDataTable("sp_Module_GetById", new SqlParameter[] { new SqlParameter("@ModuleID", moduleId) });

        public DataTable GetAll()
        {
            return DbHelper.ExecuteDataTable("SELECT * FROM Modules", null);
        }
    }
}
