using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Testmonial
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public decimal? Roleid { get; set; }
        public string Username { get; set; }
        public decimal Testmonialid { get; set; }

        public virtual Role Role { get; set; }
        public virtual Login UsernameNavigation { get; set; }
    }
}
