using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        public int Add(Lesson lesson)
        {
            var obj = DbHelper.ExecuteScalar("sp_Lesson_Add", new SqlParameter[]
            {
                new SqlParameter("@ModuleID", lesson.ModuleID),
                new SqlParameter("@Title", lesson.Title),
                new SqlParameter("@Content", lesson.Content ?? (object)DBNull.Value),
                new SqlParameter("@VideoUrl", lesson.VideoUrl ?? (object)DBNull.Value),
                new SqlParameter("@SortOrder", lesson.SortOrder)
            });
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        public int Update(Lesson lesson)
        {
            return DbHelper.ExecuteNonQuery("sp_Lesson_Update", new SqlParameter[]
            {
                new SqlParameter("@LessonID", lesson.LessonID),
                new SqlParameter("@Title", lesson.Title),
                new SqlParameter("@Content", lesson.Content ?? (object)DBNull.Value),
                new SqlParameter("@VideoUrl", lesson.VideoUrl ?? (object)DBNull.Value),
                new SqlParameter("@SortOrder", lesson.SortOrder)
            });
        }

        public int Delete(int lessonId) => DbHelper.ExecuteNonQuery("sp_Lesson_Delete", new SqlParameter[] { new SqlParameter("@LessonID", lessonId) });

        public DataTable GetAllByModule(int moduleId) => DbHelper.ExecuteDataTable("sp_Lesson_GetByModule", new SqlParameter[] { new SqlParameter("@ModuleID", moduleId) });

        public DataTable GetById(int lessonId) => DbHelper.ExecuteDataTable("sp_Lesson_GetById", new SqlParameter[] { new SqlParameter("@LessonID", lessonId) });
    }
}
