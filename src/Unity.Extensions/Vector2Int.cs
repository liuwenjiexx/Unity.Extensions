using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LWJ.Unity
{
    public static partial class Extensions
    {
        public static Vector2Int Rotate(Vector2Int dir, float angle)
        {
            var p1 = Quaternion.AngleAxis(angle, Vector3.back) * (Vector2)dir;
            Vector2Int p = new Vector2Int((int)p1.x, (int)p1.y);
            return p;
        }
        public static bool IsInPolygon(this Vector2Int testPoint, Vector2Int[] polygon)
        {
            bool result = false;
            int j = polygon.Length - 1;
            Vector2Int start, end;
            for (int i = 0, len = polygon.Length; i < len; i++)
            {
                start = polygon[j];
                end = polygon[i];
                if (end.y < testPoint.y && start.y >= testPoint.y || start.y < testPoint.y && end.y >= testPoint.y)
                {
                    if (end.x + (testPoint.y - end.y) / ((float)(start.y - end.y)) * (start.x - end.x) < testPoint.x)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

    }
}