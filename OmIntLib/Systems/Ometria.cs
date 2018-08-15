using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace OmIntLib.Systems
{
    public class Ometria
    {
        private readonly HttpClient httpClient;

        public Ometria(Settings settings)
        {
            httpClient = new HttpClient() { BaseAddress = new Uri(settings["BaseUrl"]) };
            httpClient.DefaultRequestHeaders.Add("X-Ometria-Auth", settings["ApiKey"]);
        }

        public Ometria(string baseUlr, string apiKey)
        {
            httpClient = new HttpClient() { BaseAddress = new Uri(baseUlr) };
            httpClient.DefaultRequestHeaders.Add("X-Ometria-Auth", apiKey);
        }

        ~Ometria() => httpClient.Dispose();

        private string Post(string path, string content)
        {
            HttpResponseMessage result = httpClient.PostAsync(path, new StringContent(content)).GetAwaiter().GetResult();
            if (!result.IsSuccessStatusCode) throw new Exception($"{result.ReasonPhrase} {result.Content.ReadAsStringAsync().GetAwaiter().GetResult()}");
            return result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        private string Get(string url)
        {
            HttpResponseMessage result = httpClient.GetAsync(url).GetAwaiter().GetResult();
            if (!result.IsSuccessStatusCode) throw new Exception(result.ReasonPhrase);
            return result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }


        public string GetProduct(string id) => Get($"products/{id}");

        public string ListProducts(int limit = 10, int offsett = 0, bool? active = null) => Get($"products?limit={limit}&offset={offsett}{(active == null ? "" : $"&active={active.ToString().ToLower()}")}");

        public void CreateProduct(string productJson, string id) => Post($"products/{id}", productJson);


        public string ListCollections(int limit = 10, int offsett = 0) => Get($"contacts?limit={limit}&offset={offsett}");

        public string GetContactListing(string collection, string id) => Get($"contacts/{collection}/{id}");

        public string ListContacts(string collection, int limit = 10, int offsett = 0) => Get($"contacts/{collection}?limit={limit}&offset={offsett}");

        public string CreateContactListing(string collection, string contactJson, string id) => Post($"contacts/{collection}/{id}", contactJson);


        public string GetOrder(string id) => Get($"orders/{id}");

        public string ListOrders(int limit = 10, int offsett = 0) => Get($"orders?limit={limit}&offset={offsett}");

        public string CreateOrder(string orderJson, string id) => Post($"orders/{id}", orderJson);


        public string Push(string objJson) => Post("push", objJson);

        public string ListErrors() => Get("push/_errors");

        
        public string ListProfiles(int limit = 10, int offsett = 0, DateTime? updateSince = null) 
            => Get($"profiles?limit={limit}&offset={offsett}{(updateSince == null ? "" : $"&updateSince={updateSince:yyyy-MM-dd}")}");

        public string GetProfile(string id) => Get($"profiles/{id}");

        public string GetUnsubscribes(DateTime? since, DateTime? before, string type) => Get($"unsubscribes?since={since}&before={before}&type={type}");
    }
}
