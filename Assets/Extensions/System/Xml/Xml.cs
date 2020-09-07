using System;
using System.Collections.Generic;
using System.Extensions;
using System.Linq;
using System.Text;

namespace System.Xml.Extensions
{
    public static partial class Extension
    {
        public static T Read<T>(this XmlNode elem, string path)
        {
            return Read<T>(elem, path, default(T));
        }

        public static T Read<T>(this XmlNode elem, string path, T defaultValue)
        {
            if (elem == null)
                return defaultValue;

            XmlNode node = elem.SelectSingleNode(path);
            if (node == null)
                return defaultValue;

            string str = node.InnerText;

            if (string.IsNullOrEmpty(str))
                return defaultValue;

            object result = null;
            result = Convert.ChangeType(str, typeof(T));

            return (T)result;
        }
        public static void SetAttributeValue(this XmlNode elem, string attrName, object value)
        {
            XmlAttribute attr = elem.Attributes[attrName];
            if (attr == null)
            {
                attr = elem.OwnerDocument.CreateAttribute(attrName);
                elem.Attributes.Append(attr);
            }
            attr.Value = value.ToStringOrEmpty();
        }
        public static T GetAttributeValue<T>(this XmlNode elem, string attrName)
        {
            return GetAttributeValue<T>(elem, attrName, default(T));
        }

        public static T GetAttributeValue<T>(this XmlNode elem, string attrName, T defaultValue)
        {
            if (elem == null)
                return defaultValue;
            XmlAttribute attr = elem.Attributes[attrName];
            if (attr == null)
                return defaultValue;

            string str = attr.Value;

            if (string.IsNullOrEmpty(str))
                return defaultValue;

            object result = null;
            result = Convert.ChangeType(str, typeof(T));
            return (T)result;
        }
        public static bool HasAttribute(this XmlNode elem, string attrName)
        {
            if (elem == null)
                return false;
            XmlAttribute attr = elem.Attributes[attrName];
            if (attr == null)
                return false;
            return true;
        }

        public static T GetNodeValue<T>(this XmlNode elem, string name, T defaultValue)
        {
            if (elem == null)
                return defaultValue;

            XmlNode node = elem.SelectSingleNode(name);
            if (node == null)
                return defaultValue;

            string str = node.InnerText;

            if (string.IsNullOrEmpty(str))
                return defaultValue;

            object result = null;
            result = Convert.ChangeType(str, typeof(T));

            return (T)result;
        }

        public static XmlNode SetNodeValue(this XmlNode elem, string name, object value)
        {
            var node = elem.OwnerDocument.CreateElement(name);
            node.InnerText = value.ToStringOrEmpty();
            elem.AppendChild(node);
            return node;
        }


    }

}
