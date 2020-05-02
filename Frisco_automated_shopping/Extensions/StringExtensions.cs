using System;
using System.Collections.Generic;
using System.Linq;

namespace Frisco_automated_shopping.Extensions
{
    public static class StringExtensions
    {
        public static void ConvertStringToMonthNumber(this string monthAsString, out int month)
        {
            Dictionary<string, int> months = new Dictionary<string, int>()
            {
                { "Maj", 5},
                { "Cze", 6},
                { "Lip", 7}
            };

            months.TryGetValue(monthAsString.Split(' ').FirstOrDefault(), out month);
        }

        public static (int HourFrom, int MinutesFrom, int HourTo, int MinutesTo) GetHours(this string hoursAsString)
        {
            var hoursAndMinutesFrom = hoursAsString.Split('-').FirstOrDefault().TrimEnd().Split(':');
            var hoursAndMinutesTo = hoursAsString.Split('-').Last().Trim().Split(':');

            return (int.Parse(hoursAndMinutesFrom[0]), int.Parse(hoursAndMinutesFrom[1]), 
                int.Parse(hoursAndMinutesTo[0]), int.Parse(hoursAndMinutesTo[1]));
        }
    }
}
