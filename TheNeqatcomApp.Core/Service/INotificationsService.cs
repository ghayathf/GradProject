using TheNeqatcomApp.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheNeqatcomApp.Core.Service
{
    public interface INotificationsService
    {
        List<Notification> GetNotificationById(int id);
    
        void CreateNewNotification(Notification notification);
    }
}
