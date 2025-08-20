using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        public int Add(Quiz quiz)
        {
            var obj = DbHelper.ExecuteScalar("sp_Quiz_Add", new SqlParameter[]
            {
                new SqlParameter("@CourseID", quiz.CourseID),
                new SqlParameter("@Title", quiz.Title),
                new SqlParameter("@TotalMarks", quiz.TotalMarks)
            });
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        public int Update(Quiz quiz)
        {
            return DbHelper.ExecuteNonQuery("sp_Quiz_Update", new SqlParameter[]
            {
                new SqlParameter("@QuizID", quiz.QuizID),
                new SqlParameter("@Title", quiz.Title),
                new SqlParameter("@TotalMarks", quiz.TotalMarks)
            });
        }

        public int Delete(int quizId) => DbHelper.ExecuteNonQuery("sp_Quiz_Delete", new SqlParameter[] { new SqlParameter("@QuizID", quizId) });

        public DataTable GetByCourse(int courseId) => DbHelper.ExecuteDataTable("sp_Quiz_GetByCourse", new SqlParameter[] { new SqlParameter("@CourseID", courseId) });

        public DataTable GetById(int quizId) => DbHelper.ExecuteDataTable("sp_Quiz_GetById", new SqlParameter[] { new SqlParameter("@QuizID", quizId) });

        public DataTable GetAll()
        {
            return DbHelper.ExecuteDataTable("sp_Quiz_GetAll", null);
        }
    }
}
