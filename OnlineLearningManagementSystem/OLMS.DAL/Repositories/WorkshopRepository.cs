using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class WorkshopRepository : IWorkshopRepository
    {
        private readonly string _connectionString;

        public WorkshopRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Workshops", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int Add(Workshop workshop)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Workshops (Title, Description, TrainerID, StartDate, EndDate, Location, IsActive)
                  VALUES (@Title, @Description, @TrainerID, @StartDate, @EndDate, @Location, @IsActive)", conn))
            {
                cmd.Parameters.AddWithValue("@Title", workshop.Title);
                cmd.Parameters.AddWithValue("@Description", workshop.Description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TrainerID", workshop.TrainerID);
                cmd.Parameters.AddWithValue("@StartDate", workshop.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", workshop.EndDate);
                cmd.Parameters.AddWithValue("@Location", workshop.Location ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", workshop.IsActive);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int Update(Workshop workshop)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE Workshops
                  SET Title=@Title, Description=@Description, TrainerID=@TrainerID, 
                      StartDate=@StartDate, EndDate=@EndDate, Location=@Location, IsActive=@IsActive
                  WHERE WorkshopID=@WorkshopID", conn))
            {
                cmd.Parameters.AddWithValue("@WorkshopID", workshop.WorkshopID);
                cmd.Parameters.AddWithValue("@Title", workshop.Title);
                cmd.Parameters.AddWithValue("@Description", workshop.Description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TrainerID", workshop.TrainerID);
                cmd.Parameters.AddWithValue("@StartDate", workshop.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", workshop.EndDate);
                cmd.Parameters.AddWithValue("@Location", workshop.Location ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", workshop.IsActive);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int Delete(int workshopId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Workshops WHERE WorkshopID=@WorkshopID", conn))
            {
                cmd.Parameters.AddWithValue("@WorkshopID", workshopId);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
