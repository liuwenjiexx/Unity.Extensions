using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Extensions
{
    public static partial class Extension
    {
        public static string ToStringOrEmpty(this object source)
        {
            string resullt;

            if (source != null)
            {
                resullt = source.ToString();
                if (resullt == null)
                    resullt = string.Empty;
            }
            else
                resullt = string.Empty;


            return resullt;
        }



        public static string ToStringOrDefault(this object source)
        {
            if (source == null)
                return null;
            return source.ToString();
        }

        public static string ToStringOrDefault(this object source, string defaultValue)
        {
            string result = ToStringOrDefault(source);

            if (string.IsNullOrEmpty(result))
                result = defaultValue;

            return result;
        }


    }
}
