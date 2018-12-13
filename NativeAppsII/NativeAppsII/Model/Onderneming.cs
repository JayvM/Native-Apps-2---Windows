using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeAppsII.Model
{
    class Onderneming
    {
        public int Id { get; set; }
        public String Naam { get; set; }
        public String Openingsuur { get; set; }
        public String Sluituur { get; set; }
        public String Categorie { get; set; }
        public String Gemeente { get; set; }
        public String Straat { get; set; }
        public String Land { get; set; }
        public String Website { get; set; }
    }
}
