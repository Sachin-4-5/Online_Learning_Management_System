using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;

namespace OLMS.DAL.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly string _connectionString;

        public ReportRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        }

        public DataTable GetQuizResultsByCourse(int courseId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT q.QuizID, q.Title AS QuizTitle, s.StudentID, s.Score, s.AttemptDate
                               FROM Quizzes q
                               INNER JOIN StudentQuizzes s ON q.QuizID = s.QuizID
                               WHERE q.CourseID = @CourseID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable GetStudentScores(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT s.StudentQuizID, s.QuizID, q.Title AS QuizTitle, s.Score, s.AttemptDate
                               FROM StudentQuizzes s
                               INNER JOIN Quizzes q ON s.QuizID = q.QuizID
                               WHERE s.StudentID = @StudentID";

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

        public DataTable GetMaterialsByWorkshop(int workshopId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT MaterialID, Title, FilePath, Description, UploadedOn
                               FROM Materials
                               WHERE WorkshopID = @WorkshopID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@WorkshopID", workshopId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable GetAllCourses()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT CourseID, Title FROM Courses";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetAllWorkshops()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT WorkshopID, Title FROM Workshops";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
