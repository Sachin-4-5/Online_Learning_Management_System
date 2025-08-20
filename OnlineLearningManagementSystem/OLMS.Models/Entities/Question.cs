using System;

namespace OLMS.Models.Entities
{
    public class Question
    {
        public int QuestionID { get; set; }
        public int QuizID { get; set; }
        public string QuestionText { get; set; }
        public int Marks { get; set; }
        public string CorrectAnswer { get; set; } // Optional
    }
}
