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
        public IList<Actie> acties { get; set; }
        public IList<Evenement> evenementen { get; set; }

        public String Adres { get { return this.Straat + "," + this.Gemeente + " " + this.Land; } }
        public String Openingsuren { get { return Openingsuur + " - " + Sluituur; } }
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
        public bool isClosed { get { return !isOpen; } }

        public bool hasActies { get { return acties.Count != 0; } }

    }
}
