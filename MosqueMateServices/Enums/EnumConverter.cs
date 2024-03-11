using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MosqueMateServices.Enums
{
    public  class EnumConverter<T>
    {
        //public static int ConvertToInt<T>(string value)
        //{
        //    var result = Enum.TryParse<T>(value, out var enumValue) ? enumValue : 0;
        //    return (int)result;
        //}
        public static IEnumerable<T>? ConvertToEnum()
        {
            return Enum.GetValues(typeof(T))?
                           .Cast<T>();
        }
        public static string ConvertToString<T>(int value)
        {
            var result = (T)Enum.ToObject(typeof(T), value);
            return result.ToString();
        }
        public static PrayerTimesEnum GetPrayerTime(string key)
        {
            return key switch
            {
                string value when value == PrayerTimesEnum.Fajr.ToString() => PrayerTimesEnum.Fajr,
                string value when value == PrayerTimesEnum.Sunrise.ToString() => PrayerTimesEnum.Sunrise,
                string value when value == PrayerTimesEnum.Dhuhr.ToString() => PrayerTimesEnum.Dhuhr,
                string value when value == PrayerTimesEnum.Asr.ToString() => PrayerTimesEnum.Asr,
                string value when value == PrayerTimesEnum.Maghrib.ToString() => PrayerTimesEnum.Maghrib,
                string value when value == PrayerTimesEnum.Isha.ToString() => PrayerTimesEnum.Isha,
                _ => PrayerTimesEnum.Fajr,
            };
        }
    }
}
