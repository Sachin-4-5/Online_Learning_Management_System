using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface INotificationRepository
    {
        int Add(Notification notif);
        int MarkAsRead(int notificationId);
        DataTable GetByUser(int userId);
    }
}