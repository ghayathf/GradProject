using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.Service;

namespace TheNeqatcomApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationsService _notificationsService;
        public NotificationController(INotificationsService notificationsService)
        {

            this._notificationsService = notificationsService;
        }
        [HttpGet]
        [Route("GetNotificationById/{id}")]
        public List<Notification> GetNotificationById(int id)
        {
            return _notificationsService.GetNotificationById(id);
        }
        [HttpPost]
        [Route("CreateNewNotification")]
        public void CreateNewNotification(Notification notification)
        {
            _notificationsService.CreateNewNotification(notification);
        }
        [HttpDelete]
        [Route("DeleteNotificationsByUSerID/{id}")]
        public void DeleteNotificationsByUSerID(int id)
        {
            _notificationsService.DeleteNotificationsByUSerID(id);
        }
    }
}
