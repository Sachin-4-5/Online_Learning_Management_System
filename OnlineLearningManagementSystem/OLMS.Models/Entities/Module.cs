using System;

namespace OLMS.Models.Entities
{
    public class Module
    {
        public int ModuleID { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
    }
}
