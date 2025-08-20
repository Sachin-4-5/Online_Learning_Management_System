using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface IQuizRepository
    {
        int Add(Quiz quiz);
        int Update(Quiz quiz);
        int Delete(int quizId);
        DataTable GetByCourse(int courseId);
        DataTable GetById(int quizId);
        DataTable GetAll();
    }
}