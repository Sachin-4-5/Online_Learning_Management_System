using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface IStudentRepository
    {
        DataTable GetAll();
        DataRow GetById(int studentId);
        DataTable GetByWorkshopId(int workshopId);
        void Insert(Student student);
        void Update(Student student);
        void Delete(int studentId);
    }
}