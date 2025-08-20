using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Models.Entities
{
    public class StudentAnswer
    {
        public int StudentAnswerID { get; set; }
        public int StudentQuizID { get; set; }
        public int QuestionID { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
