using System.Xml;
using UnityEngine;

namespace LWJ.Unity
{
    public static partial class Extensions
    {
        public static XmlDocument AsXml(this WWW www)
        {
            if (www.error != null)
            {
                Debug.LogError(www.error);
                return null;
            }
            byte[] data = www.bytes;
            if (data == null)
                return null;

            XmlDocument doc = new XmlDocument();
            doc.Load(new System.IO.MemoryStream(data, false));
            return doc;
        }
    }
}
