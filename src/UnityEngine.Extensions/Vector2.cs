using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityEngine.Extensions
{
    public static partial class UnityExtensions
    {

        public static Vector2 Center(this Vector2 point1, Vector2 point2)
        {
            return new Vector2((point1.x + point2.x) * 0.5f, (point1.y + point2.y) * 0.5f);
        }


        public static Vector2 Lerp(this Vector2 point1, Vector2 point2, float value)
        {
            if (value > 1.0f)
                return point2;
            if (value < 0.0f)
                return point1;
            return new Vector2(point1.x + (point2.x - point1.x) * value, point1.y + (point2.y - point1.y) * value);
        }


        public static float SignAngle(this Vector2 vector1, Vector2 vector2)
        {
            float num1 = (vector1.x * vector2.y) - (vector2.x * vector1.y);
            float num2 = (vector1.x * vector2.x) + (vector1.y * vector2.y);

            return Mathf.Atan2(num1, num2) * Mathf.Rad2Deg;
        }


        public static Vector2 ToLineCrossPoint(this Vector2 source, Vector2 lineStart, Vector2 lineEnd)
        {
            Vector2 dir = lineEnd - lineStart;
            Vector2 w = source - lineStart;

            float b = Vector2.Dot(w, dir) / Vector2.Dot(dir, dir);

            return lineStart + b * dir;
        }


        public static Vector2 ToSegmentCrossPoint(this Vector2 source, Vector2 lineStart, Vector2 lineEnd)
        {
            Vector2 v = lineEnd - lineStart;
            Vector2 w = source - lineStart;

            float c1 = Vector2.Dot(w, v);
            if (c1 <= 0)
                return lineStart;
            float c2 = Vector2.Dot(v, v);

            if (c2 <= c1)
                return lineEnd;

            return lineStart + (c1 / c2) * v;
        }

        public static bool ToSegmentCrossPoint(this Vector2 source, Vector2 lineStart, Vector2 lineEnd, out Vector2 crossPoint)
        {
            Vector2 v = lineEnd - lineStart;
            Vector2 w = source - lineStart;

            float c1 = Vector2.Dot(w, v);
            if (c1 <= 0)
            {
                crossPoint = Vector2.zero;
                return false;
            }
            float c2 = Vector2.Dot(v, v);

            if (c2 <= c1)
            {
                crossPoint = Vector2.zero;
                return false;
            }
            crossPoint = lineStart + c1 / c2 * v;
            return true;
        }

        public static float GetLineDistance(this Vector2 source, Vector2 lineStart, Vector2 lineEnd)
        {
            return Vector2.Distance(source, ToLineCrossPoint(source, lineStart, lineEnd));
        }


        public static float GetSegmentDistance(this Vector2 source, Vector2 lineStart, Vector2 lineEnd)
        {
            return Vector2.Distance(source, ToSegmentCrossPoint(source, lineStart, lineEnd));
        }


        /// <summary>
        /// 
        /// </summary> 
        /// <returns>true: in line ,false:out line</returns>
        public static bool GetSegmentDistance(this Vector2 source, Vector2 lineStart, Vector2 lineEnd, out float distance)
        {
            Vector2 point;
            if (ToSegmentCrossPoint(source, lineStart, lineEnd, out point))
            {
                distance = Vector2.Distance(source, point);
                return true;
            }
            distance = 0;
            return false;
        }

        public static Vector2 Rotate(this Vector2 dir, float angle)
        {
            Vector2 p = Quaternion.AngleAxis(angle, Vector3.back) * dir;
            return p;
        }


        public static bool GetRect(this IEnumerable<Vector2> points, out Rect rect)
        {
            float xMin = 0, xMax = 0, yMin = 0, yMax = 0;
            bool first = true;
            foreach (Vector3 pt in points)
            {
                if (first)
                {
                    xMin = xMax = pt.x;
                    yMin = yMax = pt.y;
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
                }

            }
            Vector3 size = new Vector3(xMax - xMin, yMax - yMin);

            rect = new Rect(new Vector3(xMin, yMin) + size * 0.5f, size);

            return !first;
        }

   

        public static bool GetBounds(this IEnumerable<Vector2> points, out Bounds bounds)
        {
            float xMin = 0, xMax = 0, yMin = 0, yMax = 0;
            bool first = true;
            foreach (Vector2 pt in points)
            {
                if (first)
                {
                    xMin = xMax = pt.x;
                    yMin = yMax = pt.y;
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
                }

            }
            Vector2 size = new Vector3(xMax - xMin, yMax - yMin, 0);

            bounds = new Bounds(new Vector2(xMin, yMin) + size * 0.5f, size);

            return !first;
        }



        public static Vector2 ScaleToFit(this Vector2 src, float dstX, float dstY)
        {
            return ScaleToFit(src, new Vector2(dstX, dstY));
        }

        public static Vector2 ScaleToFit(this Vector2 src, Vector2 dst)
        {
            float rate;

            if (dst.x / dst.y < src.x / src.y)
                rate = dst.x / src.x;
            else
                rate = dst.y / src.y;

            if (rate != 1f)
            {
                src.x *= rate;
                src.y *= rate;
            }

            return src;
        }

        public static Vector2 ScaleToFill(this Vector2 src, float dstX, float dstY)
        {
            return ScaleToFill(src, new Vector2(dstX, dstY));
        }
        public static Vector2 ScaleToFill(this Vector2 src, Vector2 dst)
        {
            float rate;

            if (dst.x / dst.y < src.x / src.y)
                rate = dst.y / src.y;
            else
                rate = dst.x / src.x;

            if (rate != 1f)
            {
                src.x *= rate;
                src.y *= rate;
            }

            return src;
        }

        public static void GetLineRect(this Vector2 start, Vector2 end, float lineWidth, Vector2[] outBounds)
        {

            Vector2 dir = end - start;
            Vector2 crossDir = dir.Rotate(-90).normalized;

            Vector2 min = start + crossDir * (lineWidth * 0.5f);
            Vector2 max = end + (-crossDir * (lineWidth * 0.5f));

            outBounds[0] = min;
            outBounds[1] = max + crossDir * lineWidth;
            outBounds[2] = max;
            outBounds[3] = min - crossDir * lineWidth;

        }
        #region Bezier

        private static Material lineMat;

        public static bool CalculateBezierTangent(this Vector2 start, Vector2 end, float arrowDist, out Vector2 startTangent, out Vector2 endTangent)
        {
            Vector2 dir;
            float dist;

            float angle = Vector2.Angle((end - start), Vector2.right);
            if (angle > 90)
            {
                angle = 180 - angle;
            }
            bool horizontal = false;
            if (angle < 45f)
            {
                horizontal = true;
            }
            else
            {
                horizontal = false;
            }
            if (horizontal)
            {

                dir = new Vector2(end.x - start.x, 0);
            }
            else
            {
                dir = new Vector2(0, end.y - start.y);
            }
            dist = dir.magnitude;
            dir = dir.normalized;
            float _arrowDist = dist * arrowDist;
            startTangent = start + dir * _arrowDist;
            endTangent = end + dir * -_arrowDist;

            return horizontal;
        }

        public static void GLScreenBezier(this Vector2 start, Vector2 end, Color lineColor, float lineWidth, float lineStep, Material mat = null)
        {
            GLScreenBezier(start, end, 0.3f, lineColor, lineWidth, lineStep, mat);
        }

        public static void GLScreenBezier(this Vector2 start, Vector2 end, float arrowDist, Color lineColor, float lineWidth, float lineStep, Material mat = null)
        {
            Vector2 startTangent, endTangent;
            CalculateBezierTangent(start, end, arrowDist, out startTangent, out endTangent);
            GLScreenBezier(start, startTangent, end, endTangent, lineColor, lineWidth, lineStep, mat);
        }

        public static void GLScreenBezier(this Vector2 start, Vector2 startTangent, Vector2 end, Vector2 endTangent, Color lineColor, float lineWidth, float lineStep, Material mat = null)
        {
            if (mat == null)
            {
                if (lineMat == null)
                {
                    //lineMat = new Material("Shader \"Lines/Colored Blended\" {" +
                    //               "SubShader { Pass { " +
                    //               "    Blend SrcAlpha OneMinusSrcAlpha " +
                    //               "    ZWrite Off Cull Off Fog { Mode Off } " +
                    //               "    BindChannels {" +
                    //               "      Bind \"vertex\", vertex Bind \"color\", color }" +
                    //               "} } }");//生成画线的材质 
                    //lineMat = new Material(Shader.Find("Lines/Colored Blended"));

                    lineMat = new Material(Shader.Find("UI/Default"));
                }
                mat = lineMat;
            }
            GL.PushMatrix();
            GL.LoadPixelMatrix();
            mat.SetPass(0);


            IEnumerable<Vector3> path;

            path = start.EnumerateBezierPoints(end, startTangent, endTangent, lineStep).Select(o => (Vector3)o);

            path.GLDrawLineStrip(lineColor, Mathf.CeilToInt(lineWidth));

            GL.PopMatrix();
        }

        #endregion
    }
}