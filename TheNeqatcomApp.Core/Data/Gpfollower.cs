using System;
using System.Collections.Generic;

#nullable disable

namespace TheNeqatcomApp.Core.Data
{
    public partial class Gpfollower
    {
        public decimal Followersid { get; set; }
        public decimal? Lenderid { get; set; }
        public decimal? Loaneeid { get; set; }

        public virtual Gplenderstore Lender { get; set; }
        public virtual Gploanee Loanee { get; set; }
    }
}
