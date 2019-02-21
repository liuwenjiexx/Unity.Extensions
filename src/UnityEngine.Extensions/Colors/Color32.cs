using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


<<<<<<< HEAD:src/UnityEngine.Extensions/Colors/Color32.cs
namespace UnityEngine.Extensions.Colors
=======
namespace Core.Unity.Extension.Colors
>>>>>>> e2db7b97206b38fd85e98182bacbdb5df502aa77:src/Unity.Extensions/Colors/Color32.cs
{

    public static partial class Extensions
    {

        public static bool TryParseColor32(this string colorString, out Color32 color)
        {
            color = new Color32();
            colorString = colorString.Trim();

            if (colorString.StartsWith("#"))
            {
                int n;

                for (int i = 0; i < 4; i++)
                {
                    try
                    {
                        if (i * 2 + 1 > colorString.Length - 2)
                        {
                            if (i == 3)
                            {
                                n = 0xff;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            n = int.Parse(colorString.Substring(i * 2 + 1, 2), System.Globalization.NumberStyles.HexNumber);
                        }
                        switch (i)
                        {
                            case 0:
                                color.r = (byte)n;
                                break;
                            case 1:
                                color.g = (byte)n;
                                break;
                            case 2:
                                color.b = (byte)n;
                                break;
                            case 3:
                                color.a = (byte)n;

                                return true;
                        }
                    }
                    catch
                    {
                        return false;
                    }

                }
            }
            else if (colorString.StartsWith("hsv(", StringComparison.InvariantCultureIgnoreCase) && colorString.EndsWith(")"))
            {
                int i = 0;
                int n;
                float h=0, s=0, v=0;
                foreach (var part in colorString.Substring(4, colorString.Length - 5).Split(','))
                {
                   if(!int.TryParse(part,out n))
                    {
                        return false;
                    }
                    switch (i++)
                    {
                        case 0:
                            h=n / 359f;
                            break;
                        case 1:
                            s = n / 255f;
                            break;
                        case 2:
                            v = n / 255f;
                            break;
                    }
                }
                color= Color.HSVToRGB(h, s, v);
                return true;
            }
            return false;
        }
    }

}