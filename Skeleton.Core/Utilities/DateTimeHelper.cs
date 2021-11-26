using System;
using System.Globalization;
using System.Threading;

namespace Skeleton.Core.Utilities
{
    public static class DateTimeExtensions
    {
        public static DateTime UtcToLocal(this DateTime utcDateTime)
        {
            DateTime localDateTime = GetLocalDateTime(utcDateTime);
            return localDateTime;
        }

        private static DateTime GetLocalDateTime(DateTime utcDateTime)
        {
            string timeZoneFromConfig = "Turkey Standard Time";
            TimeZoneInfo localTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneFromConfig);
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localTimeZoneInfo);
            return localDateTime;
        }

        public static string GetTurkishDate(DateTime longDate)
        {
            string month = null;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
            var splitedDate = longDate.ToShortDateString().Split('.');

            switch (Convert.ToInt32(splitedDate[1]))
            {
                case 1:
                    month = "Oca";
                    break;
                case 2:
                    month = "Şub";
                    break;
                case 3:
                    month = "Mar";
                    break;
                case 4:
                    month = "Nis";
                    break;
                case 5:
                    month = "May";
                    break;
                case 6:
                    month = "Haz";
                    break;
                case 7:
                    month = "Tem";
                    break;
                case 8:
                    month = "Ağu";
                    break;
                case 9:
                    month = "Eyl";
                    break;
                case 10:
                    month = "Eki";
                    break;
                case 11:
                    month = "Kas";
                    break;
                case 12:
                    month = "Ara";
                    break;
            }

            return splitedDate[0] + " " + month + " " + splitedDate[2];
        }
    }

    public static class DateTimeHelper
    {
        public static DateTime LocalTime()
        {
            return DateTime.UtcNow.UtcToLocal();
        }
    }
}
