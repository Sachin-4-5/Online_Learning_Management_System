using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class ApprovalRepository : IApprovalRepository
    {
        private readonly string _connectionString;

        public ApprovalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Approvals";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataRow GetById(int approvalId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Approvals WHERE ApprovalID=@ApprovalID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ApprovalID", approvalId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
        }

        public DataTable GetByStudentId(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Approvals WHERE StudentID=@StudentID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public int Add(Approval approval)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    INSERT INTO Approvals (StudentID, WorkshopID, MaterialID, Status, ApprovedBy, ApprovalDate, Comments, CreatedOn)
                    VALUES (@StudentID, @WorkshopID, @MaterialID, @Status, @ApprovedBy, @ApprovalDate, @Comments, @CreatedOn)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", approval.StudentID);
                    cmd.Parameters.AddWithValue("@WorkshopID", approval.WorkshopID);
                    cmd.Parameters.AddWithValue("@MaterialID", (object)approval.MaterialID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", approval.Status);
                    cmd.Parameters.AddWithValue("@ApprovedBy", approval.ApprovedBy ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ApprovalDate", (object)approval.ApprovalDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Comments", approval.Comments ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedOn", approval.CreatedOn);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int Update(Approval approval)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    UPDATE Approvals
                    SET StudentID=@StudentID, WorkshopID=@WorkshopID, MaterialID=@MaterialID, Status=@Status,
                        ApprovedBy=@ApprovedBy, ApprovalDate=@ApprovalDate, Comments=@Comments
                    WHERE ApprovalID=@ApprovalID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ApprovalID", approval.ApprovalID);
                    cmd.Parameters.AddWithValue("@StudentID", approval.StudentID);
                    cmd.Parameters.AddWithValue("@WorkshopID", approval.WorkshopID);
                    cmd.Parameters.AddWithValue("@MaterialID", (object)approval.MaterialID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", approval.Status);
                    cmd.Parameters.AddWithValue("@ApprovedBy", approval.ApprovedBy ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ApprovalDate", (object)approval.ApprovalDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Comments", approval.Comments ?? (object)DBNull.Value);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int Delete(int approvalId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Approvals WHERE ApprovalID=@ApprovalID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ApprovalID", approvalId);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
