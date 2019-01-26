using System.Xml;
using UnityEngine;

namespace Core.Unity
{

    public static partial class Extensions
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
