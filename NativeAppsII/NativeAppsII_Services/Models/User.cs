using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace NativeAppsII_Services.Models
{
    [DataContract(IsReference = true)]
    public class User
    {
        [DataMember]
        [Required]
        public int Id { get; set; }
        [DataMember]
        [Required]
        public String Gebruikersnaam { get; set; }
        [DataMember]
        [Required]
        public String Wachtwoord { get; set; }
        [DataMember]
        [Required]
        public String Salt { get; set; }
        [DataMember]
        [Required]
        public String Role { get; set; }

    }
       
    
}