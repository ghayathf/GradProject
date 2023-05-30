using System;
using System.Collections.Generic;

#nullable disable

namespace TheNeqatcomApp.Core.Data
{
    public partial class Gpuser
    {
        public Gpuser()
        {
            Gplenderstores = new HashSet<Gplenderstore>();
            Gploanees = new HashSet<Gploanee>();
            Gptestimonials = new HashSet<Gptestimonial>();
            Notifications = new HashSet<Notification>();
        }

        public decimal Userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phonenum { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Userimage { get; set; }

        public virtual ICollection<Gplenderstore> Gplenderstores { get; set; }
        public virtual ICollection<Gploanee> Gploanees { get; set; }
        public virtual ICollection<Gptestimonial> Gptestimonials { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
