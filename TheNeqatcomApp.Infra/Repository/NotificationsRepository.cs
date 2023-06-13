using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TheNeqatcomApp.Core.Common;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.Repository;

namespace TheNeqatcomApp.Infra.Repository
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly IDBContext _dbContext;

        public NotificationsRepository(IDBContext dbContext)
        {
            this._dbContext = dbContext;
            
        }
        public void CreateNewNotification(Notification notification)
        {
            var p = new DynamicParameters();
            p.Add("Message", notification.Notificationsmessage, DbType.String, direction: ParameterDirection.Input);
            p.Add("useridd", notification.Userid, DbType.Int32, direction: ParameterDirection.Input);

            var result = _dbContext.Connection.Execute("INSERT INTO Notifications (NotificationsMessage, userID, NotificationsDate) VALUES (@Message, @useridd, GETDATE())", p);
        }
        public void DeleteNotificationsByUSerID(int id)
        {
            var p = new DynamicParameters();
            p.Add("uid", id, DbType.Int32, direction: ParameterDirection.Input);
            var query = "DELETE FROM notifications WHERE userid = @uid";
            var result = _dbContext.Connection.Execute(query, p);
        }

        public List<Notification> GetNotificationById(int id)
        {var p = new DynamicParameters();
            p.Add("id", id, DbType.Int32, ParameterDirection.Input);

            string query = @"SELECT * FROM Notifications 
                     WHERE userid = @id";

            

            IEnumerable<Notification> result = _dbContext.Connection.Query<Notification>(query, p);
            return result.ToList();
        }
    }
}
