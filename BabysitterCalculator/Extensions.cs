using System;

namespace BabysitterCalculator
{
    public static class Extensions
    {
        public static DateTime FloorHour(this DateTime dt)
        {
            if (dt.Minute != 0 || dt.Second != 0)
                dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0, dt.Kind);
            return dt;
        }

        public static DateTime CeilHour(this DateTime dt)
        {
            if (dt.Minute != 0 || dt.Second != 0)
                dt = dt.FloorHour().AddHours(1);
            return dt;
        }

        public static DateTime GetMidnightForShift(this DateTime dt)
        {
            if (dt.TimeOfDay.Hours <= 4)
                return dt.Date;
            if (dt.TimeOfDay.Hours >= 17)
                return dt.Date.AddDays(1);
            throw new ArgumentException("DateTime must be inside a valid shift", "dt");
        }
    }
}
