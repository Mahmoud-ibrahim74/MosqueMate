using Humanizer;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MosqueMate.Helper
{
    public static class StringHelper
    {
        public static string NumberToWords(this int number)
        {
            string word = number.ToWords(new CultureInfo("ar-SA"));
            return word;
        }
    }
}
