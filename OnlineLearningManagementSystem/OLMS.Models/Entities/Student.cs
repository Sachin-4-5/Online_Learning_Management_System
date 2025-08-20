using System;

namespace OLMS.Models.Entities
{
    public class Student
    {
        public int StudentID { get; set; }
        public int WorkshopID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}