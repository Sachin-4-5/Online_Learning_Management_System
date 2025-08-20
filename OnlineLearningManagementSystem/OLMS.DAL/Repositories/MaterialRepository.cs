using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly string _connectionString;

        public MaterialRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Get all materials with related workshop info
        /// </summary>
        public DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT m.MaterialID, m.WorkshopID, w.Title AS WorkshopTitle,
                           m.Title, m.FilePath, m.Description, m.IsActive, m.UploadDate
                    FROM Materials m
                    INNER JOIN Workshops w ON m.WorkshopID = w.WorkshopID";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// Get a material by MaterialID
        /// </summary>
        public DataRow GetById(int materialId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT m.MaterialID, m.WorkshopID, w.Title AS WorkshopTitle, 
                           m.Title, m.FilePath, m.Description, m.IsActive, m.UploadDate
                    FROM Materials m
                    INNER JOIN Workshops w ON m.WorkshopID = w.WorkshopID
                    WHERE m.MaterialID = @MaterialID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@MaterialID", SqlDbType.Int).Value = materialId;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
        }

        /// <summary>
        /// Get all materials for a specific workshop
        /// </summary>
        public DataTable GetByWorkshopId(int workshopId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT m.MaterialID, m.WorkshopID, w.Title AS WorkshopTitle, 
                           m.Title, m.FilePath, m.Description, m.IsActive, m.UploadDate
                    FROM Materials m
                    INNER JOIN Workshops w ON m.WorkshopID = w.WorkshopID
                    WHERE m.WorkshopID = @WorkshopID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@WorkshopID", SqlDbType.Int).Value = workshopId;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// Add new material
        /// </summary>
        public int Add(Material material)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Materials (WorkshopID, Title, FilePath, Description, IsActive, UploadDate)
                    VALUES (@WorkshopID, @Title, @FilePath, @Description, @IsActive, @UploadDate)", conn);

                cmd.Parameters.Add("@WorkshopID", SqlDbType.Int).Value = material.WorkshopID;
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 200).Value = material.Title ?? (object)DBNull.Value;
                cmd.Parameters.Add("@FilePath", SqlDbType.NVarChar, 500).Value = (object)material.FilePath ?? DBNull.Value;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, -1).Value = (object)material.Description ?? DBNull.Value;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = material.IsActive;
                //cmd.Parameters.Add("@UploadDate", SqlDbType.DateTime).Value = material.UploadDate == DateTime.MinValue ? DateTime.Now : material.UploadDate;
                // FIX: handle both null and MinValue
                cmd.Parameters.Add("@UploadDate", SqlDbType.DateTime).Value = (!material.UploadDate.HasValue || material.UploadDate.Value == DateTime.MinValue) ? (object)DBNull.Value: material.UploadDate.Value;

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Update material by ID
        /// </summary>
        public int Update(Material material)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"
                    UPDATE Materials
                    SET WorkshopID=@WorkshopID, Title=@Title, FilePath=@FilePath, 
                        Description=@Description, IsActive=@IsActive, UploadDate=@UploadDate
                    WHERE MaterialID=@MaterialID", conn);

                cmd.Parameters.Add("@MaterialID", SqlDbType.Int).Value = material.MaterialID;
                cmd.Parameters.Add("@WorkshopID", SqlDbType.Int).Value = material.WorkshopID;
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 200).Value = material.Title ?? (object)DBNull.Value;
                cmd.Parameters.Add("@FilePath", SqlDbType.NVarChar, 500).Value = (object)material.FilePath ?? DBNull.Value;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, -1).Value = (object)material.Description ?? DBNull.Value;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = material.IsActive;
                cmd.Parameters.Add("@UploadDate", SqlDbType.DateTime).Value = (!material.UploadDate.HasValue || material.UploadDate.Value == DateTime.MinValue) ? (object)DBNull.Value : material.UploadDate.Value;

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Delete material by ID
        /// </summary>
        public int Delete(int materialId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Materials WHERE MaterialID=@MaterialID", conn);
                cmd.Parameters.Add("@MaterialID", SqlDbType.Int).Value = materialId;

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}