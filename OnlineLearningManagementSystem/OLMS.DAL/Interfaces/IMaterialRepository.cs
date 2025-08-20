using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface IMaterialRepository
    {
        DataTable GetAll();
        DataTable GetByWorkshopId(int workshopId);
        DataRow GetById(int materialId);
        int Add(Material material);
        int Update(Material material);
        int Delete(int materialId);
    }
}