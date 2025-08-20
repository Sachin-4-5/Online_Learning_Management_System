using System;

namespace OLMS.Models.Entities
{
    public class Trainer
    {
        public int TrainerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Expertise { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}