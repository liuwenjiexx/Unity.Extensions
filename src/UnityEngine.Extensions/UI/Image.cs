using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.Extensions
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
