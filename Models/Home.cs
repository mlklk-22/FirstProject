using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProject.Models
{
    public partial class Home
    {
        public string Titleslider1 { get; set; }
        public string Descriptionslider1 { get; set; }
        public string Titleslider2 { get; set; }
        public string Descriptionslider2 { get; set; }
        public string Titleslider3 { get; set; }
        public string Descriptionslider3 { get; set; }
        public string Sliderimage { get; set; }
        public string Video { get; set; }
        public string Titleservice1 { get; set; }
        public string Descriptionservice1 { get; set; }
        public string Titleservice2 { get; set; }
        public string Descriptionservice2 { get; set; }
        public string Titleservice3 { get; set; }
        public string Descriptionservice3 { get; set; }
        public decimal? Roleid { get; set; }
        public string Username { get; set; }

        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }

        public virtual Role Role { get; set; }
        public virtual Login UsernameNavigation { get; set; }
    }
}
