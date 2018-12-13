using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeAppsII.Model
{
    [JsonObjectAttribute]
    class Categorie
    {
        public int Id { get; set; }
        public String Naam { get; set; }
    }
}
