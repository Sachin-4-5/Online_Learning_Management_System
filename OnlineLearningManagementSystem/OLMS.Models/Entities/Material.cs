using System;

namespace OLMS.Models.Entities
{
    public class Material
    {
        public int MaterialID { get; set; }
        public int WorkshopID { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public DateTime? UploadDate { get; set; }
        public bool IsActive { get; set; }
        public string WorkshopTitle { get; set; } // Added for Join
    }
}