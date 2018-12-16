using NativeAppsII.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NativeAppsII.ViewModel
{
    public class GebruikerViewModel
    {
        internal async Task<String> LogInAsync(string username, string password)
        {
            password = HashPassword.Hashpassword(password);
            var dict = new Dictionary<string, string>();
            dict.Add("grant_type", "password");
            dict.Add("username", username);
            dict.Add("password", password);
            string uri = "http://localhost:65078/Token";

            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(dict))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    HttpResponseMessage response = await httpClient.PostAsync(uri, content);

                    var token = response.Content.ReadAsStringAsync().Result;//.Substring()
                    return token;

                }
            }
           
        }

        internal async Task<String> Register(Gebruiker gebruiker)
        {
            gebruiker.Wachtwoord = HashPassword.Hashpassword(gebruiker.Wachtwoord);
            var registerJson = JsonConvert.SerializeObject(gebruiker);
            HttpClient client = new HttpClient();
            var json = await client.PostAsync("http://localhost:65078/api/Register/", new StringContent(registerJson, System.Text.Encoding.UTF8, "application/json"));

            if (json.IsSuccessStatusCode)
            {
                var dict = new Dictionary<string, string>();
                dict.Add("grant_type", "password");
                dict.Add("username", gebruiker.Gebruikersnaam);
                dict.Add("password", gebruiker.Wachtwoord);
                using (var httpClient = new HttpClient())
                {
                    using (var content = new FormUrlEncodedContent(dict))
                    {
                        content.Headers.Clear();
                        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                        HttpResponseMessage response = await httpClient.PostAsync("http://localhost:65078/Token", content);

                        var token = response.Content.ReadAsStringAsync().Result;//.Substring()
                        return token;

                    }
                }
            }
            else return "error";

        }
    }

}


