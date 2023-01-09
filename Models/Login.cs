using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Login
    {
        public Login()
        {
            Aboutus = new HashSet<Aboutu>();
            Contactus = new HashSet<Contactu>();
            Headers = new HashSet<Header>();
            Homes = new HashSet<Home>();
            Leavemessages = new HashSet<Leavemessage>();
            Reservations = new HashSet<Reservation>();
            Testmonials = new HashSet<Testmonial>();
            Visas = new HashSet<Visa>();
        }

        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal? Roleid { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Aboutu> Aboutus { get; set; }
        public virtual ICollection<Contactu> Contactus { get; set; }
        public virtual ICollection<Header> Headers { get; set; }
        public virtual ICollection<Home> Homes { get; set; }
        public virtual ICollection<Leavemessage> Leavemessages { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Testmonial> Testmonials { get; set; }
        public virtual ICollection<Visa> Visas { get; set; }
    }
}
