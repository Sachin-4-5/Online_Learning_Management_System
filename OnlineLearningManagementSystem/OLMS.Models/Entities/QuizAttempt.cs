using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Models.Entities
{
    public class QuizAttempt
    {
        public int AttemptID { get; set; }
        public int QuizID { get; set; }
        public int UserID { get; set; }
        public int Score { get; set; }
        public DateTime AttemptDate { get; set; }
    }
}
