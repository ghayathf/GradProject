using System;
using System.Collections.Generic;
using System.Text;

namespace TheNeqatcomApp.Core.DTO
{
    public class LoaneeComplaintsDTO
    {
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
        public decimal Complaintsid { get; set; }
        public string Compliantnotes { get; set; }
        public DateTime? Dateofcomplaints { get; set; }
        public decimal? Loid { get; set; }
        public decimal? Leid { get; set; }
        public decimal Loaneeid { get; set; }
        public string Nationalnumber { get; set; }
       
        public decimal Lenderid { get; set; }
        public string Commercialregister { get; set; }
        public decimal? Lenderuserid { get; set; }
        public decimal? Registerstatus { get; set; }
        public decimal? Shadowstatus { get; set; }
        public string Companysize { get; set; }
        public decimal? Managestatus { get; set; }
        public string Siteurl { get; set; }
        public DateTime? Warndate { get; set; }
        public decimal? Warncounter { get; set; }
    }
}
