using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface IEnrollmentRepository
    {
        int Add(Enrollment enrollment);
        int UpdateStatus(int enrollmentId, string status);
        int Delete(int enrollmentId);
        DataTable GetByUser(int userId);
        DataTable GetByCourse(int courseId);
    }
}