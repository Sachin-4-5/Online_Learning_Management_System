using System;

namespace OLMS.Models.Entities
{
    public class StudentQuiz
    {
        public int StudentQuizID { get; set; }
        public int StudentID { get; set; }
        public int QuizID { get; set; }
        public int Score { get; set; }
        public DateTime AttemptDate { get; set; }
    }
}