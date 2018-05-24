using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;


namespace LWJ.Unity
{
    public static partial class Extensions
    {

        /// <summary>
        /// Center Point
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static Vector3 Center(this Vector3 point1, Vector3 point2)
        {
            return new Vector3((point1.x + point2.x) * 0.5f, (point1.y + point2.y) * 0.5f, (point1.z + point2.z) * 0.5f);
        }

        /// <summary>
        /// <seealso cref="Vector3.SqrMagnitude(Vector3)"/>
        /// </summary> 
        public static float SqrDistance(this Vector3 point1, Vector3 point2)
        {
            return Vector3.SqrMagnitude(point1 - point2);
        }


        /// <summary>
        /// cliamp <paramref name="min"/> &lt;= <paramref name="source"/> &lt;= <paramref name="max"/>
        /// </summary>        
        public static Vector3 Clamp(this Vector3 source, Vector3 min, Vector3 max)
        {
            return Vector3.Max(Vector3.Min(source, max), min);
        }

        public static Vector3 Clamp(this Vector3 source, Bounds bounds)
        {
            return Vector3.Max(Vector3.Min(source, bounds.max), bounds.min);
        }

        /// <summary>
        /// [x,y,z] <see cref="float.IsNaN(float)"/>
        /// </summary> 
        /// <returns></returns>
        public static bool IsNaN(this Vector3 source)
        {
            return float.IsNaN(source.x * source.y * source.z);
        }

        /// <summary>
        /// [ x,y,z ] <see cref="Mathf.Abs(float)"/>
        /// </summary> 
        public static Vector3 Abs(this Vector3 source)
        {
            return new Vector3(Mathf.Abs(source.x), Mathf.Abs(source.y), Mathf.Abs(source.z));
        }


        public static float AngleAroundAxis(this Vector3 onNormal, Vector3 dirA, Vector3 dirB)
        {
            dirA = dirA - Vector3.Project(dirA, onNormal);
            dirB = dirB - Vector3.Project(dirB, onNormal);
            float angle = Vector3.Angle(dirA, dirB);
            return angle * (Vector3.Dot(onNormal, Vector3.Cross(dirA, dirB)) < 0 ? -1 : 1);
        }

        /// <summary>
        /// point - project point
        /// </summary> 
        public static Vector3 ProjectOntoPlane(this Vector3 planeNormal, Vector3 point)
        {
            return point - Vector3.Project(point, planeNormal);
        }

        #region Get Normal 


        /// <summary>
        /// 3 point to plane normal
        /// </summary>
        public static Vector3 PlaneNormal(this Vector3 point1, Vector3 point2, Vector3 point3)
        {
            return Vector3.Cross((point3 - point1), (point2 - point1));
        }

        #endregion


        public static Vector3 ToLineCrossPoint(this Vector3 source, Vector3 lineStart, Vector3 lineEnd)
        {
            Vector3 dir = lineEnd - lineStart;
            Vector3 w = source - lineStart;

            float b = Vector3.Dot(w, dir) / Vector3.Dot(dir, dir);

            return lineStart + b * dir;
        }


        public static Vector3 ToSegmentCrossPoint(this Vector3 source, Vector3 lineStart, Vector3 lineEnd)
        {
            Vector3 v = lineEnd - lineStart;
            Vector3 w = source - lineStart;

            float c1 = Vector3.Dot(w, v);
            if (c1 <= 0)
                return lineStart;
            float c2 = Vector3.Dot(v, v);

            if (c2 <= c1)
                return lineEnd;

            return lineStart + (c1 / c2) * v;
        }

        public static bool ToSegmentCrossPoint(this Vector3 source, Vector3 lineStart, Vector3 lineEnd, out Vector3 crossPoint)
        {
            Vector3 v = lineEnd - lineStart;
            Vector3 w = source - lineStart;

            float c1 = Vector3.Dot(w, v);
            if (c1 <= 0)
            {
                crossPoint = Vector3.zero;
                return false;
            }
            float c2 = Vector3.Dot(v, v);

            if (c2 <= c1)
            {
                crossPoint = Vector3.zero;
                return false;
            }
            crossPoint = lineStart + c1 / c2 * v;
            return true;
        }

        public static float GetLineDistance(this Vector3 source, Vector3 lineStart, Vector3 lineEnd)
        {
            return Vector3.Distance(source, ToLineCrossPoint(source, lineStart, lineEnd));
        }
        public static float GetLineSqrDistance(this Vector3 source, Vector3 lineStart, Vector3 lineEnd)
        {
            return Vector3.SqrMagnitude(source - ToLineCrossPoint(source, lineStart, lineEnd));
        }

