using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        public int Add(Enrollment enrollment)
        {
            var obj = DbHelper.ExecuteScalar("sp_Enrollment_Add", new SqlParameter[]
            {
                new SqlParameter("@CourseID", enrollment.CourseID),
                new SqlParameter("@UserID", enrollment.UserID),
                new SqlParameter("@Status", enrollment.Status ?? (object)DBNull.Value)
            });
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        public int UpdateStatus(int enrollmentId, string status)
        {
            return DbHelper.ExecuteNonQuery("sp_Enrollment_UpdateStatus", new SqlParameter[]
            {
                new SqlParameter("@EnrollmentID", enrollmentId),
                new SqlParameter("@Status", status)
            });
        }

        public int Delete(int enrollmentId) => DbHelper.ExecuteNonQuery("sp_Enrollment_Delete", new SqlParameter[] { new SqlParameter("@EnrollmentID", enrollmentId) });

        public DataTable GetByUser(int userId) => DbHelper.ExecuteDataTable("sp_Enrollment_GetByUser", new SqlParameter[] { new SqlParameter("@UserID", userId) });

        public DataTable GetByCourse(int courseId) => DbHelper.ExecuteDataTable("sp_Enrollment_GetByCourse", new SqlParameter[] { new SqlParameter("@CourseID", courseId) });
    }
}
