using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Contactu
    {
        public string Visittitle { get; set; }
        public string Descriptionvisit { get; set; }
        public string Mailtitle { get; set; }
        public string Descriptionmail { get; set; }
        public string Call { get; set; }
        public string Descriptioncall { get; set; }
        public string Work { get; set; }
        public string Descriptionwork { get; set; }
        public decimal? Roleid { get; set; }
        public string Username { get; set; }

        public virtual Role Role { get; set; }
        public virtual Login UsernameNavigation { get; set; }
    }
}
