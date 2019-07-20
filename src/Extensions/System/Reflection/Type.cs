using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Reflection.Extensions
{
    public static partial class Extension
    {
        /// <summary>
        /// <see cref="Array.SetValue(object, int)"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MethodInfo GetArraySetValueMethod(this Type type)
        { 
            return type.GetMethod("SetValue", new Type[] { typeof(object), typeof(int) });
        }

        /// <summary>
        /// <see cref="Array.GetValue(int)"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MethodInfo GetArrayGetValueMethod(this Type type)
        {
            return type.GetMethod("GetValue", new Type[] { typeof(int) });
        }


        public static MemberInfo GetPropertyOrField(this Type type, string propertyOrFieldName, BindingFlags bindingFlags)
        {
            MemberInfo member;
            if (!string.IsNullOrEmpty(propertyOrFieldName))
            {
                member = type.GetProperty(propertyOrFieldName, bindingFlags);
                if (member == null)
                    member = type.GetField(propertyOrFieldName, bindingFlags);
            }
            else
            {
                member = null;
            }
            return member;
        }

        public static MemberInfo GetPropertyOrField(this Type type, string propertyOrFieldName)
        {
            MemberInfo member;
            if (!string.IsNullOrEmpty(propertyOrFieldName))
            {
                member = type.GetProperty(propertyOrFieldName);
                if (member == null)
                    member = type.GetField(propertyOrFieldName);
            }
            else
            {
                member = null;
            }
            return member;
        }


    }
}
