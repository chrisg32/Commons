using System;
using System.Linq;

namespace CG.Commons.Extensions
{
    public static class DateTimeExtensions
    {
        #region DateTime

        /// <summary>
        /// Output a string representation of a DateTime object that can be used in a SQL where clause. Does not include time.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToSqlFormatString(this DateTime date)
        {
            //TODO add time
            return string.Format("to_date('{0:MM/dd/yyyy}','MM/dd/yyyy')", date);
        }

        public static DateTime ToTimeZone(this DateTime utcTime, TimeZoneInfo timezone)
        {
            return timezone == null ? utcTime.SetKindToUtc() : TimeZoneInfo.ConvertTime(utcTime.SetKindToUtc(), timezone);
        }

        public static DateTime ToTimeZone(this DateTime utcTime, string timezone, TimeZoneInfo defaultTimezone = null)
        {
            if (string.IsNullOrWhiteSpace(timezone))
            {
                if (defaultTimezone == null)
                {
                    return utcTime.SetKindToUtc();
                }
                return utcTime.ToTimeZone(defaultTimezone);
            }
            var tz = TimeZoneInfo.GetSystemTimeZones().SingleOrDefault(t => t.StandardName == timezone);
            return utcTime.ToTimeZone(tz);
        }

        public static DateTime FromTimeZone(this DateTime datetime, string timezone, TimeZoneInfo defaultTimezone = null)
        {
            if (string.IsNullOrWhiteSpace(timezone))
            {
                if (defaultTimezone == null)
                {
                    return datetime.SetKindToUtc();
                }
                return datetime.FromTimeZone(defaultTimezone);
            }
            var tz = TimeZoneInfo.GetSystemTimeZones().SingleOrDefault(t => t.StandardName == timezone);
            return datetime.FromTimeZone(tz);
        }

        public static DateTime FromTimeZone(this DateTime datetime, TimeZoneInfo timezone)
        {
            return timezone == null ? datetime.SetKindToUtc() : TimeZoneInfo.ConvertTime(datetime, TimeZoneInfo.Utc);
        }

        public static DateTime SetKindToUtc(this DateTime dateTime)
        {
            return new DateTime(dateTime.Ticks, DateTimeKind.Utc);
        }

        public static DateTime SetKindToUnspecified(this DateTime dateTime)
        {
            return new DateTime(dateTime.Ticks, DateTimeKind.Unspecified);
        }

        public static DateTime TruncateMilliseconds(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, 0);
        }

        #endregion DateTime

        #region Nullable<DateTime>

        public static DateTime? ToTimeZone(this DateTime? utcTime, TimeZoneInfo timezone)
        {
            if (utcTime == null) return null;
            return timezone == null ? utcTime.SetKindToUtc() : TimeZoneInfo.ConvertTime(utcTime.Value.SetKindToUtc(), timezone);
        }

        public static DateTime? ToTimeZone(this DateTime? utcTime, string timezone)
        {
            if (utcTime == null) return null;
            if (string.IsNullOrWhiteSpace(timezone)) return utcTime.SetKindToUtc();
            var tz = TimeZoneInfo.GetSystemTimeZones().SingleOrDefault(t => t.StandardName == timezone);
            return utcTime.ToTimeZone(tz);
        }

        public static DateTime? SetKindToUtc(this DateTime? dateTime)
        {
            if (dateTime == null) return null;
            return new DateTime(dateTime.Value.Ticks, DateTimeKind.Utc);
        }

        #endregion Nullable<DateTime>

        #region DateTimeOffset

        public static DateTimeOffset TruncateMilliseconds(this DateTimeOffset dateTimeOffset)
        {
            return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second, 0, dateTimeOffset.Offset);
        }

        #endregion
    }
}
