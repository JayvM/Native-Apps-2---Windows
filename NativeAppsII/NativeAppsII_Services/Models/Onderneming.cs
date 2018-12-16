using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NativeAppsII_Services.Models
{
    [DataContract(IsReference = true)]
    public class Onderneming
    {
        [Required]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public String Naam { get; set; }
        [DataMember]
        public String Openingsuur { get; set; }
        [DataMember]
        public String Sluituur { get; set; }
        [DataMember]
        [ForeignKey("Categorie")]
        public int CategorieId { get; set; }
        [DataMember]
        public Categorie Categorie { get; set; }
        [DataMember]
        public String Gemeente { get; set; }
        [DataMember]
        public String Straat { get; set; }
        [DataMember]
        public String Land { get; set; }
        [DataMember]
        public String Website { get; set; }
        [DataMember]
        public String Telefooonnummer { get; set; }
        [DataMember]
        public String Information { get; set; }
        [DataMember]
        public List<Actie> Acties { get; set; }
        [DataMember]
        public List<Evenement> Evenementen { get; set; }
        [DataMember]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [JsonIgnore] 
        public User User { get; set; }
    }
}