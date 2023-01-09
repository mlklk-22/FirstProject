using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Header
    {
        public string Rights { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Title3 { get; set; }
        public string Title4 { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Contact3 { get; set; }
        public string Contact4 { get; set; }
        public string News { get; set; }
        public decimal? Roleid { get; set; }
        public string Username { get; set; }

        public virtual Role Role { get; set; }
        public virtual Login UsernameNavigation { get; set; }
    }
}
