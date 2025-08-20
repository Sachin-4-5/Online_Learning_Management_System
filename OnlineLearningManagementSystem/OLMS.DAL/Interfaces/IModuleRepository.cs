using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface IModuleRepository
    {
        int Add(Module module);
        int Update(Module module);
        int Delete(int moduleId);
        DataTable GetAllByCourse(int courseId);
        DataTable GetById(int moduleId);
        DataTable GetAll();
    }
}