using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface IFileRepository
    {
        int Add(File file);
        int Delete(int fileId);
        DataTable GetByLesson(int lessonId);
    }
}