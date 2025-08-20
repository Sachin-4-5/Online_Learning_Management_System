using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System;
using System.Data;
using System.Collections.Generic;

namespace OLMS.BLL.Services
{
    public class NotificationService
    {
        private readonly INotificationRepository _repo;

        public NotificationService(INotificationRepository repo)
        {
            _repo = repo;
        }

        public List<Notification> GetByUserId(int userId)
        {
            DataTable dt = _repo.GetByUser(userId);
            List<Notification> notifications = new List<Notification>();
            foreach (DataRow row in dt.Rows)
            {
                notifications.Add(new Notification
                {
                    NotificationID = Convert.ToInt32(row["NotificationID"]),
                    UserID = Convert.ToInt32(row["UserID"]),
                    Message = row["Message"].ToString(),
                    IsRead = Convert.ToBoolean(row["IsRead"]),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                });
            }
            return notifications;
        }

        public void Add(Notification notification)
        {
            int result = _repo.Add(notification);
            if (result <= 0) throw new Exception("Failed to add notification.");
        }

        public void MarkAsRead(int notificationId)
        {
            int result = _repo.MarkAsRead(notificationId);
            if (result <= 0) throw new Exception("Failed to mark notification as read.");
        }
    }
}
