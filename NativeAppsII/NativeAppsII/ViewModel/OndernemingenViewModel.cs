using NativeAppsII.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NativeAppsII.ViewModel
{
    class OndernemingenViewModel
    {
        public async Task<ObservableCollection<Onderneming>> getOndernemingen()
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(new Uri("http://localhost:65078/api/getOndernemingen/"));
            return JsonConvert.DeserializeObject<ObservableCollection<Onderneming>>(json);
        }

        public async Task<bool> addOnderneming(Onderneming onderneming)
        {
            var ondernemingJson = JsonConvert.SerializeObject(onderneming);
            HttpClient client = new HttpClient();
            var json = await client.PostAsync("http://localhost:65078/api/postOnderneming/", new StringContent(ondernemingJson, System.Text.Encoding.UTF8, "application/json"));
            return json.IsSuccessStatusCode;
        }
        public async Task<bool> bewerkOnderneming(Onderneming onderneming)
        {
            var ondernemingJson = JsonConvert.SerializeObject(onderneming);
            HttpClient client = new HttpClient();
            var json = await client.PostAsync("http://localhost:65078/api/bewerkOnderneming/", new StringContent(ondernemingJson, System.Text.Encoding.UTF8, "application/json"));
            return json.IsSuccessStatusCode;
        }

        public async Task<ObservableCollection<Onderneming>> getOndernemingenVanGebruiker(int id)
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(new Uri("http://localhost:65078/api/getOndernemingen/" + id));
            return JsonConvert.DeserializeObject<ObservableCollection<Onderneming>>(json);
        }

        public async Task<bool> deleteOnderneming(int id)
        {
            HttpClient client = new HttpClient();
            var json = await client.DeleteAsync(new Uri("http://localhost:65078/api/deleteOndernemingen/" + id));
            return json.IsSuccessStatusCode;
        }
    }
}
