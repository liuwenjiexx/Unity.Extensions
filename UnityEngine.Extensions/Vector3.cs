using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace UnityEngine.Extensions
{
    public static partial class UnityExtensions
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
            Vector3 min = Vector3.Min(bounds.min, bounds.max);
            Vector3 max = Vector3.Max(bounds.min, bounds.max);
            return Vector3.Max(Vector3.Min(source, max), min);
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

        public static Vector3 OnUnitSphereXZ()
        {
            return new Vector3(Random.value - 0.5f, 0f, Random.value - 0.5f).normalized;
        }

        public static Vector3 RandomRangeXZ(this Vector3 source, float maxDistance)
        {
            source += OnUnitSphereXZ() * (maxDistance * Random.value);
            return source;
        }

        public static Vector3 RandomRangeXZ(this Vector3 source, float minDistance, float maxDistance)
        {
            source += OnUnitSphereXZ() * Mathf.Lerp(minDistance, maxDistance, Random.value);
            return source;
        }

        public static Vector3 RandomRotationY(this Vector3 dir, float openAngle)
        {
            return RandomRotationY(Quaternion.LookRotation(dir), openAngle) * Vector3.forward;
        }
        public static Quaternion RandomRotationY(this Quaternion dir, float openAngle)
        {
            float angle = (Random.value - 0.5f) * openAngle;
            return dir * Quaternion.Euler(0, angle, 0);
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
        /// <param name="defaultValue"></param>
        /// <returns></returns> 
        public static bool GetCenter(this IEnumerable<Vector3> points, out Vector3 center)
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
            {
                center = Vector3.zero;
                return false;
            }

            pos /= count;
            center = pos;
            return true;

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



        //public static Bounds GetBounds(this IEnumerable<Vector3> points)
        //{
        //    return GetBounds(points, Vector3.zero);
        //}
        /// <summary>
        /// calculate all points <see cref="Bounds"/>
        /// </summary> 
        /// <returns>if <paramref name="points"/> null or empty the return Bounds(<paramref name="defualtCenter"/>,<see cref="Vector3.zero"/>)</returns>
        //public static bool GetBounds(this IEnumerable<Vector3> points, out Bounds bounds)
        //{
        //    if (points == null)
        //    {
        //        bounds = new Bounds(Vector3.zero, Vector3.zero);
        //        return false;
        //    }
        //    Vector3 min = Vector3.one * float.MaxValue, max = Vector3.one * float.MinValue;
        //    int n = 0;
        //    foreach (var p in points)
        //    {
        //        min = Vector3.Min(min, p);
        //        max = Vector3.Max(max, p);
        //        n++;
        //    }
        //    if (n == 0)
        //    {
        //        bounds = new Bounds(Vector3.zero, Vector3.zero);
        //        return false;
        //    }

        //    Bounds b = new Bounds();
        //    b.SetMinMax(min, max);

        //    bounds = b;
        //    return true;
        //}

        public static bool GetBounds(this IEnumerable<Vector3> points, out Bounds bounds)
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



        public static Vector2 ScreenPointReverseY(this Vector2 screenPoint)
        {
            screenPoint.y = Screen.height - screenPoint.y;
            return screenPoint;
        }
        public static Vector2 ScreenPointReverseY(this Vector2 screenPoint, int screenHeight)
        {
            screenPoint.y = screenHeight - screenPoint.y;
            return screenPoint;
        }
        public static Rect ToRect(this Vector2 center, Vector2 size)
        {
            return new Rect(center.x - size.x * 0.5f, center.y - size.y * 0.5f, size.x, size.y);
        }

        public static void FindNear2Point(this Vector2[] a, Vector2[] b, out int aIndex, out int bIndex)
        {
            float n;

            n = Mathf.Infinity;
            aIndex = -1;
            bIndex = -1;
            float n2;
            for (int i = 0; i < a.Length; i++)
            {
                Vector2 aPoint = a[i];

                for (int j = 0; j < b.Length; j++)
                {
                    n2 = Vector2.SqrMagnitude(aPoint - b[j]);
                    if (n2 < n)
                    {
                        aIndex = i;
                        bIndex = j;
                        n = n2;
                    }
                }
            }

        }

        #region Generate Path Points

        public static IEnumerable<Vector3> CirclePath(this Vector3 center, Vector3 normal, float radius, int pointCount)
        {
            if (pointCount < 2)
                pointCount = 2;
            float deltaAngle = 360f / pointCount;
            Vector3 forward = new Vector3(0f, 0f, radius);
            Vector3 point;
            Quaternion originRot = Quaternion.FromToRotation(normal, Vector3.up);
            for (int i = 0; i < pointCount; i++)
            {
                point = center + (originRot * Quaternion.AngleAxis(i * deltaAngle, Vector3.up)) * forward;
                yield return point;
            }
        }


        #endregion



        #region GL Draw

        public static void GLDrawLine(this Vector3 start, Vector3 end, int lineWidth)
        {
            float x1 = start.x, y1 = start.y, z1 = start.z, x2 = end.x, y2 = end.y, z2 = end.z;

            int halfWidth = (int)(lineWidth * 0.5f);
            GL.Vertex3(x1, y1, z1);
            GL.Vertex3(x2, y2, z2);
            for (float i = 0; i < lineWidth; ++i)
            {
                if (i % 2 == 0)
                {
                    for (float j = -halfWidth; j < halfWidth; ++j)
                    {
                        GL.Vertex3((x1 - (i * 0.5f)), (y1 + j), z1);
                        GL.Vertex3((x2 - (i * 0.5f)), (y2 + j), z2);
                    }
                }
                else
                {
                    for (float j = -halfWidth; j < halfWidth; ++j)
                    {
                        GL.Vertex3((x1 + (i * 0.5f)), (y1 + j), z1);
                        GL.Vertex3((x2 + (i * 0.5f)), (y2 + j), z2);
                    }
                }
            }

        }


        public static void GLDrawLineStrip(this IEnumerable<Vector3> points, Color color)
        {
            GLDrawLineStrip(points, color, 1, false);
        }

        public static void GLDrawLineStrip(this IEnumerable<Vector3> points, Color color, int lineWidth)
        {
            GLDrawLineStrip(points, color, lineWidth, false);
        }

        public static void GLDrawLineStrip(this IEnumerable<Vector3> points, Color color, int lineWidth, bool closed)
        {
            GL.Begin(GL.LINE_STRIP);
            GL.Color(color);
            Vector3 first = new Vector3();
            Vector3 prev = new Vector3();
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
                    prev.GLDrawLine(point, lineWidth);
                }

                prev = point;
            }
            if (!isFirst && closed)
            {
                prev.GLDrawLine(first, lineWidth);
            }
            GL.End();
        }

        #endregion


        #region Gizmos Draw 

        public static void GizmosDrawLineStrip(this IEnumerable<Vector3> points, Color color, bool closed)
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



        public static void GizmosDrawLine(this IEnumerable<Vector3> points, Color color)
        {
            bool isStart = true;
            Vector3 start = Vector3.zero;
            Color oldColor = Gizmos.color;
            Gizmos.color = color;
            foreach (var p in points)
            {
                if (isStart)
                {
                    start = p;
                    isStart = false;
                }
                else
                {
                    Gizmos.DrawLine(start, p);
                    isStart = true;
                }
            }
            Gizmos.color = oldColor;
        }


        #endregion

        #region Debug Draw

        public static void DebugDrawLineStrip(this IEnumerable<Vector3> points, Color color, float duration, bool closed)
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

        public static void DebugDrawLine(this IEnumerable<Vector3> points, Color color, float duration)
        {
            bool isStart = true;
            Vector3 start = new Vector3();
            foreach (var point in points)
            {
                if (isStart)
                {
                    start = point;
                    isStart = false;
                }
                else
                {
                    Debug.DrawLine(start, point, color, duration);
                    isStart = true;
                }
            }

        }

        #endregion

        #region Bezier Curve



        /// <summary>
        /// B(t) = (1 - t)3 P0 + 3 (1 - t)2 t P1 + 3 (1 - t) t2 P2 + t3 P3 
        /// https://catlikecoding.com/unity/tutorials/curves-and-splines/
        /// </summary>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="startTangent"></param>
        /// <param name="endTangent"></param>
        public static Vector3 GetBezierPoint(this Vector3 start, Vector3 end, Vector3 startTangent, Vector3 endTangent, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            float tt = t * t;
            float oneMinusT2 = oneMinusT * oneMinusT;
            return
                 oneMinusT2 * oneMinusT * start +
                 3f * oneMinusT2 * t * startTangent +
                 3f * oneMinusT * tt * endTangent +
                 tt * t * end;
        }

        public static Vector2 GetBezierPoint(this Vector2 start, Vector2 end, Vector2 startTangent, Vector2 endTangent, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            float tt = t * t;
            float oneMinusT2 = oneMinusT * oneMinusT;
            return
                 oneMinusT2 * oneMinusT * start +
                 3f * oneMinusT2 * t * startTangent +
                 3f * oneMinusT * tt * endTangent +
                 tt * t * end;
        }

        /// <summary>
        /// B'(t) = 3 (1 - t)2 (P1 - P0) + 6 (1 - t) t (P2 - P1) + 3 t2 (P3 - P2).
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="startTangent"></param>
        /// <param name="endTangent"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 GetBezierVelocity(this Vector3 start, Vector3 end, Vector3 startTangent, Vector3 endTangent, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return
                3f * oneMinusT * oneMinusT * (startTangent - start) +
                6f * oneMinusT * t * (endTangent - startTangent) +
                3f * t * t * (end - endTangent);
        }

        public static Vector2 GetBezierVelocity(this Vector2 start, Vector2 end, Vector2 startTangent, Vector2 endTangent, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return
                3f * oneMinusT * oneMinusT * (startTangent - start) +
                6f * oneMinusT * t * (endTangent - startTangent) +
                3f * t * t * (end - endTangent);
        }

        public static IEnumerable<Vector3> EnumerateBezierPoints(this Vector3 start, Vector3 end, Vector3 startTangent, Vector3 endTangent, float stepDistance)
        {
            if (stepDistance <= 0f)
            {
                yield return start;
                yield return end;
                yield break;
            }

            Vector3 dir = end - start;
            float totalDist = dir.magnitude;
            if (totalDist <= 0f)
            {
                yield return start;
                yield return end;
                yield break;
            }
            float t = 0;
            float n = stepDistance / totalDist;

            while (true)
            {

                if (t < 1f)
                {
                    yield return GetBezierPoint(start, end, startTangent, endTangent, t);
                    t += n;
                }
                else
                {
                    yield return end;
                    break;
                }

            }
        }

        public static IEnumerable<Vector2> EnumerateBezierPoints(this Vector2 start, Vector2 end, Vector2 startTangent, Vector2 endTangent, float stepDistance)
        {
            if (stepDistance <= 0f)
            {
                yield return start;
                yield return end;
                yield break;
            }

            Vector2 dir = end - start;
            float totalDist = dir.magnitude;
            if (totalDist <= 0f)
            {
                yield return start;
                yield return end;
                yield break;
            }
            float t = 0;
            float n = stepDistance / totalDist;

            while (true)
            {

                if (t < 1f)
                {
                    yield return GetBezierPoint(start, end, startTangent, endTangent, t);
                    t += n;
                }
                else
                {
                    yield return end;
                    break;
                }

            }
        }
        #endregion

        #region Lerp

        public static Vector3 ArcLerp(this Vector3 start, Vector3 end, float arc, float t)
        {
            //Vector3 pos;

            //Vector3 offset = end - start;
            //float radius = offset.magnitude * 0.5f;
            //var center = (start + end) * 0.5f;
            //center -= new Vector3(0, radius * arc, 0);

            //var riseRelCenter = start - center;
            //var setRelCenter = end - center;

            //pos = Vector3.Slerp(riseRelCenter, setRelCenter, t);
            //pos += center;
            //return pos;
            return ArcLerp(start, end, Vector3.up, arc, t);
        }

        public static Vector3 ArcLerp(this Vector3 start, Vector3 end, Vector3 axis, float arc, float t)
        {
            Vector3 pos;

            Vector3 offset = end - start;
            float radius = offset.magnitude * 0.5f;
            var center = (start + end) * 0.5f;
            center -= axis * (radius * arc);

            var riseRelCenter = start - center;
            var setRelCenter = end - center;

            pos = Vector3.Slerp(riseRelCenter, setRelCenter, t);
            pos += center;
            return pos;
        }
        public static Vector3 ArcLerpUnclamped(this Vector3 start, Vector3 end, Vector3 axis, float arc, float t)
        {
            Vector3 pos;

            Vector3 offset = end - start;
            float radius = offset.magnitude * 0.5f;
            var center = (start + end) * 0.5f;
            center -= axis * (radius * arc);

            var riseRelCenter = start - center;
            var setRelCenter = end - center;

            pos = Vector3.SlerpUnclamped(riseRelCenter, setRelCenter, t);
            pos += center;
            return pos;
        }
        #endregion

        static int? WalkableAreaMask;
        public static Vector3 ToNavMeshWalkablePoint(this Vector3 position, float maxDistance = 1f)
        {
            return ToNavMeshWalkablePoint(position, position, maxDistance);
        }
        public static Vector3 ToNavMeshWalkablePoint(this Vector3 position, Vector3 defaultPosition, float maxDistance = 1f)
        {
            NavMeshHit hit;
            if (WalkableAreaMask == null)
                WalkableAreaMask = 1 << NavMesh.GetAreaFromName("Walkable");
            Vector3 result = defaultPosition;
            if (NavMesh.SamplePosition(position, out hit, maxDistance, WalkableAreaMask.Value))
            {
                result = hit.position;
            }

            return result;
        }
        public static Vector3 ClampDirection(this Vector3 from, Vector3 to, float maxAngle)
        {
            float angle = Vector3.Angle(from, to);
            if (angle <= maxAngle)
                return to;
            return Vector3.Lerp(from, to, maxAngle / angle);
            //return ClampDirection(Quaternion.LookRotation(from), Quaternion.LookRotation(to), maxAngle) * Vector3.forward;
        }

    }


}
