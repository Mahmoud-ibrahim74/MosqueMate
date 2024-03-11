using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MosqueMateServices.Helper.API
{
    public class API
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        public static string url { get; set; }
        public static string HttpException { get; set; }
        
        public static async Task<string> GetMethodAsync()
        {
            try
            {
                var responce = await _httpClient.GetAsync(url);
                if (responce.IsSuccessStatusCode)
                {
                    return await responce.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                HttpException =  ex.Message;
                return null;
            }
            return null;
        }
    }
}
