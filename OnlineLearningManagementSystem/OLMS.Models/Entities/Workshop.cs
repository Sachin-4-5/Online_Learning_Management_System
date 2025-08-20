using System;

namespace OLMS.Models.Entities
{
    public class Workshop
    {
        public int WorkshopID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TrainerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
    }
}