using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Extensions
{
    public static partial class Extension
    {
        private static readonly DateTime UtcInitializationTime = new DateTime(1970, 1, 1, 0, 0, 0);



        /// <summary>
        /// 对应java的 System.currentTimeMillis()
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ToUtcMilliseconds(this DateTime dt)
        {
            dt = dt.ToUniversalTime();
            return (long)dt.Subtract(UtcInitializationTime).TotalMilliseconds;
        }

        public static DateTime FromUtcMilliseconds(this DateTime dt, long millis)
        {
            return UtcInitializationTime.Add(TimeSpan.FromMilliseconds(millis));
        }


        public static long ToUtcSeconds(this DateTime dt)
        {
            dt = dt.ToUniversalTime();
            return (long)dt.Subtract(UtcInitializationTime).TotalSeconds;
        }
        public static DateTime FromUtcSeconds(this DateTime dt, long seconds)
        {
            return UtcInitializationTime.Add(TimeSpan.FromSeconds(seconds));
        }


    }
}
