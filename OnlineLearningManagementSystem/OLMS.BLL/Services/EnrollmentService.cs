using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System;
using System.Data;
using System.Collections.Generic;
using OLMS.DAL.Repositories;

namespace OLMS.BLL.Services
{
    public class EnrollmentService
    {
        private readonly IEnrollmentRepository _repo;

        public EnrollmentService()
        {
            _repo = new EnrollmentRepository();
        }

        public List<Enrollment> GetByUserId(int userId)
        {
            DataTable dt = _repo.GetByUser(userId);
            List<Enrollment> enrollments = new List<Enrollment>();
            foreach (DataRow row in dt.Rows)
            {
                enrollments.Add(new Enrollment
                {
                    EnrollmentID = Convert.ToInt32(row["EnrollmentID"]),
                    CourseID = Convert.ToInt32(row["CourseID"]),
                    UserID = Convert.ToInt32(row["UserID"]),
                    EnrolledDate = Convert.ToDateTime(row["EnrolledDate"]),
                    Status = row["Status"].ToString()
                });
            }
            return enrollments;
        }

        public void Enroll(Enrollment enrollment)
        {
            int result = _repo.Add(enrollment);
            if (result <= 0) throw new Exception("Failed to enroll user.");
        }

        public void UpdateStatus(int enrollmentId, string status)
        {
            int result = _repo.UpdateStatus(enrollmentId, status);
            if (result <= 0) throw new Exception("Failed to update enrollment status.");
        }

        public void Delete(int enrollmentId)
        {
            int result = _repo.Delete(enrollmentId);
            if (result <= 0) throw new Exception("Failed to delete enrollment.");
        }
    }
}
