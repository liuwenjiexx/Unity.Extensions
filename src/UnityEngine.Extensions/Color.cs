using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Extensions
{
    public static partial class UnityExtensions
    {
        public static void Fill(this Color[] colors, Color color)
        {
            for (int i = 0, len = colors.Length; i < len; i++)
                colors[i] = color;
        }

        /// <summary>
        /// 灰阶 Y-Greyscale (YIQ/NTSC), (0.299*r+0.587*g+0.114*b)
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static float GetGrayLevel(this Color color)
        {
            return (0.299f * color.r + 0.587f * color.g + 0.114f * color.b);
        }

        /// <summary>
        /// 灰阶
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static float GetGrayLevel(this Color32 color)
        {
            return (0.299f * color.r + 0.587f * color.g + 0.114f * color.b) / 255f;
        }

        #region 灰阶算法



        /// <summary>
        /// 灰阶, (max(r,g,b)+min(r,g,b))/2
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static float GetGrayLevelLightness(this Color color)
        {
            return (Mathf.Max(color.r, color.g, color.b) + Mathf.Min(color.r, color.g, color.b)) * 0.5f;
        }
        /// <summary>
        /// 灰阶, (r+g+b)/3
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static float GetGrayLevelAverage(this Color color)
        {
            return (color.r + color.g + color.b) / 3f;
        }
        /// <summary>
        /// 灰阶 Luminosity , (0.21*r+0.72*g+0.07*b)
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static float GetGrayLevelLuminosity(this Color color)
        {
            return (0.21f * color.r + 0.72f * color.g + 0.07f * color.b);
        }

        /// <summary>
        /// 灰阶 Y-Greyscale (YIQ/NTSC), (0.299*r+0.587*g+0.114*b)
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static float GetGrayLevelY(this Color color)
        {
            return (0.299f * color.r + 0.587f * color.g + 0.114f * color.b);
        }

        /// <summary>
        /// 灰阶 BT709, (0.2125*r+0.7154*g+0.0721*b)
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static float GetGrayLevelBT709(this Color color)
        {
            return (0.2125f * color.r + 0.7154f * color.g + 0.0721f * color.b);
        }

        /// <summary>
        /// 灰阶 RMY (YIQ/NTSC), (0.5*r+0.419*g+0.081*b)
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static float GetGrayLevelRMY(this Color color)
        {
            return (0.5f * color.r + 0.419f * color.g + 0.081f * color.b);
        }


        #endregion

        #region ToString

        public static string ToStringHexRgba(this Color color)
        {
            return ((Color32)color).ToStringHexRgba();
        }
        public static string ToStringHexRgb(this Color color)
        {
            return ((Color32)color).ToStringHexRgb();
        }
        public static string ToStringHsvRgb(this Color color)
        {
            float h, s, v;
            Color.RGBToHSV(color, out h, out s, out v);
            return string.Format("hsv({0},{1},{2})", (int)(h * 359), (int)(s * 255), (int)(v * 255));
        }



        #endregion
    }
}