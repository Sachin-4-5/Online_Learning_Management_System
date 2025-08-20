using System.Data;

namespace OLMS.DAL.Interfaces
{
    public interface IReportRepository
    {
        DataTable GetQuizResultsByCourse(int courseId);
        DataTable GetStudentScores(int studentId);
        DataTable GetMaterialsByWorkshop(int workshopId);
        DataTable GetAllCourses();
        DataTable GetAllWorkshops();
    }
}