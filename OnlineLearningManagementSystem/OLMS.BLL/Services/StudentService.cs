using OLMS.DAL.Repositories;
using OLMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;

namespace OLMS.BLL.Services
{
    public class StudentService
    {
        private readonly StudentRepository _repo;

        public StudentService()
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            _repo = new StudentRepository(cs);
        }

        public List<Student> GetAll()
        {
            DataTable dt = _repo.GetAll();
            List<Student> students = new List<Student>();
            foreach (DataRow row in dt.Rows)
            {
                students.Add(MapRow(row));
            }
            return students;
        }

        public Student GetById(int studentId)
        {
            DataRow row = _repo.GetById(studentId);
            if (row == null) return null;

            return MapRow(row);
        }

        public List<Student> GetByWorkshopId(int workshopId)
        {
            DataTable dt = _repo.GetByWorkshopId(workshopId);
            List<Student> students = new List<Student>();
            foreach (DataRow row in dt.Rows)
            {
                students.Add(MapRow(row));
            }
            return students;
        }

        public void Add(Student student)
        {
            _repo.Insert(student);
        }

        public void Update(Student student)
        {
            _repo.Update(student);
        }

        public void Delete(int studentId)
        {
            _repo.Delete(studentId);
        }

        private Student MapRow(DataRow row)
        {
            return new Student
            {
                StudentID = Convert.ToInt32(row["StudentID"]),
                WorkshopID = Convert.ToInt32(row["WorkshopID"]),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                Email = row["Email"].ToString(),
                Phone = row["Phone"] != DBNull.Value ? row["Phone"].ToString() : null
            };
        }
    }
}
