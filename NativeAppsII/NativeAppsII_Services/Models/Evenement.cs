using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeAppsII_Services.Models
{
    public class Evenement
    {
        public int Id { get; set; }
        public string Beschrijving { get; set; }
        public string Plaats { get; set; }
        public DateTime Datum { get; set; }
        [ForeignKey("Onderneming")]
        public int OndernemingId { get; set; }
        [JsonIgnore]
        public Onderneming Onderneming { get; set; }
      

    }
}
