using System;
using System.Collections.Generic;

#nullable disable

namespace TheNeqatcomApp.Core.Data
{
    public partial class Gpoffer
    {
        public Gpoffer()
        {
            Gploans = new HashSet<Gploan>();
        }

        public decimal Offerid { get; set; }
        public decimal? Totalmonths { get; set; }
        public string Descriptions { get; set; }
        public decimal? Lenderid { get; set; }
        public decimal? Categoryid { get; set; }
        public decimal? Minmonth { get; set; }

        public virtual Gpcategory Category { get; set; }
        public virtual Gplenderstore Lender { get; set; }
        public virtual ICollection<Gploan> Gploans { get; set; }
    }
}
