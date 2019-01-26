using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core.Unity
{
    public static partial class Extensions
    {

        public static Rect ToGUIRect(this Rect source)
        {
            Rect rect = new Rect(source.xMin, Screen.height - source.yMax, source.width, source.height);

            return rect;
        }

        public static Rect FitRect(this Rect src, Rect dst)
        {
            return FitRect(src, dst, true);
        }

        public static Rect FitRect(this Rect src, Rect dst, bool center)
        {
            Rect rect = new Rect(src);


            float rate;

            if (dst.width / dst.height < src.width / src.height)
                rate = dst.width / src.width;
            else
                rate = dst.height / src.height;

            if (rate != 1)
            {
                rect.width *= rate;
                rect.height *= rate;
            }
            if (center)
            {
                rect.x = dst.x + (dst.width - rect.width) * 0.5f;
                rect.y = dst.y + (dst.height - rect.height) * 0.5f;
            }
            else
            {
                rect.x = dst.x;
                rect.y = dst.y;

            }
            return rect;
        }


        public static Rect FillRect(this Rect src, Rect dst)
        {
            return FillRect(src, dst, true);
        }

        public static Rect FillRect(this Rect src, Rect dst, bool center)
        {
            Rect rect = new Rect(src);


            float rate;

            if (dst.width / dst.height < src.width / src.height)
                rate = dst.height / src.height;
            else
                rate = dst.width / src.width;

            if (rate != 1)
            {
                rect.width *= rate;
                rect.height *= rate;
            }
            if (center)
            {
                rect.x = dst.x + (dst.width - rect.width) * 0.5f;
                rect.y = dst.y + (dst.height - rect.height) * 0.5f;
            }
            else
            {
                rect.x = dst.x;
                rect.y = dst.y;

            }
            return rect;
        }


        public static IEnumerable<Vector2> EnumeratePoints(this Rect rect)
        {
            Vector2 min = rect.min, max = rect.max;
            yield return min;
            yield return new Vector2(min.x, max.y);
            yield return max;
            yield return new Vector2(max.x, min.y);
        }

        public static Rect AdjustInRect(this Rect src, Rect container)
        {
            Vector2 srcMin = Vector2.Min(src.min, src.max);
            Vector2 srcMax = Vector2.Max(src.min, src.max);
            Vector2 dstMin = Vector2.Min(container.min, container.max);
            Vector2 dstMax = Vector2.Max(container.min, container.max);

            Vector2 dstSize = dstMax - dstMin;
            Vector2 srcSize = srcMax - srcMin;
            Vector2 minOffset = dstMin - srcMin;
            Vector2 maxOffset = dstMax - srcMax;
            Vector2 offset = Vector2.zero;

            if (srcSize.x >= dstSize.x)
            {
                if (minOffset.x < 0f)
                    offset.x = minOffset.x;
                else if (maxOffset.x > 0f)
                    offset.x = maxOffset.x;
            }
            else
            {
                if (minOffset.x > 0f)
                    offset.x = minOffset.x;
                else if (maxOffset.x < 0f)
                    offset.x = maxOffset.x;
            }
            if (srcSize.y >= dstSize.y)
            {
                if (minOffset.y < 0f)
                    offset.y = minOffset.y;
                else if (maxOffset.y > 0f)
                    offset.y = maxOffset.y;
            }
            else
            {
                if (minOffset.y > 0f)
                    offset.y = minOffset.y;
                else if (maxOffset.y < 0f)
                    offset.y = maxOffset.y;
            }
            return new Rect(src.min + offset, src.size);
        }

        public static void GizmosDraw(this Rect rect)
        {
            GizmosDraw(rect, Color.white);
        }

        public static void GizmosDraw(this Rect rect, Color color)
        {
            rect.EnumeratePoints()
                .Select(o =>(Vector3) o)
                .GizmosDrawPath(color, true);
        }

        public static void GizmosDrawLocal(this Rect rect, Transform transform)
        {
            GizmosDrawLocal(rect, transform, Color.white);
        }

        public static void GizmosDrawLocal(this Rect rect, Transform transform, Color color)
        {
            rect.EnumeratePoints()
                .Select(o => transform.TransformPoint(o))
                .GizmosDrawPath(color, true);
        }
        public static void DebugDraw(this Rect rect)
        {
            DebugDraw(rect, Color.white, 0f);
        }

        public static void DebugDraw(this Rect rect, Color color, float duration = 0f)
        {
            rect.EnumeratePoints()
                .Select(o => (Vector3)o)
                .DebugDrawPath(color, duration, true);
        }

        public static void DebugDrawLocal(this Rect rect, Transform transform)
        {
            DebugDrawLocal(rect, transform, Color.white);
        }

        public static void DebugDrawLocal(this Rect rect, Transform transform, Color color, float duration = 0f)
        {
            rect.EnumeratePoints()
                .Select(o => transform.TransformPoint(o))
                .DebugDrawPath(color, duration, true);
        }



    }
}
