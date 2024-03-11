using MosqueMateServices.Helper.API;
using MosqueMateServices.Interfaces;
using PrayIDataServices.Helper.API;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace MosqueMateServices.Repositories
{
    public class AppDataRepo : IAppData
    {
        public static string AppPath { get; private set; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public string ResourcePath { get; set; } = AppPath + @"\AppResources\";
        public bool AlertNoifyUser { get; set; }
        public bool AlertPrayerNow { get; set; }
        public string currLang { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int method { get; set; }
        public Dictionary<string, DateTime> prayerTimesParse { get; set; } = new Dictionary<string, DateTime>();

        public string ContentAPI { get; set; }
        public DateTime AppTime { get; set; }
        public PrayerTimesResponse API_DATA { get; set; }



        #region SignletonPatteren
        private static AppDataRepo instance;
        private static readonly object _lock = new object();
        private AppDataRepo()
        {

        }
        public static AppDataRepo Instance
        {
            get
            {
                lock (_lock) // For thread Safty if two or more thread reach at same time to intialize new instance only allow one 
                {
                    if (instance == null)
                    {
                        instance = new AppDataRepo();
                    }
                }
                return instance;
            }
        }


        #endregion



        public Dictionary<int, string> CitiesValue()
        {
            Dictionary<int, string> cities = new Dictionary<int, string>()
        {
              #region cities
		            { 1, "Abuja" },
                    { 2, "Accra" },
                    { 3, "Addis Ababa" },
                    { 4, "Algiers" },
                    { 5, "Amman" },
                    { 6, "Andorra la Vella" },
                    { 7, "Ankara" },
                    { 8, "Apia" },
                    { 9, "Ashgabat" },
                    { 10, "Asmara" },
                    { 11, "Astana" },
                    { 12, "Asunción" },
                    { 13, "Athens" },
                    { 14, "Baghdad" },
                    { 15, "Baku" },
                    { 16, "Bandar Seri Begawan" },
                    { 17, "Bangkok" },
                    { 18, "Bangui" },
                    { 19, "Banjul" },
                    { 20, "Basseterre" },
                    { 21, "Beijing" },
                    { 22, "Beirut" },
                    { 23, "Belgrade" },
                    { 24, "Belmopan" },
                    { 25, "Berlin" },
                    { 26, "Bern" },
                    { 27, "Bishkek" },
                    { 28, "Bissau" },
                    { 29, "Bogotá" },
                    { 30, "Brasília" },
                    { 31, "Bratislava" },
                    { 32, "Brazzaville" },
                    { 33, "Bridgetown" },
                    { 34, "Brussels" },
                    { 35, "Bucharest" },
                    { 36, "Budapest" },
                    { 37, "Buenos Aires" },
                    { 38, "Bujumbura" },
                    { 39, "Cairo" },
                    { 40, "Canberra" },
                    { 41, "Caracas" },
                    { 42, "Castries" },
                    { 43, "Colombo" },
                    { 44, "Conakry" },
                    { 45, "Copenhagen" },
                    { 46, "Dakar" },
                    { 47, "Damascus" },
                    { 48, "Dhaka" },
                    { 49, "Dili" },
                    { 50, "Djibouti" },
                    { 51, "Dodoma" },
                    { 52, "Doha" },
                    { 53, "Dublin" },
                    { 54, "Dushanbe" },
                    { 55, "Freetown" },
                    { 56, "Funafuti" },
                    { 57, "Gaborone" },
                    { 58, "Georgetown" },
                    { 59, "Guatemala City" },
                    { 60, "Hanoi" },
                    { 61, "Harare" },
                    { 62, "Havana" },
                    { 63, "Helsinki" },
                    { 64, "Honiara" },
                    { 65, "Islamabad" },
                    { 66, "Jakarta" },
                    { 67, "Jerusalem" },
                    { 68, "Juba" },
                    { 69, "Kabul" },
                    { 70, "Kampala" },
                    { 71, "Khartoum" },
                    { 72, "Kigali" },
                    { 73, "Kingston" },
                    { 74, "Kingstown" },
                    { 75, "Kinshasa" },
                    { 76, "Kuwait City" },
                    { 77, "Kyiv" },
                    { 78, "Libreville" },
                    { 79, "Lima" },
                    { 80, "Lisbon" },
                    { 81, "Ljubljana" },
                    { 82, "Lomé" },
                    { 83, "London" },
                    { 84, "Luanda" },
                    { 85, "Lusaka" },
                    { 86, "Madrid" },
                    { 87, "Malabo" },
                    { 88, "Manama" },
                    { 89, "Manila" },
                    { 90, "Maseru" },
                    { 91, "Minsk" },
                    { 92, "Mogadishu" },
                    { 93, "Monrovia" },
                    { 94, "Montevideo" },
                    { 95, "Moroni" },
                    { 96, "Moscow" },
                    { 97, "N'Djamena" },
                    { 98, "Nairobi" },
                    { 99, "Nassau" },
                    { 100, "New Delhi" },
                    { 101, "Nicosia" },
                    { 102, "Nuku'alofa" },
                    { 103, "Ottawa" },
                    { 104, "Ouagadougou" },
                    { 105, "Palikir" },
                    { 106, "Panama City" },
                    { 107, "Paramaribo" },
                    { 108, "Paris" },
                    { 109, "Phnom Penh" },
                    { 110, "Port Moresby" },
                    { 111, "Port of Spain" },
                    { 112, "Port Vila" },
                    { 113, "Port-au-Prince" },
                    { 114, "Porto-Novo" },
                    { 115, "Prague" },
                    { 116, "Praia" },
                    { 117, "Pretoria" },
                    { 118, "Pristina" },
                    { 119, "Quito" },
                    { 120, "Reykjavik" },
                    { 121, "Riga" },
                    { 122, "Riyadh" },
                    { 123, "Rome" },
                    { 124, "Roseau" },
                    { 125, "Saint George's" },
                    { 126, "San José" },
                    { 127, "San Marino" },
                    { 128, "San Salvador" },
                    { 129, "Sana'a" },
                    { 130, "Santiago" },
                    { 131, "Santo Domingo" },
                    { 132, "Sao Tomé" },
                    { 133, "Sarajevo" },
                    { 134, "Seoul" },
                    { 135, "Singapore" },
                    { 136, "Sofia" },
                    { 137, "Stockholm" },
                    { 138, "Sucre" },
                    { 139, "Suva" },
                    { 140, "Taipei" },
                    { 141, "Tallinn" },
                    { 142, "Tarawa" },
                    { 143, "Tashkent" },
                    { 144, "Tbilisi" },
                    { 145, "Tegucigalpa" },
                    { 146, "Tehran" },
                    { 147, "Thimphu" },
                    { 148, "Tirana" },
                    { 149, "Tokyo" },
                    { 150, "Tripoli" },
                    { 151, "Tunis" },
                    { 152, "Vatican City" },
                    { 153, "Victoria" },
                    { 154, "Vienna" },
                    { 155, "Vientiane" },
                    { 156, "Warsaw" },
                    { 157, "Washington, D.C." },
                    { 158, "Yaoundé" },
                    { 159, "Yerevan" },
                    { 160, "Zagreb" }, 
	#endregion
            };
            return cities;
        }
        public Dictionary<int, string> CountriesValue()
        {
            Dictionary<int, string> countries = new Dictionary<int, string>()
        {
            #region CountriesValue
            { 1, "Afghanistan" },
            { 2, "Albania" },
            { 3, "Algeria" },
            { 4, "Andorra" },
            { 5, "Angola" },
            { 6, "Antigua and Barbuda" },
            { 7, "Argentina" },
            { 8, "Armenia" },
            { 9, "Australia" },
            { 10, "Austria" },
            { 11, "Azerbaijan" },
            { 12, "Bahamas" },
            { 13, "Bahrain" },
            { 14, "Bangladesh" },
            { 15, "Barbados" },
            { 16, "Belarus" },
            { 17, "Belgium" },
            { 18, "Belize" },
            { 19, "Benin" },
            { 20, "Bhutan" },
            { 21, "Bolivia" },
            { 22, "Bosnia and Herzegovina" },
            { 23, "Botswana" },
            { 24, "Brazil" },
            { 25, "Brunei" },
            { 26, "Bulgaria" },
            { 27, "Burkina Faso" },
            { 28, "Burundi" },
            { 29, "Cabo Verde" },
            { 30, "Cambodia" },
            { 31, "Cameroon" },
            { 32, "Canada" },
            { 33, "Central African Republic" },
            { 34, "Chad" },
            { 35, "Chile" },
            { 36, "China" },
            { 37, "Colombia" },
            { 38, "Comoros" },
            { 39, "Congo" },
            { 40, "Costa Rica" },
            { 41, "Croatia" },
            { 42, "Cuba" },
            { 43, "Cyprus" },
            { 44, "Czech Republic" },
            { 45, "Denmark" },
            { 46, "Djibouti" },
            { 47, "Dominica" },
            { 48, "Dominican Republic" },
            { 49, "East Timor (Timor-Leste)" },
            { 50, "Ecuador" },
            { 51, "Egypt" },
            { 52, "El Salvador" },
            { 53, "Equatorial Guinea" },
            { 54, "Eritrea" },
            { 55, "Estonia" },
            { 56, "Eswatini" },
            { 57, "Ethiopia" },
            { 58, "Fiji" },
            { 59, "Finland" },
            { 60, "France" },
            { 61, "Gabon" },
            { 62, "Gambia" },
            { 63, "Georgia" },
            { 64, "Germany" },
            { 65, "Ghana" },
            { 66, "Greece" },
            { 67, "Grenada" },
            { 68, "Guatemala" },
            { 69, "Guinea" },
            { 70, "Guinea-Bissau" },
            { 71, "Guyana" },
            { 72, "Haiti" },
            { 73, "Honduras" },
            { 74, "Hungary" },
            { 75, "Iceland" },
            { 76, "India" },
            { 77, "Indonesia" },
            { 78, "Iran" },
            { 79, "Iraq" },
            { 80, "Ireland" },
            { 81, "Palestine" }, // Free Palestine
            { 82, "Italy" },
            { 83, "Ivory Coast" },
            { 84, "Jamaica" },
            { 85, "Japan" },
            { 86, "Jordan" },
            { 87, "Kazakhstan" },
            { 88, "Kenya" },
            { 89, "Kiribati" },
            { 90, "Korea, North" },
            { 91, "Korea, South" },
            { 92, "Kosovo" },
            { 93, "Kuwait" },
            { 94, "Kyrgyzstan" },
            { 95, "Laos" },
            { 96, "Latvia" },
            { 97, "Lebanon" },
            { 98, "Lesotho" },
            { 99, "Liberia" },
            { 100, "Libya" },
            { 101, "Liechtenstein" },
            { 102, "Lithuania" },
            { 103, "Luxembourg" },
            { 104, "Madagascar" },
            { 105, "Malawi" },
            { 106, "Malaysia" },
            { 107, "Maldives" },
            { 108, "Mali" },
            { 109, "Malta" },
            { 110, "Marshall Islands" },
            { 111, "Mauritania" },
            { 112, "Mauritius" },
            { 113, "Mexico" },
            { 114, "Micronesia" },
            { 115, "Moldova" },
            { 116, "Monaco" },
            { 117, "Mongolia" },
            { 118, "Montenegro" },
            { 119, "Morocco" },
            { 120, "Mozambique" },
            { 121, "Myanmar (Burma)" },
            { 122, "Namibia" },
            { 123, "Nauru" },
            { 124, "Nepal" },
            { 125, "Netherlands" },
            { 126, "New Zealand" },
            { 127, "Nicaragua" },
            { 128, "Niger" },
            { 129, "Nigeria" },
            { 130, "North Macedonia" },
            { 131, "Norway" },
            { 132, "Oman" },
            { 133, "Pakistan" },
            { 134, "Palau" },
            { 135, "Panama" },
            { 136, "Papua New Guinea" },
            { 137, "Paraguay" },
            { 138, "Peru" },
            { 139, "Philippines" },
            { 140, "Poland" },
            { 141, "Portugal" },
            { 142, "Qatar" },
            { 143, "Romania" },
            { 144, "Russia" },
            { 145, "Rwanda" },
            { 146, "Saint Kitts and Nevis" },
            { 147, "Saint Lucia" },
            { 148, "Saint Vincent and the Grenadines" },
            { 149, "Samoa" },
            { 150, "San Marino" },
            { 151, "Spain" },
            #endregion
        };
            return countries;
        }
        public List<string> GetCountryList()
        {
            List<string> cultureList = new List<string>();

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.LCID);

                if (!(cultureList.Contains(region.EnglishName)))
                {
                    cultureList.Add(region.EnglishName);
                }
            }
            return cultureList.OrderBy(x=>x).ToList();
        }
        public Dictionary<int, string> MuzzienNames()
        {
            Dictionary<int, string> names = new Dictionary<int, string>()
        {
            #region MuzzienNames
            { 1, "AbdelAbaset" },
            { 2, "AhmedKordy" },
            { 3, "Egypt" },
            { 4, "ElDemerdash" },
            { 5, "Elmenshawy" },
            { 6, "ElOfasy" },
            { 7, "ELQods" },
            { 8, "HamzaElmagally" },
            { 9, "fajrAdhan" },
            { 10, "IslamSobhy" },
            { 11, "MansoorAzZahrani" },
            { 12, "Qtaamy" },

                #endregion
            };
            return names;
        }
        public Dictionary<(double, double), string> GeoDictionary()
        {
            Dictionary<(double, double), string> geoDictionary = new Dictionary<(double, double), string>();
            geoDictionary.Add((40.7128, -74.0060), "United States"); // New York City
            geoDictionary.Add((51.5074, -0.1278), "United Kingdom");  // London
            geoDictionary.Add((35.6895, 139.6917), "Japan");         // Tokyo
            geoDictionary.Add((55.7558, 37.6176), "Russia");          // Moscow
            geoDictionary.Add((48.8566, 2.3522), "France");          // Paris
            geoDictionary.Add((41.9028, 12.4964), "Italy");          // Rome
            geoDictionary.Add((-33.8688, 151.2093), "Australia");    // Sydney
            geoDictionary.Add((40.4168, -3.7038), "Spain");           // Madrid
            geoDictionary.Add((39.9042, 116.4074), "China");         // Beijing
            geoDictionary.Add((-22.9068, -43.1729), "Brazil");       // Rio de Janeiro
            geoDictionary.Add((55.7558, -37.6176), "Argentina");     // Buenos Aires
            geoDictionary.Add((19.4326, -99.1332), "Mexico");        // Mexico City
            geoDictionary.Add((-33.4378, -70.6504), "Chile");        // Santiago
            geoDictionary.Add((37.9838, 23.7275), "Greece");         // Athens
            geoDictionary.Add((55.7558, 12.5148), "Denmark");        // Copenhagen
            geoDictionary.Add((37.5665, 126.9780), "South Korea");   // Seoul
            geoDictionary.Add((-26.2041, 28.0473), "South Africa");  // Johannesburg
            geoDictionary.Add((25.2769, 55.2962), "United Arab Emirates");  // Dubai
            geoDictionary.Add((-6.2088, 106.8456), "Indonesia");     // Jakarta
            geoDictionary.Add((45.4215, -75.6994), "Canada");          // Ottawa
            geoDictionary.Add((51.1657, 10.4515), "Germany");          // Berlin
            geoDictionary.Add((30.0444, 31.2357), "Egypt");           // Cairo
            geoDictionary.Add((53.3498, -6.2603), "Ireland");                // Dublin
            return geoDictionary;
        }
        public KeyValuePair<(double, double), string> GetLocationCity(string city)
        {
            var result = GeoDictionary().Where(x => x.Value.Contains(city)).FirstOrDefault();
            return result;
        }

        public void ShutDownApp()
        {
            Application.Exit();
        }

        public void RestartApp()
        {
            Application.Restart();
        }
    }
}
