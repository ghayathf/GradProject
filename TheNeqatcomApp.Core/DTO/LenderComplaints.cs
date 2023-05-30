using System;
using System.Collections.Generic;
using System.Text;

namespace TheNeqatcomApp.Core.DTO
{
   public class LenderComplaints
    {
        public decimal Complaintsid { get; set; }
        public string Compliantnotes { get; set; }
        public DateTime? Dateofcomplaints { get; set; }
        public decimal? Loid { get; set; }
        public decimal? Leid { get; set; }
        public decimal? Managestatus { get; set; }
        public decimal Loaneeid { get; set; }
        public string Nationalnumber { get; set; }
        public DateTime? Dateofbirth { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Numoffamily { get; set; }
        public decimal? Creditscore { get; set; }
        public decimal? Loaneeuserid { get; set; }
        public decimal? Warncounter { get; set; }
        public decimal? Postponecounter { get; set; }
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
        public decimal Lenderid { get; set; }
    }
}
