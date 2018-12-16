using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NativeAppsII.Model
{
    public class Onderneming
    {

        public int Id { get; set; }
        public String Naam { get; set; }
        public String Openingsuur { get; set; }
        public String Sluituur { get; set; }
        public int CategorieId { get; set; }
        public virtual Categorie Categorie { get; set; }
        public String Gemeente { get; set; }
        public String Straat { get; set; }
        public String Land { get; set; }
        public String Website { get; set; }
        public String Telefooonnummer { get; set; }
        public String Information { get; set; }
        public IList<Actie> Acties { get; set; }
        public IList<Evenement> Evenementen { get; set; }
        [JsonIgnore]
        public String Adres { get { return this.Straat + "," + this.Gemeente + " " + this.Land; } }
        [JsonIgnore]
        public String Openingsuren { get { return Openingsuur + " - " + Sluituur; } }
        [JsonIgnore]
        public bool isOpen { get
            {
                var currentTime = DateTime.Now.ToLocalTime();

                if (TimeSpan.Parse(this.Openingsuur) <= currentTime.TimeOfDay && TimeSpan.Parse(this.Sluituur) >= currentTime.TimeOfDay)
                {
                    return true;
                }
                return false;
            }
        }
        [JsonIgnore]
        public bool isClosed { get { return !isOpen; } }
        [JsonIgnore]
        public bool hasActies { get { return Acties.Count != 0; } }


        public Onderneming()
        {

        }
        public Onderneming(string naam, string openingsuur, string sluituur, int categorieId, string gemeente, string straat, string land, string website, string telefooonnummer, string information)
        {
            Naam = naam;
            Openingsuur = openingsuur;
            Sluituur = sluituur;
            CategorieId = categorieId;
            Gemeente = gemeente;
            Straat = straat;
            Land = land;
            Website = website;
            Telefooonnummer = telefooonnummer;
            Information = information;
            this.Acties = new List<Actie>();
            this.Evenementen = new List<Evenement>();
        }
    }
}
