using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface ICourseRepository
    {
        int Add(Course course);
        int Update(Course course);
        int Delete(int courseId);
        DataTable GetAll();
        DataTable GetById(int courseId);
    }
}