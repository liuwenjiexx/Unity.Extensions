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
       
    }
}