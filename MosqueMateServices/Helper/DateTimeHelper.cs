using MosqueMateServices.Enums;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using Helper;
using System.Globalization;
using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MosqueMateServices.Helper.API;

namespace MosqueMateServices.Helper
{
    public class DateTimeHelper
    {
        public TimeSpan PrayerTimeNow { get; set; }
        public static string TimeAMOrPM = DateTime.Now.ToString("tt", CultureInfo.InvariantCulture);
        public static string TimeFormatWithTT = DateTime.Now.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture);
        public static string hijriDate;
        public static string georgianDate;
        private static DateTime nextPrayerTime;
        /// <summary>  
        ///   return true if prayer time equal time now ,otherwise return false   
        /// </summary>  
        /// <param name="time">TODO</param>  
        /// <param name="key">TODO</param>  
        /// <returns>bool</returns>  
        public static bool IsPrayerTimeNow()
        {
            DateTime current = DateTime.Now;
            int currentTotalMinutes = (int)Math.Round(current.TimeOfDay.TotalMinutes);
            int timeTotalMinutes = (int)Math.Round(nextPrayerTime.TimeOfDay.TotalMinutes);
            return currentTotalMinutes == timeTotalMinutes;
        }

        /// <summary>  
        ///   return true if time now less than prayer time for one hour,otherwise return false   
        /// </summary>  
        /// <param name="time">TODO</param>  
        /// <param name="key">TODO</param>  
        /// <returns>bool</returns>  
        public static bool IsLessOneHour()
        {
            if (nextPrayerTime == default)  // if nextPrayerTime has default value doesn't assgin to new value
                return false;

            var result = nextPrayerTime - DateTime.Now;
            var hour = Math.Abs(result.Hours);
            var Minutes = Math.Abs(result.Minutes);
            if (hour < 0)
                return Minutes <= 59 ? true : false;
            else
                return false;
        }
        /// <summary>  
        ///   return true if time now less than prayer time for half hour,otherwise return false   
        /// </summary>  
        /// <param name="time">TODO</param>  
        /// <param name="key">TODO</param>  
        /// <returns>bool</returns>  
        public static bool IsLessHalfHour()
        {
            if (nextPrayerTime == default)  // if nextPrayerTime has default value doesn't assgin to new value
                return false;

            var result = nextPrayerTime.TimeOfDay.Subtract(DateTime.Now.TimeOfDay);
            return (int)result.TotalMinutes <= 30;
        }
        /// <summary>  
        ///      Subtract next prayer time with time now and return string 
        /// </summary>  
        /// <param name="time">TODO</param>  
        /// <param name="key">TODO</param>  
        /// <returns>string time format</returns>  
        public static string GetSubPrayers()
        {
            var result = nextPrayerTime - DateTime.Now;
            return result.ToString(@"hh\:mm");

        }
        public static PrayerTimesEnum GetNextPrayer(PrayIDataServices.Helper.API.Timings timings)
        {
            if (AppDataRepo.Instance.prayerTimesParse.Count < 1)
            {
                #region SetDictionary
                AppDataRepo.Instance.prayerTimesParse.Add(PrayerTimesEnum.Fajr.ToString(), timings.Fajr);
                AppDataRepo.Instance.prayerTimesParse.Add(PrayerTimesEnum.Sunrise.ToString(), timings.Sunrise);
                AppDataRepo.Instance.prayerTimesParse.Add(PrayerTimesEnum.Dhuhr.ToString(), timings.Dhuhr);
                AppDataRepo.Instance.prayerTimesParse.Add(PrayerTimesEnum.Asr.ToString(), timings.Asr);
                AppDataRepo.Instance.prayerTimesParse.Add(PrayerTimesEnum.Maghrib.ToString(), timings.Maghrib);
                AppDataRepo.Instance.prayerTimesParse.Add(PrayerTimesEnum.Isha.ToString(), timings.Isha);
                #endregion
            }
            var query = (from t in AppDataRepo.Instance.prayerTimesParse
                         where t.Value.TimeOfDay > DateTime.Now.TimeOfDay
                         orderby t.Value
                         select t
                    ).FirstOrDefault();
            nextPrayerTime = query.Value;
            PrayerTimesEnum result;
            result = AfterIsha() ? result = PrayerTimesEnum.Fajr : EnumConverter<CalculationMethods>.GetPrayerTime(query.Key);
            return result;
        }
        public static DateTime GetInternetDate()
        {
            DateTime time = DateTime.Now;
            Task.Run(() =>
            {
                try
                {
                    DateTime dateTime = DateTime.MinValue;
                    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://www.microsoft.com");
                    request.Method = "GET";
                    request.Accept = "text/html, application/xhtml+xml, */*";
                    request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                    System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string todaysDates = response.Headers["date"];
                        dateTime = DateTime.ParseExact(todaysDates, "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                        CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
                        time = dateTime;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            });


            return time;


        }
        public static string ArabicDateName(string lang)
        {
            var culLang = lang == "ar" ? "ar-SA" : "en-SA";
            CultureInfo Arculture = new CultureInfo(culLang);
            string formattedDate = DateTime.Now.ToString("dddd dd MMMM yyyy", Arculture);
            return formattedDate;
        }
        public static string ArabicDayName(string lang)
        {
            var culLang = lang == "ar" ? "ar-SA" : "en-SA";
            CultureInfo Arculture = new CultureInfo(culLang);
            string formattedDate = DateTime.Now.ToString("dddd", Arculture);
            return formattedDate;
        }
        public static bool AfterIsha()
        {
            #region Special case: Current time is after Isha, consider Fajr time of the next day
            if (DateTime.Now.TimeOfDay > AppDataRepo.Instance.prayerTimesParse[PrayerTimesEnum.Isha.ToString()].TimeOfDay)
            {
                var nextDayFajr = AppDataRepo.Instance.prayerTimesParse[PrayerTimesEnum.Fajr.ToString()].AddDays(1);
                AppDataRepo.Instance.prayerTimesParse[PrayerTimesEnum.Fajr.ToString()] = nextDayFajr;
                nextPrayerTime = nextDayFajr;
                return true;
            }

            #endregion
            return false;
        }

    }
}
