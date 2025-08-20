using System.Data;

namespace OLMS.DAL.Interfaces
{
    public interface IStudentAnswerRepository
    {
        DataTable GetByStudentQuiz(int studentQuizId);
        int Add(int studentQuizId, int questionId, string answer);
        int Update(int studentAnswerId, string answer);
        int Delete(int studentAnswerId);
    }
}