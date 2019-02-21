using UnityEngine;

<<<<<<< HEAD:src/UnityEngine.Extensions/Color32.cs
namespace UnityEngine.Extensions
=======
namespace Core.Unity
>>>>>>> e2db7b97206b38fd85e98182bacbdb5df502aa77:src/Unity.Extensions/Color32.cs
{
    public static partial class UnityExtensions
    {
        public static Color ToColor(this Color32 color)
        {
            return new Color(color.r / 255f, color.g / 255f, color.b / 255f, color.a / 255f);
        }

        #region ToString

        public static string ToStringHexRgba(this Color32 color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", color.r, color.g, color.b, color.a);
        }
        public static string ToStringHexRgb(this Color32 color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", color.r, color.g, color.b);
        }
       
        #endregion
    }
}
