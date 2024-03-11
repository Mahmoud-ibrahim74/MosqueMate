using PrayIDataServices.Helper.API;
using System.Threading.Tasks;

namespace MosqueMateServices.Interfaces
{
    public interface IPrayerAdhanAPI
    {
        public string queryParam { get; set; }
        public string responceContent { get; set; } 
        public string BaseUrl { get; set; } 
        public Task<string> GetAdanResponceAsync();
        public  Task<bool> GetCalenderResponceAsync();
    }
}
