using System;

namespace Common.Helper
{
    public class DateTimeHelper
    {
        public static DateTime? ValidateSQLDate(DateTime? dateTime)
        {
            if (dateTime == null) return null;
            DateTime rngMin = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            DateTime rngMax = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
            if (dateTime < rngMin) return rngMin;
            else if (dateTime > rngMax) return rngMax;
            else return dateTime;
        }

        public static string ToFormatMMDDYYYY(DateTime? datetime)
        {
            if(datetime == null) return string.Empty;
            return ((DateTime)datetime).ToString("MM/dd/yyyy");
        }

        public static string ToFormatMMDDYYYYhhmmtt(DateTime? datetime)
        {
            if(datetime == null) return string.Empty;
            return ((DateTime)datetime).ToString("MM/dd/yyyy hh:mm tt");
        }

        public static string GetTimeSpan_PrettyDate(DateTime? startDate, bool showTimeTakenFormat=true)
        {
            if (startDate == null) return string.Empty;
            return GetPrettyDate(new TimeSpan(DateTime.Now.Ticks - ((DateTime)startDate).Ticks), showTimeTakenFormat);
        }

        public static string GetTimeSpan_PrettyDate(DateTime? startDate, DateTime? endDate, bool showTimeTakenFormat = true)
        {
            if (startDate == null || endDate == null) return string.Empty;
            return GetPrettyDate(new TimeSpan(((DateTime)endDate).Ticks - ((DateTime)startDate).Ticks), showTimeTakenFormat);
        }
        public static string GetPrettyDate(TimeSpan timeSpan, bool showTimeTakenFormat = true)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = timeSpan;
            double delta = Math.Abs(ts.TotalSeconds);

            var ago = showTimeTakenFormat ? string.Empty: " ago";

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? $"one second{ago}" : ts.Seconds + $" seconds{ago}";

            if (delta < 2 * MINUTE)
                return $"a minute{ago}";

            if (delta < 45 * MINUTE)
                return ts.Minutes + $" minutes{ago}";

            if (delta < 90 * MINUTE)
                return $"an hour{ago}";

            if (delta < 24 * HOUR)
                return ts.Hours + $" hours{ago}";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + $" days{ago}";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? $"one month{ago}" : months + $" months{ago}";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? $"one year{ago}" : years + $" years{ago}";
            }
        }

    }
}
