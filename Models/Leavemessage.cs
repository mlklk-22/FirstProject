using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Leavemessage
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public decimal? Roleid { get; set; }
        public string Username { get; set; }

        public virtual Role Role { get; set; }
        public virtual Login UsernameNavigation { get; set; }
    }
}
