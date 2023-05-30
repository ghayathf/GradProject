using System;
using System.Collections.Generic;

#nullable disable

namespace TheNeqatcomApp.Core.Data
{
    public partial class Gpmeeting
    {
        public decimal Meetingid { get; set; }
        public DateTime? Startdate { get; set; }
        public string Meetingurl { get; set; }
        public decimal? Feedbackk { get; set; }
        public string Meetingtime { get; set; }
        public decimal? Loaneeid { get; set; }
        public decimal? Lenderid { get; set; }
        public decimal? Loanid { get; set; }

        public virtual Gplenderstore Lender { get; set; }
        public virtual Gploan Loan { get; set; }
        public virtual Gploanee Loanee { get; set; }
    }
}
