using System;

namespace OLMS.Models.Entities
{
    public class Lesson
    {
        public int LessonID { get; set; }
        public int ModuleID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string VideoUrl { get; set; }
        public int SortOrder { get; set; }
    }
}
