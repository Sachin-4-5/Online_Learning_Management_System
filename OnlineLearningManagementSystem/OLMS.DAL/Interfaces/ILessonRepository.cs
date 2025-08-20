using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface ILessonRepository
    {
        int Add(Lesson lesson);
        int Update(Lesson lesson);
        int Delete(int lessonId);
        DataTable GetAllByModule(int moduleId);
        DataTable GetById(int lessonId);
    }
}