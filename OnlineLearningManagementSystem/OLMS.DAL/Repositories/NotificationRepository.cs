using System;
using System.Data;
using System.Data.SqlClient;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;

namespace OLMS.DAL.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public int Add(Notification notif)
        {
            return DbHelper.ExecuteNonQuery("sp_Notification_Add", new SqlParameter[]
            {
                new SqlParameter("@UserID", notif.UserID),
                new SqlParameter("@Message", notif.Message)
            });
        }

        public int MarkAsRead(int notificationId)
        {
            return DbHelper.ExecuteNonQuery("sp_Notification_MarkAsRead", new SqlParameter[] { new SqlParameter("@NotificationID", notificationId) });
        }

        public DataTable GetByUser(int userId) => DbHelper.ExecuteDataTable("sp_Notification_GetByUser", new SqlParameter[] { new SqlParameter("@UserID", userId) });
    }
}