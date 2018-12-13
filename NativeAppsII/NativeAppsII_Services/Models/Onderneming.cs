using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NativeAppsII_Services.Models
{
    public class Onderneming
    {
        [Required]
        public int Id { get; set; }
        public String naam { get; set; }
        public String Openingsuur { get; set; }
        public String Sluituur { get; set; }
        public String Categorie { get; set; }
        public String Gemeente { get; set; }
        public String Straat { get; set; }
        public String Land { get; set; }
        public String Website { get; set; }
    }
}