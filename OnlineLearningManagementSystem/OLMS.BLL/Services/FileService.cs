using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System;
using System.Data;
using System.Collections.Generic;

namespace OLMS.BLL.Services
{
    public class FileService
    {
        private readonly IFileRepository _repo;

        public FileService(IFileRepository repo)
        {
            _repo = repo;
        }

        public List<File> GetByLessonId(int lessonId)
        {
            DataTable dt = _repo.GetByLesson(lessonId);
            List<File> files = new List<File>();
            foreach (DataRow row in dt.Rows)
            {
                files.Add(new File
                {
                    FileID = Convert.ToInt32(row["FileID"]),
                    LessonID = Convert.ToInt32(row["LessonID"]),
                    FileName = row["FileName"].ToString(),
                    FilePath = row["FilePath"].ToString(),
                    UploadedDate = Convert.ToDateTime(row["UploadedDate"])
                });
            }
            return files;
        }

        public void Add(File file)
        {
            int result = _repo.Add(file);
            if (result <= 0) throw new Exception("Failed to add file.");
        }

        public void Delete(int fileId)
        {
            int result = _repo.Delete(fileId);
            if (result <= 0) throw new Exception("Failed to delete file.");
        }
    }
}
