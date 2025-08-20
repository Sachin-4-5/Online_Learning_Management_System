using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System;
using System.Data;
using System.Collections.Generic;
using OLMS.DAL.Repositories;

namespace OLMS.BLL.Services
{
    public class QuestionService
    {
        private readonly IQuestionRepository _repo;

        public QuestionService()
        {
            _repo = new QuestionRepository();
        }

        public List<Question> GetByQuizId(int quizId)
        {
            DataTable dt = _repo.GetByQuiz(quizId);
            List<Question> questions = new List<Question>();
            foreach (DataRow row in dt.Rows)
            {
                questions.Add(new Question
                {
                    QuestionID = Convert.ToInt32(row["QuestionID"]),
                    QuizID = Convert.ToInt32(row["QuizID"]),
                    QuestionText = row["QuestionText"].ToString(),
                    Marks = Convert.ToInt32(row["Marks"])
                });
            }
            return questions;
        }

        public Question GetById(int questionId)
        {
            DataTable dt = _repo.GetById(questionId);
            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            return new Question
            {
                QuestionID = Convert.ToInt32(row["QuestionID"]),
                QuizID = Convert.ToInt32(row["QuizID"]),
                QuestionText = row["QuestionText"].ToString(),
                Marks = Convert.ToInt32(row["Marks"])
            };
        }

        public void Add(Question question)
        {
            int result = _repo.Add(question);
            if (result <= 0) throw new Exception("Failed to add question.");
        }

        public void Update(Question question)
        {
            int result = _repo.Update(question);
            if (result <= 0) throw new Exception("Failed to update question.");
        }

        public void Delete(int questionId)
        {
            int result = _repo.Delete(questionId);
            if (result <= 0) throw new Exception("Failed to delete question.");
        }
    }
}
