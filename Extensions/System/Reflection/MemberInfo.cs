using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Reflection.Extensions
{
    public static partial class Extension
    {


        #region MemberInfo

        public static object GetValue(this MemberInfo member, object obj)
        {
            var pInfo = member as PropertyInfo;
            if (pInfo != null)
                return pInfo.GetValueUnity(obj, null);
            var fInfo = member as FieldInfo;
            if (fInfo != null)
                return fInfo.GetValue(obj);
            throw new Exception("member not property or field member");
        }

        public static void SetValue(this MemberInfo member, object obj, object value)
        {
            var pInfo = member as PropertyInfo;
            if (pInfo != null)
            {
                pInfo.SetValueUnity(obj, value, null);
                return;
            }

            var fInfo = member as FieldInfo;
            if (fInfo != null)
            {
                fInfo.SetValue(obj, value);
                return;
            }
            throw new Exception("member not property or field member");
        }

        #endregion


        #region PropertyInfo


        /// <summary>
        /// IOS <see cref="PropertyInfo.GetSetMethod"/>
        /// </summary>        　
        public static void SetValueUnity(this PropertyInfo source, object obj, object value, object[] index)
        {
            source.GetSetMethod(true).Invoke(obj, new object[] { value });
        }

        /// <summary>
        /// IOS <see cref="PropertyInfo.GetGetMethod"/>
        /// </summary>         
        public static object GetValueUnity(this PropertyInfo source, object obj, object[] index)
        {
            object value;
            value = source.GetGetMethod(true).Invoke(obj, null);
            return value;
        }
        public static bool IsSetPublic(this PropertyInfo propertyInfo)
        {
            var setter = propertyInfo.GetSetMethod();
            if (setter != null)
                return setter.IsPublic;
            return false;
        }

        public static bool IsGetPublic(this PropertyInfo propertyInfo)
        {
            var getter = propertyInfo.GetGetMethod();
            if (getter != null)
                return getter.IsPublic;
            return false;
        }


        #endregion
    }
}
