using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NativeAppsII_Services.Models
{
    public class Onderneming
    {
        [Required]
        public int Id { get; set; }
        public String Naam { get; set; }
        public String Openingsuur { get; set; }
        public String Sluituur { get; set; }
        [ForeignKey("Categorie")]
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }
        public String Gemeente { get; set; }
        public String Straat { get; set; }
        public String Land { get; set; }
        public String Website { get; set; }
        public String Telefooonnummer { get; set; }
        public String Information { get; set; }
    }
}