using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System;
using System.Data;
using System.Collections.Generic;
using OLMS.DAL.Repositories;

namespace OLMS.BLL.Services
{
    public class CourseService
    {
        private readonly ICourseRepository _repo;

        public CourseService()
        {
            _repo = new CourseRepository();
        }

        public List<Course> GetAll()
        {
            DataTable dt = _repo.GetAll();
            List<Course> courses = new List<Course>();
            foreach (DataRow row in dt.Rows)
            {
                courses.Add(new Course
                {
                    CourseID = Convert.ToInt32(row["CourseID"]),
                    Title = row["Title"].ToString(),
                    Description = row["Description"].ToString(),
                    Category = row["Category"].ToString(),
                    CreatedBy = Convert.ToInt32(row["CreatedBy"]),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                    IsActive = Convert.ToBoolean(row["IsActive"])
                });
            }
            return courses;
        }

        public Course GetById(int courseId)
        {
            DataTable dt = _repo.GetById(courseId);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            return new Course
            {
                CourseID = Convert.ToInt32(row["CourseID"]),
                Title = row["Title"].ToString(),
                Description = row["Description"].ToString(),
                Category = row["Category"].ToString(),
                CreatedBy = Convert.ToInt32(row["CreatedBy"]),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                IsActive = Convert.ToBoolean(row["IsActive"])
            };
        }

        public void Add(Course course)
        {
            int result = _repo.Add(course);
            if (result <= 0) throw new Exception("Failed to add course.");
        }

        public void Update(Course course)
        {
            int result = _repo.Update(course);
            if (result <= 0) throw new Exception("Failed to update course.");
        }

        public void Delete(int courseId)
        {
            int result = _repo.Delete(courseId);
            if (result <= 0) throw new Exception("Failed to delete course.");
        }
    }
}
