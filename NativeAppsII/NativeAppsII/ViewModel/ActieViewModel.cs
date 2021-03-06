﻿using NativeAppsII.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NativeAppsII.ViewModel
{
    public class ActieViewModel
    {
        internal async Task<bool> addActieAsync(Actie actie)
        {
            var evenementJson = JsonConvert.SerializeObject(actie);
            HttpClient client = new HttpClient();
            var json = await client.PostAsync("http://localhost:65078/api/postActie/", new StringContent(evenementJson, System.Text.Encoding.UTF8, "application/json"));
            return json.IsSuccessStatusCode;
        }

        internal async Task<bool> bewerkActieAsync(Actie actie)
        {
            var evenementJson = JsonConvert.SerializeObject(actie);
            HttpClient client = new HttpClient();
            var json = await client.PostAsync("http://localhost:65078/api/bewerkActie/", new StringContent(evenementJson, System.Text.Encoding.UTF8, "application/json"));
            return json.IsSuccessStatusCode;
        }

        public async Task<bool> deleteActie(int id)
        {
            HttpClient client = new HttpClient();
            var json = await client.DeleteAsync(new Uri("http://localhost:65078/api/deleteActie/" + id));
            return json.IsSuccessStatusCode;
        }
    }
}
