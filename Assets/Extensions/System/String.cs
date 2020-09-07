using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Extensions
{
    public static partial class Extension
    {
        public static string LastSubstring(this string self, int lastStartIndex, int length)
        {
            int len = self.Length;

            if (lastStartIndex < 0 || lastStartIndex >= len)
                throw new ArgumentOutOfRangeException("lastStartIndex");

            if (lastStartIndex + length > len)
                throw new ArgumentOutOfRangeException("length");

            int startIndex = len - (lastStartIndex + length);
            string result = self.Substring(startIndex, length);
            return result;
        }

        public static string OverflowLeft(this string self, int totalLength, string padString)
        {
            return Overflow(self, totalLength, padString, true);
        }

        public static string OverflowRight(this string self, int totalLength, string padString)
        {
            return Overflow(self, totalLength, padString, false);
        }

        static string Overflow(string self, int totalLength, string padString, bool isLeft)
        {
            int len = self.Length;
            if (len <= totalLength)
                return self;

            int len2;
            int padLength = padString.Length;
            len2 = totalLength - padLength;

            if (isLeft)
                return padString + self.LastSubstring(0, len2);
            else
                return self.Substring(0, len2) + padString;
        } //[InitializeOnLoadMethod]
        //public static void TestFormatString()
        //{
        //    string str;
        //    str = "{$a} + {$b} = {$result:0.##}{{$a}{{$s}}{abc}";
        //    Dictionary<string, object> values;

        //    float f = 1.2345f;

        //    values = new Dictionary<string, object>();
        //    values["a"] = "1";
        //    values["b"] = "0.23";
        //    values["result"] = f;

        //    Debug.Log("out :" + FormatString(str, values));
        //}


        private static Regex formatStringRegex = new Regex("(?<!\\{)\\{\\$([^}:]*)(:([^}]*))?\\}(?!\\})");

        public static string FormatString(this string input, IDictionary<string, object> values)
        {
            return FormatString(input, null, values);
        }
        /// <summary>
        /// format:{$name:format} 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string FormatString(this string input, IFormatProvider formatProvider, IDictionary<string, object> values)
        {
            string result;

            result = formatStringRegex.Replace(input, (m) =>
            {
                string paramName = m.Groups[1].Value;
                string format = m.Groups[3].Value;
                object value;
                string ret = null;

                if (string.IsNullOrEmpty(paramName))
                    throw new FormatException("format error:" + m.Value);

                if (!values.TryGetValue(paramName, out value))
                    throw new ArgumentException("not found param name:" + paramName);

                if (value != null)
                {
                    ret = string.Format(formatProvider, "{0:" + format + "}", value);
                    //if (format.Length > 0)
                    //{
                    //    IFormattable formattable = value as IFormattable;
                    //    if (formattable != null)
                    //    {
                    //        ret = formattable.ToString(format, null);
                    //    }
                    //}
                    //if (ret == null)
                    //    ret = value.ToString();
                }
                else
                {
                    ret = string.Empty;
                }

                return ret;
            });
            return result;
        }
    }
}
