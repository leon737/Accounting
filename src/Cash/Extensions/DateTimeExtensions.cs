using System;

namespace Cash.Web.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FromUnixTime(this DateTime dateTime, long unixTimeStamp)
        {
            // Unix timestamp is milliseconds past epoch
            var date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            date = date.AddMilliseconds(unixTimeStamp);
            return date;
        }
    }
}