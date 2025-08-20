using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface IWorkshopRepository
    {
        DataTable GetAll();
        int Add(Workshop workshop);
        int Update(Workshop workshop);
        int Delete(int workshopId);
    }
}