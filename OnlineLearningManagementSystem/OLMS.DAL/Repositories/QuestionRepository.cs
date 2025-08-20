using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        public int Add(Question q)
        {
            var obj = DbHelper.ExecuteScalar("sp_Question_Add", new SqlParameter[]
            {
                new SqlParameter("@QuizID", q.QuizID),
                new SqlParameter("@QuestionText", q.QuestionText),
                new SqlParameter("@Marks", q.Marks)
            });
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        public int Update(Question q)
        {
            return DbHelper.ExecuteNonQuery("sp_Question_Update", new SqlParameter[]
            {
                new SqlParameter("@QuestionID", q.QuestionID),
                new SqlParameter("@QuestionText", q.QuestionText),
                new SqlParameter("@Marks", q.Marks)
            });
        }

        public int Delete(int questionId) => DbHelper.ExecuteNonQuery("sp_Question_Delete", new SqlParameter[] { new SqlParameter("@QuestionID", questionId) });

        public DataTable GetByQuiz(int quizId) => DbHelper.ExecuteDataTable("sp_Question_GetByQuiz", new SqlParameter[] { new SqlParameter("@QuizID", quizId) });

        public DataTable GetById(int questionId) => DbHelper.ExecuteDataTable("sp_Question_GetById", new SqlParameter[] { new SqlParameter("@QuestionID", questionId) });
    }
}
