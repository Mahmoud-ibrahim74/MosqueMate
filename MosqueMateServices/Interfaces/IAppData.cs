using MosqueMateServices.Helper.API;
using PrayIDataServices.Helper.API;
using System;
using System.Collections.Generic;
namespace MosqueMateServices.Interfaces
{
    public interface IAppData
    {
        public string currLang { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int method { get; set; }
        public string ResourcePath { get; set; }
        public string ContentAPI { get; set; }
        public DateTime AppTime { get; set; }
        public PrayerTimesResponse API_DATA { get; set; }
        public bool AlertNoifyUser { get; set; }
        public bool AlertPrayerNow { get; set; }

        public Dictionary<int, string> CountriesValue();
        public List<string> GetCountryList();
        public Dictionary<int, string> CitiesValue();
        public  Dictionary<int, string> MuzzienNames();
        public Dictionary<(double, double), string> GeoDictionary();
        public KeyValuePair<(double, double), string> GetLocationCity(string city);
        Dictionary<string, DateTime> prayerTimesParse { get; set; }

        public void ShutDownApp();
        public void RestartApp();

    }
}
