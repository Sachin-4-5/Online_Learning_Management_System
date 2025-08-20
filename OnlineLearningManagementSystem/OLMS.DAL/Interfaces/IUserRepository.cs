using OLMS.Models.Entities;
using System.Collections.Generic;

namespace OLMS.DAL.Interfaces
{
    public interface IUserRepository
    {
        int Add(User user);
        int Update(User user);
        int Delete(int userId);
        List<User> GetAll();
        User GetById(int userId);
        User GetByEmail(string email);
    }
}