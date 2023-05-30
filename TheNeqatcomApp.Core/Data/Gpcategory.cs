using System;
using System.Collections.Generic;

#nullable disable

namespace TheNeqatcomApp.Core.Data
{
    public partial class Gpcategory
    {
        public Gpcategory()
        {
            Gpoffers = new HashSet<Gpoffer>();
        }

        public decimal Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Categoryimage { get; set; }

        public virtual ICollection<Gpoffer> Gpoffers { get; set; }
    }
}
