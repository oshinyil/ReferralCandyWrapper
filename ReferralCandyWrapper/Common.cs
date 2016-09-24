using System;

namespace ReferralCandyWrapper
{
    internal static class Common
    {
        public static int GetUnixTimestamp()
        {
            return ToUnixTimestamp(DateTime.UtcNow);
        }

        public static int GetUnixTimestamp(DateTime dateTime)
        {
            return ToUnixTimestamp(dateTime.ToUniversalTime());
        }

        private static int ToUnixTimestamp(DateTime dateTime)
        {
            return (int)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
