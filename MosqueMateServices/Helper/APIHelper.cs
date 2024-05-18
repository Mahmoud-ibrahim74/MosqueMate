using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MosqueMateServices.Helper
{
    public class APIHelper
    {
        private readonly HttpClient httpClient;
        public APIHelper(string url) 
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
        }
        public async Task<string> GetAsync()
        {
            try
            {
                var response = await httpClient.GetAsync(httpClient.BaseAddress);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }

        }

    }
}
