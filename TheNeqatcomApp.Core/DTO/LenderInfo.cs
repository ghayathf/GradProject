using System;
using System.Collections.Generic;
using System.Text;

namespace TheNeqatcomApp.Core.DTO
{
    public class LenderInfo
    {
        public string Commercialregister { get; set; }
        public decimal? Lenderuserid { get; set; }
        public decimal? Registerstatus { get; set; }
        public decimal? Shadowstatus { get; set; }
        public string Companysize { get; set; }
        public string Siteurl { get; set; }
        public decimal? Warncounter { get; set; }
        public DateTime? Warndate { get; set; }
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
        public decimal Meetingid { get; set; }
        public DateTime? Startdate { get; set; }
        public string Meetingurl { get; set; }
        public decimal? Feedbackk { get; set; }
        public string Meetingtime { get; set; }
        public decimal? Loaneeid { get; set; }
        public decimal? Lenderid { get; set; } 
        public decimal? Loanid { get; set; }
        public double AVG_FEEDBACK { get; set; }
    }
}