        public static float GetSegmentDistance(this Vector3 source, Vector3 lineStart, Vector3 lineEnd)
        {
            return Vector3.Distance(source, ToSegmentCrossPoint(source, lineStart, lineEnd));
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>true: in line ,false:out line</returns>
        public static bool GetSegmentDistance(this Vector3 source, Vector3 lineStart, Vector3 lineEnd, out float distance)
        {
            Vector3 point;
            if (ToSegmentCrossPoint(source, lineStart, lineEnd, out point))
            {
                distance = Vector3.Distance(source, point);
                return true;
            }
            distance = 0;
            return false;
        }

        #region IEnumerable<Vector3>

        /// <summary>
        /// 计算所有点的中心点
        ///<para>calculate all point of center point</para>
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Vector3 GetCenter(this IEnumerable<Vector3> points)
        {
            return GetCenter(points, Vector3.zero);
        }

        /// <summary>
        /// 计算所有点的中心点
        ///<para>calculate all point of center point</para>
        /// </summary>
        /// <param name="points"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns> 
        public static Vector3 GetCenter(this IEnumerable<Vector3> points, Vector3 defaultValue)
        {
            Vector3 pos = new Vector3();

            int count = 0;
            foreach (Vector3 v in points)
            {
                if (count == 0)
                    pos = v;
                else
                    pos += v;
                count++;
            }

            if (count == 0)
                return defaultValue;

            pos /= count;
            return pos;

        }

        /// <summary>
        /// 计算所有点的总距离 <see cref="Vector3.Distance(Vector3, Vector3)"/>
        ///<para>calculate all points of total distance <see cref="Vector3.Distance(Vector3, Vector3)"/></para>
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static float GetDistance(this IEnumerable<Vector3> points)
        {
            float dist = 0;
            Vector3 point1 = Vector3.zero;
            bool first = true;
            foreach (Vector3 point2 in points)
            {
                if (first)
                {
                    point1 = point2;
                    first = false;
                }
                else
                {
                    dist += Vector3.Distance(point2, point1);
                    point1 = point2;
                }
            }

            return dist;
        }



        public static Bounds GetBounds(this IEnumerable<Vector3> points)
        {
            return GetBounds(points, Vector3.zero);
        }
        /// <summary>
        /// calculate all points <see cref="Bounds"/>
        /// </summary> 
        /// <returns>if <paramref name="points"/> null or empty the return Bounds(<paramref name="defualtCenter"/>,<see cref="Vector3.zero"/>)</returns>
        public static Bounds GetBounds(this IEnumerable<Vector3> points, Vector3 defualtCenter)
        {
            if (points == null)
                return new Bounds(defualtCenter, Vector3.zero);

            Vector3 min = Vector3.one * float.MaxValue, max = Vector3.one * float.MinValue;
            int n = 0;
            foreach (var p in points)
            {
                min = Vector3.Min(min, p);
                max = Vector3.Max(max, p);
                n++;
            }
            if (n == 0)
                return new Bounds(defualtCenter, Vector3.zero);

            Bounds b = new Bounds();
            b.SetMinMax(min, max);

            return b;
        }

        public static bool GetBounds(IEnumerable<Vector3> points, out Bounds bounds)
        {
            float xMin = 0, xMax = 0, yMin = 0, yMax = 0, zMin = 0, zMax = 0;
            bool first = true;
            foreach (Vector3 pt in points)
            {
                if (first)
                {
                    xMin = xMax = pt.x;
                    yMin = yMax = pt.y;
                    zMin = zMax = pt.z;
                    first = false;
                }
                else
                {
                    if (pt.x < xMin)
                        xMin = pt.x;
                    else if (pt.x > xMax)
                        xMax = pt.x;

                    if (pt.y < yMin)
                        yMin = pt.y;
                    else if (pt.y > yMax)
                        yMax = pt.y;

                    if (pt.z < zMin)
                        zMin = pt.z;
                    else if (pt.z > zMax)
                        zMax = pt.z;
                }

            }
            Vector3 size = new Vector3(xMax - xMin, yMax - yMin, zMax - zMin);

            bounds = new Bounds(new Vector3(xMin, yMin, zMin) + size * 0.5f, size);

            return !first;
        }

        #endregion

        public static bool Approximately(this Vector3 source, Vector3 point, float range)
        {
            return ((source - point).sqrMagnitude < range * range);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="numberFormat">0.#####</param>
        /// <returns></returns>
        public static string ToString(this Vector3 v, string numberFormat)
        {
            string str = string.Format("({0},{1},{2})", v.x.ToString(numberFormat), v.y.ToString(numberFormat), v.z.ToString(numberFormat));

            return str;
        }





        public static bool IsInPolygon(this Vector2 testPoint, Vector2[] polygon)
        {
            bool result = false;
            int j = polygon.Length - 1;
            Vector2 start, end;
            for (int i = 0, len = polygon.Length; i < len; i++)
            {
                start = polygon[j];
                end = polygon[i];
                if (end.y < testPoint.y && start.y >= testPoint.y || start.y < testPoint.y && end.y >= testPoint.y)
                {
                    if (end.x + (testPoint.y - end.y) / (start.y - end.y) * (start.x - end.x) < testPoint.x)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
       




        #region Gizmos Draw 

        public static void DrawPathGizmos(this IEnumerable<Vector3> points, Color color, bool closed)
        {
            Vector3 first = new Vector3();
            Vector3 last = new Vector3();
            bool isFirst = true;
            Color oldColor = Gizmos.color;
            Gizmos.color = color;
            foreach (var point in points)
            {
                if (isFirst)
                {
                    first = point;
                    isFirst = false;
                }
                else
                {
                    Gizmos.DrawLine(last, point);
                }

                last = point;
            }
            if (!isFirst && closed)
            {
                Gizmos.DrawLine(last, first);
            }
            Gizmos.color = oldColor;
        }





        #endregion

        #region Debug Draw

        public static void DrawPath(this IEnumerable<Vector3> points, Color color, float duration, bool closed)
        {
            Vector3 first = new Vector3();
            Vector3 last = new Vector3();
            bool isFirst = true;
            foreach (var point in points)
            {
                if (isFirst)
                {
                    first = point;
                    isFirst = false;
                }
                else
                {
                    Debug.DrawLine(last, point, color, duration);
                }

                last = point;
            }
            if (!isFirst && closed)
            {
                Debug.DrawLine(last, first, color, duration);
            }
        }

        #endregion
    }
}
