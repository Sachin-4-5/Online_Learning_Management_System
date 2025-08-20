using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface IOptionRepository
    {
        int Add(Option option);
        int Update(Option option);
        int Delete(int optionId);
        DataTable GetByQuestion(int questionId);
    }
}