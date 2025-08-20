using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly string _connectionString;

        public TrainerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable GetAll()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Trainers", con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetById(int trainerId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Trainers WHERE TrainerID = @TrainerID", con))
            {
                cmd.Parameters.AddWithValue("@TrainerID", trainerId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public int Add(Trainer trainer)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(@"INSERT INTO Trainers (Name, Email, Phone, Expertise) 
                                                     VALUES (@Name, @Email, @Phone, @Expertise)", con))
            {
                cmd.Parameters.AddWithValue("@Name", trainer.Name);
                cmd.Parameters.AddWithValue("@Email", trainer.Email);
                cmd.Parameters.AddWithValue("@Phone", trainer.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Expertise", trainer.Expertise ?? (object)DBNull.Value);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int Update(Trainer trainer)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(@"UPDATE Trainers 
                                                     SET Name = @Name, Email = @Email, Phone = @Phone, Expertise = @Expertise 
                                                     WHERE TrainerID = @TrainerID", con))
            {
                cmd.Parameters.AddWithValue("@TrainerID", trainer.TrainerID);
                cmd.Parameters.AddWithValue("@Name", trainer.Name);
                cmd.Parameters.AddWithValue("@Email", trainer.Email);
                cmd.Parameters.AddWithValue("@Phone", trainer.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Expertise", trainer.Expertise ?? (object)DBNull.Value);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int Delete(int trainerId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Trainers WHERE TrainerID = @TrainerID", con))
            {
                cmd.Parameters.AddWithValue("@TrainerID", trainerId);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
