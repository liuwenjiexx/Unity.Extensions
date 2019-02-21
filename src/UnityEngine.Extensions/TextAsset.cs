using System.Xml;
using UnityEngine;

namespace UnityEngine.Extensions
{

    public static partial class UnityExtensions
    {
        public static XmlDocument AsXml(this TextAsset asset)
        {
            byte[] data = asset.bytes;
            if (data == null)
                return null;

            XmlDocument doc = new XmlDocument();
            doc.Load(new System.IO.MemoryStream(data, false));
            return doc;
        }

    }

}
