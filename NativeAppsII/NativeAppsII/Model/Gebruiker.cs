using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeAppsII.Model
{
    public class Gebruiker
    {

        public int Id { get; }
        public String Gebruikersnaam { get; set; }
        public String Wachtwoord { get; set; }
       public String Role { get; set; }

        public Gebruiker() { }

        public Gebruiker(string gebruikersnaam,string password,string role)
        {
            this.Gebruikersnaam = gebruikersnaam;
            this.Role = role;
            this.Wachtwoord = password;
        }
        public Gebruiker(int id,string gebruikersnaam, string role)
        {
            this.Id = id;
            this.Gebruikersnaam = gebruikersnaam;
            this.Role = role;
        }

    }
}

