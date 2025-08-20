using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface ITrainerRepository
    {
        DataTable GetAll();
        DataTable GetById(int trainerId);
        int Add(Trainer trainer);
        int Update(Trainer trainer);
        int Delete(int trainerId);
    }
}