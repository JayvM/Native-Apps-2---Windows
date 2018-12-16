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
    class CategorieViewModel
    {
        public async Task<ObservableCollection<Categorie>> getCategorieën()
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(new Uri("http://localhost:65078/api/getCategorieën/"));
            return JsonConvert.DeserializeObject<ObservableCollection<Categorie>>(json);
        }

        public async Task<Categorie> getCategorie(String naam)
        {
            Categorie categorie = null;
            HttpClient client = new HttpClient();
            string uri = "http://localhost:65078/api/getCategorie/" + naam;
            var json = client.GetAsync(new Uri(uri)).ContinueWith((taskresponse) =>
            {
                var response = taskresponse.Result;
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                categorie = JsonConvert.DeserializeObject<Categorie>(jsonString.Result);

            });
            json.Wait();
            return categorie;
        }
    }
}
