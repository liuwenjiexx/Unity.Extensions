using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Reflection.Extensions
{
    public static partial class Extension
    {
        public static T GetCustomAttribute<T>(this ICustomAttributeProvider member, bool inherit)
            where T : Attribute
        {
            var attrs = member.GetCustomAttributes(typeof(T), inherit);
            if (attrs != null && attrs.Length > 0)
                return (T)attrs[0];
            return null;
        }

        public static T[] GetCustomAttributes<T>(this ICustomAttributeProvider member, bool inherit)
            where T : Attribute
        {
            var attrs = member.GetCustomAttributes(typeof(T), inherit);
            if (attrs != null && attrs.Length > 0)
            {
                T[] result = new T[attrs.Length];
                for (int i = 0; i < attrs.Length; i++)
                    result[i] = (T)attrs[i];
                return result;
            }
            return new T[0];
        }

        public static bool IsDefined<T>(this ICustomAttributeProvider member, bool inherit)
            where T : Attribute
        {
            return member.IsDefined(typeof(T), inherit);
        }
    }
}
