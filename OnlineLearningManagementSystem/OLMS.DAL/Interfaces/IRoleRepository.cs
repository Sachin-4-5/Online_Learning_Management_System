using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface IRoleRepository
    {
        int Add(Role role);
        int Update(Role role);
        int Delete(int roleId);
        DataTable GetAll();
        DataTable GetById(int roleId);
    }
}