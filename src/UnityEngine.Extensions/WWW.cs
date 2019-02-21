using System.Xml;
using UnityEngine;

<<<<<<< HEAD:src/UnityEngine.Extensions/WWW.cs
namespace UnityEngine.Extensions
=======
namespace Core.Unity
>>>>>>> e2db7b97206b38fd85e98182bacbdb5df502aa77:src/Unity.Extensions/WWW.cs
{
    public static partial class UnityExtensions
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
