using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System;
using System.Data;
using System.Collections.Generic;

namespace OLMS.BLL.Services
{
    public class LessonService
    {
        private readonly ILessonRepository _repo;

        public LessonService(ILessonRepository repo)
        {
            _repo = repo;
        }

        public List<Lesson> GetByModuleId(int moduleId)
        {
            DataTable dt = _repo.GetAllByModule(moduleId);
            List<Lesson> lessons = new List<Lesson>();
            foreach (DataRow row in dt.Rows)
            {
                lessons.Add(new Lesson
                {
                    LessonID = Convert.ToInt32(row["LessonID"]),
                    ModuleID = Convert.ToInt32(row["ModuleID"]),
                    Title = row["Title"].ToString(),
                    Content = row["Content"].ToString(),
                    VideoUrl = row["VideoUrl"].ToString(),
                    SortOrder = Convert.ToInt32(row["SortOrder"])
                });
            }
            return lessons;
        }

        public void Add(Lesson lesson)
        {
            int result = _repo.Add(lesson);
            if (result <= 0) throw new Exception("Failed to add lesson.");
        }

        public void Update(Lesson lesson)
        {
            int result = _repo.Update(lesson);
            if (result <= 0) throw new Exception("Failed to update lesson.");
        }

        public void Delete(int lessonId)
        {
            int result = _repo.Delete(lessonId);
            if (result <= 0) throw new Exception("Failed to delete lesson.");
        }
    }
}