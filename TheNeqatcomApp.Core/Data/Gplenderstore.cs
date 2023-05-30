using System;
using System.Collections.Generic;

#nullable disable

namespace TheNeqatcomApp.Core.Data
{
    public partial class Gplenderstore
    {
        public Gplenderstore()
        {
            Gpcomplaints = new HashSet<Gpcomplaint>();
            Gpfollowers = new HashSet<Gpfollower>();
            Gpmeetings = new HashSet<Gpmeeting>();
            Gpoffers = new HashSet<Gpoffer>();
        }

        public decimal Lenderid { get; set; }
        public string Commercialregister { get; set; }
        public decimal? Lenderuserid { get; set; }
        public decimal? Registerstatus { get; set; }
        public decimal? Shadowstatus { get; set; }
        public string Companysize { get; set; }
        public string Siteurl { get; set; }
        public decimal? Warncounter { get; set; }
        public DateTime? Warndate { get; set; }
        public string Bankaccount { get; set; }

        public virtual Gpuser Lenderuser { get; set; }
        public virtual ICollection<Gpcomplaint> Gpcomplaints { get; set; }
        public virtual ICollection<Gpfollower> Gpfollowers { get; set; }
        public virtual ICollection<Gpmeeting> Gpmeetings { get; set; }
        public virtual ICollection<Gpoffer> Gpoffers { get; set; }
    }
}
