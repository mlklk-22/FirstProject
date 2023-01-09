using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProject.Models
{
    public partial class Aboutu
    {
        public string Title1 { get; set; }
        public string Descriptiontitle1 { get; set; }
        public string Title2 { get; set; }
        public string Descriptiontitle2 { get; set; }
        public string Title3 { get; set; }
        public string Descriptiontitle3 { get; set; }
        public string Title4 { get; set; }
        public string Descriptiontitle4 { get; set; }
        public string Titlechose1 { get; set; }
        public string Descriptionchose1 { get; set; }
        public string Titlechose2 { get; set; }
        public string Descriptionchose2 { get; set; }
        public string Titlechose3 { get; set; }
        public string Descriptionchose3 { get; set; }
        public string Titlechose4 { get; set; }
        public string Descriptionchose4 { get; set; }
        public string Titlechose5 { get; set; }
        public string Descriptionchose5 { get; set; }
        public string Titleimage { get; set; }
        public string Choseimage { get; set; }
        public decimal? Roleid { get; set; }
        public string Username { get; set; }

        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public virtual Role Role { get; set; }
        public virtual Login UsernameNavigation { get; set; }
    }
}
