using System;
using System.Collections.Generic;

#nullable disable

namespace TheNeqatcomApp.Core.Data
{
    public partial class Notification
    {
        public decimal Notificationsid { get; set; }
        public decimal? Userid { get; set; }
        public string Notificationsmessage { get; set; }
        public DateTime? Notificationsdate { get; set; }

        public virtual Gpuser User { get; set; }
    }
}
