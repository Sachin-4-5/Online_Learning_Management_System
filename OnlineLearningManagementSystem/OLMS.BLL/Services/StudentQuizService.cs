using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System;
using System.Data;
using System.Collections.Generic;
using OLMS.DAL.Repositories;

namespace OLMS.BLL.Services
{
    public class StudentQuizService
    {
        private readonly IStudentQuizRepository _repo;

        public StudentQuizService()
        {
            _repo = new StudentQuizRepository();
        }

        public List<StudentQuiz> GetByStudent(int studentId)
        {
            DataTable dt = _repo.GetByStudent(studentId);
            List<StudentQuiz> list = new List<StudentQuiz>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapRow(row));
            }
            return list;
        }

        public StudentQuiz GetById(int id)
        {
            DataRow row = _repo.GetById(id);
            return row != null ? MapRow(row) : null;
        }

        public int Add(StudentQuiz quiz)
        {
            int id = _repo.Add(quiz.StudentID, quiz.QuizID, quiz.Score); // repo returns new ID
            if (id <= 0)
                throw new Exception("Failed to add StudentQuiz.");
            return id; // return inserted ID
        }

        public void UpdateScore(int studentQuizId, int score)
        {
            if (_repo.UpdateScore(studentQuizId, score) <= 0)
                throw new Exception("Failed to update score.");
        }

        public void Delete(int studentQuizId)
        {
            if (_repo.Delete(studentQuizId) <= 0)
                throw new Exception("Failed to delete StudentQuiz.");
        }

        private StudentQuiz MapRow(DataRow row)
        {
            return new StudentQuiz
            {
                StudentQuizID = Convert.ToInt32(row["StudentQuizID"]),
                StudentID = Convert.ToInt32(row["StudentID"]),
                QuizID = Convert.ToInt32(row["QuizID"]),
                Score = Convert.ToInt32(row["Score"]),
                AttemptDate = Convert.ToDateTime(row["AttemptDate"])
            };
        }


        // Add this inside StudentQuizService class
        public void AddAnswer(int studentQuizId, int questionId, string answerText, bool isCorrect)
        {
            if (_repo.AddAnswer(studentQuizId, questionId, answerText, isCorrect) <= 0)
                throw new Exception("Failed to add StudentAnswer.");
        }
    }
}
