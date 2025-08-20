using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System;
using System.Data;
using System.Collections.Generic;
using OLMS.DAL.Repositories;

namespace OLMS.BLL.Services
{
    public class QuizService
    {
        private readonly IQuizRepository _repo;

        public QuizService()
        {
            _repo = new QuizRepository();
        }

        public List<Quiz> GetByCourseId(int courseId)
        {
            DataTable dt = _repo.GetByCourse(courseId);
            List<Quiz> quizzes = new List<Quiz>();
            foreach (DataRow row in dt.Rows)
            {
                quizzes.Add(new Quiz
                {
                    QuizID = Convert.ToInt32(row["QuizID"]),
                    CourseID = Convert.ToInt32(row["CourseID"]),
                    Title = row["Title"].ToString(),
                    TotalMarks = Convert.ToInt32(row["TotalMarks"])
                });
            }
            return quizzes;
        }

        public List<Quiz> GetAll()
        {
            DataTable dt = _repo.GetAll();
            List<Quiz> quizzes = new List<Quiz>();
            foreach (DataRow row in dt.Rows)
            {
                quizzes.Add(new Quiz
                {
                    QuizID = Convert.ToInt32(row["QuizID"]),
                    CourseID = Convert.ToInt32(row["CourseID"]),
                    Title = row["Title"].ToString(),
                    TotalMarks = Convert.ToInt32(row["TotalMarks"])
                });
            }
            return quizzes;
        }

        public void Add(Quiz quiz)
        {
            int result = _repo.Add(quiz);
            if (result <= 0) throw new Exception("Failed to add quiz.");
        }

        public void Update(Quiz quiz)
        {
            int result = _repo.Update(quiz);
            if (result <= 0) throw new Exception("Failed to update quiz.");
        }

        public void Delete(int quizId)
        {
            int result = _repo.Delete(quizId);
            if (result <= 0) throw new Exception("Failed to delete quiz.");
        }
    }
}
