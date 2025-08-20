using System.Data;

namespace OLMS.DAL.Interfaces
{
    public interface IStudentQuizRepository
    {
        DataTable GetByStudent(int studentId);
        DataRow GetById(int studentQuizId);
        int Add(int studentId, int quizId, int score);
        int UpdateScore(int studentQuizId, int score);
        int Delete(int studentQuizId);

        // New method to save student answers
        int AddAnswer(int studentQuizId, int questionId, string answerText, bool isCorrect);
    }
}