using System;
using System.Collections.Generic;

#nullable disable

namespace TheNeqatcomApp.Core.Data
{
    public partial class Gploan
    {
        public Gploan()
        {
            Gpmeetings = new HashSet<Gpmeeting>();
            Gppurchasings = new HashSet<Gppurchasing>();
        }

        public decimal Loanid { get; set; }
        public decimal? Totalmonths { get; set; }
        public decimal? Totalprice { get; set; }
        public decimal? Estimatedprice { get; set; }
        public decimal? Monthlyamount { get; set; }
        public decimal? Predayscounter { get; set; }
        public decimal? Latedayscounter { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public decimal? Offerid { get; set; }
        public decimal? Loaneeid { get; set; }
        public decimal? Loanstatus { get; set; }
        public decimal? Postponestatus { get; set; }
        public DateTime? Postponedate { get; set; }
        public decimal? Beforepaystatus { get; set; }
        public decimal? Inpaydatestatus { get; set; }
        public decimal? Latepaystatus { get; set; }

        public virtual Gploanee Loanee { get; set; }
        public virtual Gpoffer Offer { get; set; }
        public virtual ICollection<Gpmeeting> Gpmeetings { get; set; }
        public virtual ICollection<Gppurchasing> Gppurchasings { get; set; }
    }
}
