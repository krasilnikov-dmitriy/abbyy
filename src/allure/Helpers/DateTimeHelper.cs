using System;
namespace AllureNUnitAdapter.Helpers
{
    public static class DateTimeHelper
    {
        public static long ToUnixTimestamp(this DateTime time)
        {
            return (long)(time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
        }
    }
}

