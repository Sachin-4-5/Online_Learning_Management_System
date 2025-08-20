using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System;
using System.Data;
using System.Collections.Generic;
using OLMS.DAL.Repositories;

namespace OLMS.BLL.Services
{
    public class StudentAnswerService
    {
        private readonly IStudentAnswerRepository _repo;

        public StudentAnswerService()
        {
            _repo = new StudentAnswerRepository();
        }

        public List<StudentAnswer> GetByStudentQuiz(int studentQuizId)
        {
            DataTable dt = _repo.GetByStudentQuiz(studentQuizId);
            List<StudentAnswer> list = new List<StudentAnswer>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapRow(row));
            }
            return list;
        }

        public void Add(StudentAnswer answer)
        {
            if (_repo.Add(answer.StudentQuizID, answer.QuestionID, answer.AnswerText) <= 0)
                throw new Exception("Failed to add StudentAnswer.");
        }

        public void Update(StudentAnswer answer)
        {
            if (_repo.Update(answer.StudentAnswerID, answer.AnswerText) <= 0)
                throw new Exception("Failed to update StudentAnswer.");
        }

        public void Delete(int studentAnswerId)
        {
            if (_repo.Delete(studentAnswerId) <= 0)
                throw new Exception("Failed to delete StudentAnswer.");
        }

        private StudentAnswer MapRow(DataRow row)
        {
            return new StudentAnswer
            {
                StudentAnswerID = Convert.ToInt32(row["StudentAnswerID"]),
                StudentQuizID = Convert.ToInt32(row["StudentQuizID"]),
                QuestionID = Convert.ToInt32(row["QuestionID"]),
                AnswerText = row["Answer"].ToString()
            };
        }
    }
}
