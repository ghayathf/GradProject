using System;
using System.Collections.Generic;

#nullable disable

namespace TheNeqatcomApp.Core.Data
{
    public partial class Gpcomplaint
    {
        public decimal Complaintsid { get; set; }
        public string Compliantnotes { get; set; }
        public DateTime? Dateofcomplaints { get; set; }
        public decimal? Loid { get; set; }
        public decimal? Leid { get; set; }
        public decimal? Managestatus { get; set; }

        public virtual Gplenderstore Le { get; set; }
        public virtual Gploanee Lo { get; set; }
    }
}
