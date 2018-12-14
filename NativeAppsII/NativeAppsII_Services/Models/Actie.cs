using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace NativeAppsII_Services.Models
{
    [DataContract(IsReference = true)]
    public class Actie
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public String Beschrijving { get; set; }
        [DataMember]
        public String Image { get; set; }
        [DataMember]
        public DateTime GeldigTot { get; set; }
        [DataMember]
        [ForeignKey("Onderneming")]
        public int OndernemingId { get; set; }
        [DataMember]
        [JsonIgnore] 
        public Onderneming Onderneming { get; set; }
    }
}
