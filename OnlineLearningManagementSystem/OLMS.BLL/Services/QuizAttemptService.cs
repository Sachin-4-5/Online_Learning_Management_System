using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System;
using System.Data;
using System.Collections.Generic;

namespace OLMS.BLL.Services
{
    public class QuizAttemptService
    {
        private readonly IQuizAttemptRepository _repo;

        public QuizAttemptService(IQuizAttemptRepository repo)
        {
            _repo = repo;
        }

        public List<QuizAttempt> GetByUserId(int userId)
        {
            DataTable dt = _repo.GetByQuiz(userId);
            List<QuizAttempt> attempts = new List<QuizAttempt>();
            foreach (DataRow row in dt.Rows)
            {
                attempts.Add(new QuizAttempt
                {
                    AttemptID = Convert.ToInt32(row["AttemptID"]),
                    QuizID = Convert.ToInt32(row["QuizID"]),
                    UserID = Convert.ToInt32(row["UserID"]),
                    Score = Convert.ToInt32(row["Score"]),
                    AttemptDate = Convert.ToDateTime(row["AttemptDate"])
                });
            }
            return attempts;
        }

        public void Add(QuizAttempt attempt)
        {
            int result = _repo.Add(attempt);
            if (result <= 0) throw new Exception("Failed to record quiz attempt.");
        }
    }
}
