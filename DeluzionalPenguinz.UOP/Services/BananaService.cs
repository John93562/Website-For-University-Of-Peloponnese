using DeluzionalPenguinz.UOP.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

//namespace DeluzionalPenguinz.UOP.Services
//{
//    public class BananaService : IBananaService
//    {
//        private readonly HttpClient httpClient;

//        public BananaService(HttpClient httpClient)
//        {
//            this.httpClient = httpClient;
//        }

//        public async Task<bool> AddBananaAsync(Banana banana)
//        {
//            var content = JsonConvert.SerializeObject(banana);
//            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

//            var response = await httpClient.PostAsync($"api/bananas/AddBanana", bodyContent);

//            bool status = false;

//            if (response.IsSuccessStatusCode)
//            {
//                var contentResult = await response.Content.ReadAsStringAsync();

//                status = JsonConvert.DeserializeObject<bool>(contentResult);
//            }

//            return status;
//        }

//        public async Task<bool> DeleteBananaAsync(Banana banana)
//        {
//            var content = JsonConvert.SerializeObject(banana);
//            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

//            var response = await httpClient.PostAsync($"api/bananas/DeleteBanana", bodyContent);

//            bool status = false;

//            if (response.IsSuccessStatusCode)
//            {
//                var contentResult = await response.Content.ReadAsStringAsync();

//                status = JsonConvert.DeserializeObject<bool>(contentResult);
//            }

//            return status;
//        }

//        public async Task<List<Banana>> GetBananasAsync()
//        {

//            var response = await httpClient.GetAsync($"api/bananas/GetBananasWithoutId");

//            List<Banana> bananas = null;

//            if (response.IsSuccessStatusCode)
//            {
//                var content = await response.Content.ReadAsStringAsync();

//                bananas = JsonConvert.DeserializeObject<List<Banana>>(content);
//            }

//            return bananas;

//        }

//    }
//}