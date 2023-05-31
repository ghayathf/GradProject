using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.Repository;
using TheNeqatcomApp.Core.Service;

namespace TheNeqatcomApp.Infra.Service
{
    public class NotificationsService : INotificationsService
    {

        private readonly INotificationsRepository _notificationsRepository;
        public NotificationsService(INotificationsRepository notificationsRepository)
        {
            this._notificationsRepository = notificationsRepository;
        }
        public void CreateNewNotification(Notification notification)
        {
            _notificationsRepository.CreateNewNotification(notification);
        }

        public List<Notification> GetNotificationById(int id)
        {
            return _notificationsRepository.GetNotificationById(id);
        }
    }
}
