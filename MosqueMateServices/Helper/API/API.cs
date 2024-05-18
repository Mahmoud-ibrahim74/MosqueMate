using MosqueMateServices.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MosqueMateServices.Helper.API
{
    public class API : IAPI
    {
        private readonly HttpClient _httpClient;
        public API(string url)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
        }

        public Task<string> GetAsync()
        {
            try
            {
                return _httpClient.GetStringAsync(_httpClient.BaseAddress);
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
