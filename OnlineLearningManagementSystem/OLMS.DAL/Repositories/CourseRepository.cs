using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public int Add(Course course)
        {
            var obj = DbHelper.ExecuteScalar("sp_Course_Add", new SqlParameter[]
            {
                new SqlParameter("@Title", course.Title),
                new SqlParameter("@Description", course.Description ?? (object)DBNull.Value),
                new SqlParameter("@Category", course.Category ?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", course.CreatedBy)
            });
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        public int Update(Course course)
        {
            return DbHelper.ExecuteNonQuery("sp_Course_Update", new SqlParameter[]
            {
                new SqlParameter("@CourseID", course.CourseID),
                new SqlParameter("@Title", course.Title),
                new SqlParameter("@Description", course.Description ?? (object)DBNull.Value),
                new SqlParameter("@Category", course.Category ?? (object)DBNull.Value),
                new SqlParameter("@IsActive", course.IsActive)
            });
        }

        // Soft delete: mark course as inactive instead of removing
        public int Delete(int courseId)
        {
            return DbHelper.ExecuteNonQuery("sp_Course_SoftDelete",
                new SqlParameter[] { new SqlParameter("@CourseID", courseId) });
        }

        // Corrected GetById: call stored procedure instead of passing SQL query
        public DataTable GetById(int courseId)
        {
            return DbHelper.ExecuteDataTable("sp_Course_GetById",
                new SqlParameter[] { new SqlParameter("@CourseID", courseId) });
        }

        public DataTable GetAll()
        {
            // Only get active courses
            return DbHelper.ExecuteDataTable("sp_Course_GetAllActive", null);
        }
    }
}