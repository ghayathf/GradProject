using System;
using System.Collections.Generic;
using System.Text;

namespace TheNeqatcomApp.Core.DTO
{
   public class LoginClaims
    {
        public decimal Userid { get; set; }
        public decimal lenderId { get; set; }
        public decimal loaneeId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phonenum { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Userimage { get; set; }
        public decimal? Creditscore { get; set; }
        public decimal? Registerstatus { get; set; }

    }
}
