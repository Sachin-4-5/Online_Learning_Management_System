using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface IQuestionRepository
    {
        int Add(Question question);
        int Update(Question question);
        int Delete(int questionId);
        DataTable GetByQuiz(int quizId);
        DataTable GetById(int questionId);
    }
}