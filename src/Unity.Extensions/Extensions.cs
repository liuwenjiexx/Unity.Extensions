using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LWJ.Unity
{
    public static partial class Extensions
    {
        public static readonly GameObject[] EmptyGameObjectArray = new GameObject[0];
        public static readonly Transform[] EmptyTransformArray = new Transform[0];

        static float dpToPixel;
        static float pixelToDp;


        public static float DpToPixel
        {
            get
            {
                if (dpToPixel == 0)
                {
                    InitDP();
                }
                return dpToPixel;
            }
        }

        public static float PixelToDp
        {
            get
            {
                if (dpToPixel == 0)
                {
                    InitDP();
                }
                return pixelToDp;
            }
        }

        private static void InitDP()
        {
            float dpi = Screen.dpi;
            if (dpi <= 0)
                dpi = 72;
            dpToPixel = dpi / 160f;
            pixelToDp = 1f / dpToPixel;
        }


    }
}
