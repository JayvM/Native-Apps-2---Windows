using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NativeAppsII.Model;
using Newtonsoft.Json;

namespace NativeAppsII.ViewModel
{
    class EvenementViewModel
    {
        internal async Task<bool> addEvenementAsync(Evenement evenement)
        {
            var evenementJson = JsonConvert.SerializeObject(evenement);
            HttpClient client = new HttpClient();
            var json = await client.PostAsync("http://localhost:65078/api/postEvenement/", new StringContent(evenementJson, System.Text.Encoding.UTF8, "application/json"));
            return json.IsSuccessStatusCode;
        }

        internal async Task<bool> bewerkEvenementAsync(Evenement evenement)
        {
            var evenementJson = JsonConvert.SerializeObject(evenement);
            HttpClient client = new HttpClient();
            var json = await client.PostAsync("http://localhost:65078/api/bewerkEvenement/", new StringContent(evenementJson, System.Text.Encoding.UTF8, "application/json"));
            return json.IsSuccessStatusCode;
        }
    }
}
