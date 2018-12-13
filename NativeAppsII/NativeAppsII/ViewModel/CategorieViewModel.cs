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
    }
}
