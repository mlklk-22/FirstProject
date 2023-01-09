using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Role
    {
        public Role()
        {
            Aboutus = new HashSet<Aboutu>();
            Contactus = new HashSet<Contactu>();
            Headers = new HashSet<Header>();
            Homes = new HashSet<Home>();
            Leavemessages = new HashSet<Leavemessage>();
            Logins = new HashSet<Login>();
            Testmonials = new HashSet<Testmonial>();
            Visas = new HashSet<Visa>();
        }

        public decimal Roleid { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<Aboutu> Aboutus { get; set; }
        public virtual ICollection<Contactu> Contactus { get; set; }
        public virtual ICollection<Header> Headers { get; set; }
        public virtual ICollection<Home> Homes { get; set; }
        public virtual ICollection<Leavemessage> Leavemessages { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Testmonial> Testmonials { get; set; }
        public virtual ICollection<Visa> Visas { get; set; }
    }
}
