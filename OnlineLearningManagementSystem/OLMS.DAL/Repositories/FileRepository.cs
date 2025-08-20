using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class FileRepository : IFileRepository
    {
        public int Add(File file)
        {
            var obj = DbHelper.ExecuteScalar("sp_File_Add", new SqlParameter[]
            {
                new SqlParameter("@LessonID", file.LessonID),
                new SqlParameter("@FileName", file.FileName),
                new SqlParameter("@FilePath", file.FilePath)
            });
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        public int Delete(int fileId) => DbHelper.ExecuteNonQuery("sp_File_Delete", new SqlParameter[] { new SqlParameter("@FileID", fileId) });

        public DataTable GetByLesson(int lessonId) => DbHelper.ExecuteDataTable("sp_File_GetByLesson", new SqlParameter[] { new SqlParameter("@LessonID", lessonId) });
    }
}