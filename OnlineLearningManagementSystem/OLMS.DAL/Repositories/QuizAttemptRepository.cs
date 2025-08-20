using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class QuizAttemptRepository : IQuizAttemptRepository
    {
        public int Add(QuizAttempt attempt)
        {
            var obj = DbHelper.ExecuteScalar("sp_QuizAttempt_Add", new SqlParameter[]
            {
                new SqlParameter("@QuizID", attempt.QuizID),
                new SqlParameter("@UserID", attempt.UserID),
                new SqlParameter("@Score", attempt.Score)
            });
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        public DataTable GetByUser(int userId) => DbHelper.ExecuteDataTable("sp_QuizAttempt_GetByUser", new SqlParameter[] { new SqlParameter("@UserID", userId) });

        public DataTable GetByQuiz(int quizId) => DbHelper.ExecuteDataTable("sp_QuizAttempt_GetByQuiz", new SqlParameter[] { new SqlParameter("@QuizID", quizId) });
    }
}