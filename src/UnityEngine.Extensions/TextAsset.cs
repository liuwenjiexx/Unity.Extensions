using System.Xml;
using UnityEngine;

<<<<<<< HEAD:src/UnityEngine.Extensions/TextAsset.cs
namespace UnityEngine.Extensions
=======
namespace Core.Unity
>>>>>>> e2db7b97206b38fd85e98182bacbdb5df502aa77:src/Unity.Extensions/TextAsset.cs
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
