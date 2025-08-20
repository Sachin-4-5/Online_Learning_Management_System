using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using System.Configuration;
using System;

namespace OLMS.DAL.Repositories
{
    public class StudentAnswerRepository : IStudentAnswerRepository
    {
        private readonly string _connectionString;

        public StudentAnswerRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        }

        public DataTable GetByStudentQuiz(int studentQuizId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM StudentAnswers WHERE StudentQuizID=@StudentQuizID";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@StudentQuizID", studentQuizId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int Add(int studentQuizId, int questionId, string answer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO StudentAnswers (StudentQuizID, QuestionID, Answer) VALUES (@StudentQuizID, @QuestionID, @Answer)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentQuizID", studentQuizId);
                    cmd.Parameters.AddWithValue("@QuestionID", questionId);
                    cmd.Parameters.AddWithValue("@Answer", answer ?? (object)DBNull.Value);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int Update(int studentAnswerId, string answer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE StudentAnswers SET Answer=@Answer WHERE StudentAnswerID=@ID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", studentAnswerId);
                    cmd.Parameters.AddWithValue("@Answer", answer ?? (object)DBNull.Value);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int Delete(int studentAnswerId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM StudentAnswers WHERE StudentAnswerID=@ID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", studentAnswerId);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
