using OLMS.DAL.Interfaces;
using OLMS.DAL.Repositories;
using System.Data;

namespace OLMS.BLL.Services
{
    public class ReportService
    {
        private readonly IReportRepository _repo;

        public ReportService()
        {
            _repo = new ReportRepository();
        }

        public DataTable GetQuizResultsByCourse(int courseId) => _repo.GetQuizResultsByCourse(courseId);
        public DataTable GetStudentScores(int studentId) => _repo.GetStudentScores(studentId);
        public DataTable GetMaterialsByWorkshop(int workshopId) => _repo.GetMaterialsByWorkshop(workshopId);
        public DataTable GetAllCourses() => _repo.GetAllCourses();
        public DataTable GetAllWorkshops() => _repo.GetAllWorkshops();
    }
}