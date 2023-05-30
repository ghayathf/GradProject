using System;
using System.Collections.Generic;

#nullable disable

namespace TheNeqatcomApp.Core.Data
{
    public partial class Gppurchasing
    {
        public decimal Purchaseid { get; set; }
        public DateTime? Paymentdate { get; set; }
        public decimal? Paymenttype { get; set; }
        public decimal? Loanid { get; set; }
        public decimal? PaymentAmount { get; set; }

        public virtual Gploan Loan { get; set; }
    }
}
