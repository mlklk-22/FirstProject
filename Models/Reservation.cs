using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProject.Models
{
    public partial class Reservation
    {
        public decimal Hallnumber { get; set; }
        public string City { get; set; }
        public string Place { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public DateTime Timefrom { get; set; }
        public DateTime Timeto { get; set; }
        public DateTime Day { get; set; }
        public decimal Price { get; set; }
        public string Username { get; set; }
        public decimal? Categoryid { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public virtual Category CategoryNavigation { get; set; }
        public virtual Login UsernameNavigation { get; set; }
    }
}
