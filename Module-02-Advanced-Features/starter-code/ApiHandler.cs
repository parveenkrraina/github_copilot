using System;
using System. Net.Http;
using System.Threading.Tasks;

namespace Lab2.StarterCode
{
    public class ApiHandler
    {
        public async Task<string> FetchDataAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
        }
        
        public string PostData(string url, string jsonData)
        {
            var client = new HttpClient();
            var content = new StringContent(jsonData);
            var response = client.PostAsync(url, content).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
        
        public T DeserializeResponse<T>(string json)
        {
            return System.Text.Json.JsonSerializer. Deserialize<T>(json);
        }
    }
}