using System;

namespace OLMS.Models.Entities
{
    public class Approval
    {
        public int ApprovalID { get; set; }
        public int StudentID { get; set; }
        public int WorkshopID { get; set; }
        public int? MaterialID { get; set; }  // Nullable if approval not related to a material
        public string Status { get; set; }    // Pending, Approved, Rejected
        public string ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        // Optional: Navigation properties if you plan to use them
        // public Student Student { get; set; }
        // public Workshop Workshop { get; set; }
        // public Material Material { get; set; }
    }
}