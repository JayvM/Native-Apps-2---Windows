using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NativeAppsII_Services.Models
{
    public class UserSecurity
    {
        public static bool Login(string username, string password)
        {
            return new ServiceContext().users.Any(user => user.Gebruikersnaam.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Wachtwoord == password);
             
        }
    }
}