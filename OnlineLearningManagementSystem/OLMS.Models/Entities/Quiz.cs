using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Models.Entities
{
    public class Quiz
    {
        public int QuizID { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int TotalMarks { get; set; }
    }
}
