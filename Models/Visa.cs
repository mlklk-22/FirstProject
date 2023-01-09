using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Visa
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Cardnumber { get; set; }
        public DateTime Exp { get; set; }
        public decimal Thrnum { get; set; }
        public string Pocket { get; set; }
        public decimal? Roleid { get; set; }
        public string Username { get; set; }

        public virtual Role Role { get; set; }
        public virtual Login UsernameNavigation { get; set; }
    }
}
