using UnityEngine;
using UnityEngine.UI;

<<<<<<< HEAD:src/UnityEngine.Extensions/UI/Image.cs
namespace UnityEngine.Extensions
=======
namespace Core.Unity
>>>>>>> e2db7b97206b38fd85e98182bacbdb5df502aa77:src/Unity.Extensions/UI/Image.cs
{
    public static partial class UnityExtensions
    {

        public static bool ScreenPointToPixelPoint(this Image image, Vector2 screenPoint, out Vector2Int pixelPoint)
        {
            RectTransform trans = image.rectTransform;
            int pixelWidth = image.mainTexture.width;
            int pixelHeight = image.mainTexture.height;
            return ScreenPointToPixelPoint(trans, pixelWidth, pixelHeight, screenPoint, out pixelPoint);
        }
    }
}
