using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace UnityEditor.Extensions
{
    public static partial class UnityEditorExtensions
    {
        public static object GetObjectOfProperty(this SerializedProperty prop)
        {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }
            return obj;
        }
        private static object GetValue_Imp(object source, string name, int index)
        {
            var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
            if (enumerable == null) return null;
            var enm = enumerable.GetEnumerator();
            //while (index-- >= 0)
            //    enm.MoveNext();
            //return enm.Current;

            for (int i = 0; i <= index; i++)
            {
                if (!enm.MoveNext()) return null;
            }
            return enm.Current;
        }
        private static object GetValue_Imp(object source, string name)
        {
            if (source == null)
                return null;
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f.GetValue(source);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p.GetValue(source, null);

                type = type.BaseType;
            }
            return null;
        }
        private static MemberInfo GetValue_ImpMember(object source, string name)
        {
            if (source == null)
                return null;
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f;

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p;

                type = type.BaseType;
            }
            return null;
        }
        public static void SetObjectOfProperty(this SerializedProperty prop, object value)
        {
            var owner = GetObjectOfPropertyOwner(prop);
            if (owner == null)
                return;
            var member = GetValue_ImpMember(owner, prop.name);
            if (member is FieldInfo)
                ((FieldInfo)member).SetValue(owner, value);
            else
                ((PropertyInfo)member).SetValue(owner, value, null);
            //var path = prop.propertyPath.Replace(".Array.data[", "[");
            //object obj = prop.serializedObject.targetObject;
            //var elements = path.Split('.');
            //for (int i = 0; i < elements.Length; i++)
            //{
            //    string element = elements[i];
            //    if (element.Contains("["))
            //    {
            //        var elementName = element.Substring(0, element.IndexOf("["));
            //        var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));

            //        obj = GetValue_Imp(obj, elementName, index);

            //    }
            //    else
            //    {
            //        if (i == elements.Length - 1)
            //        {
            //            var member = GetValue_ImpMember(obj, element);
            //            if (member is FieldInfo)
            //                ((FieldInfo)member).SetValue(obj, value);
            //            else
            //                ((PropertyInfo)member).SetValue(obj, value, null);
            //        }
            //        else
            //            obj = GetValue_Imp(obj, element);
            //    }
            //}
            //return obj;
        }

        public static object GetObjectOfPropertyOwner(this SerializedProperty prop)
        {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            for (int i = 0, len = elements.Length - 1; i < len; i++)
            {

                string element = elements[i];
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }
            return obj;
        }
        public static GUIContent LabelContent(this SerializedProperty property)
        {
            return new GUIContent(property.displayName, property.tooltip);
        }
    }
}
