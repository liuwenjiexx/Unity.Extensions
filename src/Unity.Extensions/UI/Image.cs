using UnityEngine;
using UnityEngine.UI;

namespace LWJ.Unity
{
    public static partial class Extensions
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
