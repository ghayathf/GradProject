using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;
namespace TheNeqatcomApp.Core.Repository
{
    public interface INotificationsRepository
    {
       List<Notification> GetNotificationById(int id);
        void CreateNewNotification(Notification notification);
    }
}
