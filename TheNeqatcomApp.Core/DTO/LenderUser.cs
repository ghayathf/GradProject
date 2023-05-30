using System;
using System.Collections.Generic;
using System.Text;

namespace TheNeqatcomApp.Core.DTO
{
    public class LenderUser
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

        public decimal Lenderid { get; set; }
        public string Commercialregister { get; set; }
        //public decimal? Userid { get; set; }
        public decimal? Registerstatus { get; set; }
        public decimal? Shadowstatus { get; set; }
        public string Companysize { get; set; }
        public string Siteurl { get; set; }
    }
}
