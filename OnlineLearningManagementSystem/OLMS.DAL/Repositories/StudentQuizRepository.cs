using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using System.Configuration;
using System;

namespace OLMS.DAL.Repositories
{
    public class StudentQuizRepository : IStudentQuizRepository
    {
        private readonly string _connectionString;

        public StudentQuizRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        }

        public DataTable GetByStudent(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM StudentQuizzes WHERE StudentID=@StudentID";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@StudentID", studentId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataRow GetById(int studentQuizId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM StudentQuizzes WHERE StudentQuizID=@ID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", studentQuizId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
        }

        public int Add(int studentId, int quizId, int score)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO StudentQuizzes (StudentID, QuizID, Score) VALUES (@StudentID, @QuizID, @Score)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.Parameters.AddWithValue("@QuizID", quizId);
                    cmd.Parameters.AddWithValue("@Score", score);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int UpdateScore(int studentQuizId, int score)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE StudentQuizzes SET Score=@Score WHERE StudentQuizID=@ID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", studentQuizId);
                    cmd.Parameters.AddWithValue("@Score", score);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int Delete(int studentQuizId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM StudentQuizzes WHERE StudentQuizID=@ID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", studentQuizId);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        public int AddAnswer(int studentQuizId, int questionId, string answerText, bool isCorrect)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"
            INSERT INTO StudentAnswers (StudentQuizID, QuestionID, AnswerText, IsCorrect) 
            VALUES (@StudentQuizID, @QuestionID, @AnswerText, @IsCorrect)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentQuizID", studentQuizId);
                    cmd.Parameters.AddWithValue("@QuestionID", questionId);
                    cmd.Parameters.AddWithValue("@AnswerText", answerText ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsCorrect", isCorrect);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
