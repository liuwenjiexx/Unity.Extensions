using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityEngine.Extensions
{
    public static partial class UnityExtensions
    {
        public static void ScaleToFitParent(this RectTransform rectTransform)
        {
            var size = ((RectTransform)rectTransform.parent).rect.size;
            ScaleToFit(rectTransform, size);
        }

        public static void ScaleToFit(this RectTransform rectTransform, Vector2 size)
        {
            Vector2 srcSize = rectTransform.rect.size;
            srcSize = srcSize.ScaleToFit(size);
            rectTransform.sizeDelta = srcSize;
        }

        public static void ScaleToFillParent(this RectTransform rectTransform)
        {
            var size = ((RectTransform)rectTransform.parent).rect.size;
            ScaleToFill(rectTransform, size);
        }

        public static void ScaleToFill(this RectTransform rectTransform, Vector2 size)
        {
            Vector2 srcSize = rectTransform.rect.size;
            srcSize = srcSize.ScaleToFill(size);
            rectTransform.sizeDelta = srcSize;
        }



        public static Rect GetOuterRect(this RectTransform rectTrans)
        {
            Rect rect;
            rectTrans.rect
                .EnumeratePoints()
                .Select(o => (Vector2)rectTrans.TransformPoint(o))
                .GetRect(out rect);
            // Debug.Log("1: " + rect);
            Vector3 min = rectTrans.InverseTransformPoint(rect.min);
            Vector3 max = rectTrans.InverseTransformPoint(rect.max);
            if (min.x > max.x)
            {
                float tmp = min.x;
                min.x = max.x;
                max.x = tmp;
            }
            if (min.y > max.y)
            {
                float tmp = min.y;
                min.y = max.y;
                max.y = tmp;
            }
            rect = new Rect(min + (max - min) * 0.5f, max - min);
            // Debug.Log("2:" + rect);
            return rect;
        }


        public static void AdjustInRectangle(this RectTransform src, RectTransform dst)
        {
            Rect dstRect = dst.rect;
            Bounds bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(dst, src);
            Rect srcRect = new Rect(bounds.min, bounds.size);
            Vector2 offset = srcRect.AdjustInRect(dstRect).min - (Vector2)bounds.min;

            src.localPosition += (Vector3)offset;
        }

        public static bool ScreenPointToPixelPoint(this RectTransform trans, int pixelWidth, int pixelHeight, Vector2 screenPoint, out Vector2Int pixelPoint)
        {
            Vector2 tmp;


            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(trans, screenPoint, trans.GetComponentInParent<Canvas>().worldCamera, out tmp))
            {
                pixelPoint = new Vector2Int();

                Rect rect = trans.rect;

                float x = tmp.x + trans.pivot.x * rect.width;
                float y = tmp.y + trans.pivot.y * rect.height;
                if (!rect.Contains(tmp))
                    return false;
                //if (!rect.Contains(new Vector2(x, y)))
                //    return false;
                float xRate = pixelWidth / rect.width;
                float yRate = pixelHeight / rect.height;

                pixelPoint.x = (int)(x * xRate);
                pixelPoint.y = (int)(y * yRate);

                pixelPoint.y = pixelHeight - pixelPoint.y;

                return true;
            }
            else
            {
                pixelPoint = Vector2Int.zero;
            }

            return false;
        }

        public static void GizmosDrawRectLocal(this RectTransform rectTrans, Color color)
        {
            rectTrans.rect.GizmosDrawLocal(rectTrans, color);
        }

        public static void GizmosDrawRect(this RectTransform rectTrans, Color color)
        {
            Rect rect;
            rectTrans.rect
                .EnumeratePoints()
                .Select(o => (Vector2)rectTrans.TransformPoint(o))
                .GetRect(out rect);
            Vector2 halfSize = rect.size * 0.5f;
            rect.EnumeratePoints()
                .Select(o => (Vector3)(o - halfSize))
                .GizmosDrawLineStrip(color, true);
        }
        public static void DebugDrawRectLocal(this RectTransform rectTrans, Color color, float duration)
        {
            rectTrans.rect.EnumeratePoints()
                .Select(o => rectTrans.TransformPoint(o))
                .DebugDrawLineStrip(color, duration, true);
        }

        public static void DebugDrawRect(this RectTransform rectTrans, Color color, float duration)
        {
            Rect rect;
            rectTrans.rect
                .EnumeratePoints()
                .Select(o => (Vector2)rectTrans.TransformPoint(o))
                .GetRect(out rect);
            Vector2 halfSize = rect.size * 0.5f;
            rect.EnumeratePoints()
                .Select(o => (Vector3)(o - halfSize))
                .DebugDrawLineStrip(color, duration, true);
        }
    }
}
