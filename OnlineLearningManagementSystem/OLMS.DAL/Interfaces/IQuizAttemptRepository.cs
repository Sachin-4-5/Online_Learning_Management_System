using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface IQuizAttemptRepository
    {
        int Add(QuizAttempt attempt);
        DataTable GetByUser(int userId);
        DataTable GetByQuiz(int quizId);
    }
}