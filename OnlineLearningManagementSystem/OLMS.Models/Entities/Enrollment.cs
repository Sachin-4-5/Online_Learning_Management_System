using System;

namespace OLMS.Models.Entities
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int UserID { get; set; }
        public DateTime EnrolledDate { get; set; }
        public string Status { get; set; }
    }
}
