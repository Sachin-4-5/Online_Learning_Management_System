using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        public int Add(Option option)
        {
            return DbHelper.ExecuteNonQuery("sp_Option_Add", new SqlParameter[]
            {
                new SqlParameter("@QuestionID", option.QuestionID),
                new SqlParameter("@OptionText", option.OptionText),
                new SqlParameter("@IsCorrect", option.IsCorrect)
            });
        }

        public int Update(Option option)
        {
            return DbHelper.ExecuteNonQuery("sp_Option_Update", new SqlParameter[]
            {
                new SqlParameter("@OptionID", option.OptionID),
                new SqlParameter("@OptionText", option.OptionText),
                new SqlParameter("@IsCorrect", option.IsCorrect)
            });
        }

        public int Delete(int optionId) => DbHelper.ExecuteNonQuery("sp_Option_Delete", new SqlParameter[] { new SqlParameter("@OptionID", optionId) });

        public DataTable GetByQuestion(int questionId) => DbHelper.ExecuteDataTable("sp_Option_GetByQuestion", new SqlParameter[] { new SqlParameter("@QuestionID", questionId) });
    }
}