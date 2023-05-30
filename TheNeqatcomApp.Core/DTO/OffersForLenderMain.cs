using System;
using System.Collections.Generic;
using System.Text;

namespace TheNeqatcomApp.Core.DTO
{
    public class OffersForLenderMain
    {
        public decimal Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Categoryimage { get; set; }
        public decimal Offerid { get; set; }
        public decimal? Totalmonths { get; set; }
        public string Descriptions { get; set; }
        public decimal? Lenderid { get; set; }
        public decimal? Minmonth { get; set; }
    }
}
