using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Category
    {
        public Category()
        {
            Reservations = new HashSet<Reservation>();
        }

        public decimal Categoryid { get; set; }
        public string Categoryname { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
