using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System.Extensions
{
    public static partial class Extension
    {
        /// <summary>
        /// x => [a1, a2] => [b1, b2]
        /// </summary>
        public static float Remap(this float x, float a1, float a2, float b1, float b2)
        {
            float t = (x - a1) / (a2 - a1);
            if (t < 0f)
                t = 0f;
            if (t > 1f)
                t = 1f;
            float value = b1 + t * (b2 - b1);
            return value;
        }

        public static float RemapUnclamped(this float x, float a1, float a2, float b1, float b2)
        {
            float value = b1 + (x - a1) / (a2 - a1) * (b2 - b1);
            return value;
        }

    }

}
