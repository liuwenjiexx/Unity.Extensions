using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Core.Unity
{
    public static partial class Extensions
    {


        public static Vector3 Clamp(this Bounds bounds, Vector3 position)
        {
            return position.Clamp(bounds);
        }


        public static Vector3 Clamp(this Bounds bounds, Vector3 position, Transform transform)
        {
            Vector3 localPos = transform.InverseTransformPoint(position);

            localPos = localPos.Clamp(bounds);

            return transform.TransformPoint(localPos);
        }

        public static Bounds ToScreenBounds(this Bounds worldBounds, Camera camera)
        {
            Vector3 min = camera.WorldToScreenPoint(worldBounds.min);
            Vector3 max = camera.WorldToScreenPoint(worldBounds.max);
            min.z = 0f;
            max.z = 0f;
            Vector3 size = max - min;
            return new Bounds(min + size * 0.5f, size);
        }

        public static Vector3[] ToPoints(this Bounds bounds)
        {
            Vector3[] points = new Vector3[8];
            ToPoints(bounds, points, 0);
            return points;
        }

        public static void ToPoints(this Bounds bounds, Vector3[] points, int arrayOffset)
        {
            Vector3 min = bounds.min, max = bounds.max;//, size = bounds.size;/*
            /*yield return min;
            yield return max;

            Vector3 point;
            for (int axis = 0; axis < 3; axis++)
            {
                point = min;
                point[axis] += size[axis];
                yield return point;

                point = max;
                point[axis] -= size[axis];
                yield return point;
            }*/


            points[arrayOffset] = min;
            points[arrayOffset + 1] = new Vector3(max.x, min.y, min.z);
            points[arrayOffset + 2] = new Vector3(max.x, max.y, min.z);
            points[arrayOffset + 3] = new Vector3(min.x, max.y, min.z);
            points[arrayOffset + 4] = new Vector3(min.x, min.y, max.z);
            points[arrayOffset + 5] = new Vector3(max.x, min.y, max.z);
            points[arrayOffset + 6] = max;
            points[arrayOffset + 7] = new Vector3(min.x, max.y, max.z);
        }
        public static void ToPoints2D(this Bounds bounds, Vector2[] outBounds)
        {
            Vector2 min = bounds.min, max = bounds.max;
            outBounds[0] = min;
            outBounds[1] = new Vector2(min.x, max.y);
            outBounds[2] = max;
            outBounds[3] = new Vector2(max.x, min.y);
        }

        //public static Vector3 AdjustmentInScreenBounds(this Bounds fromScreenBounds, Vector3 fromWorldPosition, Camera camera)
        //{
        //    Bounds cameraScreenBounds = camera.GetScreenBounds();
        //    Vector3 screenOffset = AdjustmentInBounds(fromScreenBounds, cameraScreenBounds) - fromScreenBounds.center;
        //    Vector3 pos;
        //    pos = camera.WorldToScreenPoint(fromWorldPosition) + screenOffset;
        //    pos = camera.ScreenToWorldPoint(pos);
        //    return pos;
        //}
        public static Vector3 AdjustmentInScreenBounds(this Bounds fromScreenBounds, Vector3 fromWorldPosition, Camera camera, Bounds screenBounds)
        {
            Vector3 screenOffset = AdjustmentInBounds(fromScreenBounds, screenBounds) - fromScreenBounds.center;
            Vector3 pos;
            pos = camera.WorldToScreenPoint(fromWorldPosition) + screenOffset;
            pos = camera.ScreenToWorldPoint(pos);
            return pos;
        }

        public static Vector3 AdjustmentInBounds(this Bounds from, Bounds to)
        {
            Vector3 fromMin = from.min, fromMax = from.max;
            Vector3 toMin = to.min, toMax = to.max;

            Vector3 offsetMin = toMin - fromMin;
            Vector3 offsetMax = toMax - fromMax;

            if (offsetMin.x > 0f)
            {
                //左
                fromMin.x += offsetMin.x;
                fromMax.x += offsetMin.x;
            }
            else if (offsetMax.x < 0f)
            {
                //右
                fromMin.x += offsetMax.x;
                fromMax.x += offsetMax.x;
            }
            if (offsetMin.y > 0f)
            {
                //下
                fromMin.y += offsetMin.y;
                fromMax.y += offsetMin.y;
            }
            else if (offsetMax.y < 0f)
            {
                //上
                fromMin.y += offsetMax.y;
                fromMax.y += offsetMax.y;
            }

            if (offsetMin.z > 0f)
            {
                //后
                fromMin.z += offsetMin.z;
                fromMax.z += offsetMin.z;
            }
            else if (offsetMax.z < 0f)
            {
                //前
                fromMin.z += offsetMax.z;
                fromMax.z += offsetMax.z;
            }

            return (fromMin + (fromMax - fromMin) * 0.5f);
        }


        public static bool ContainsAny(this Bounds bounds, IEnumerable<Vector3> points)
        {
            return points.Any(p => bounds.Contains(p));
        }
        public static bool ContainsAll(this Bounds bounds, IEnumerable<Vector3> points)
        {
            return points.All(p => bounds.Contains(p));
        }


        public static IEnumerable<Vector3> EnumeratePoints(this Bounds bounds)
        {
            Vector3 min = bounds.min, max = bounds.max;
            yield return min;
            yield return new Vector3(max.x, min.y, min.z);
            yield return new Vector3(max.x, max.y, min.z);
            yield return new Vector3(min.x, max.y, min.z);
            yield return new Vector3(min.x, min.y, max.z);
            yield return new Vector3(max.x, min.y, max.z);
            yield return max;
            yield return new Vector3(min.x, max.y, max.z);
        }

        public static IEnumerable<Vector2> EnumeratePoints2D(this Bounds bounds)
        {
            Vector2 min = bounds.min, max = bounds.max;
            yield return min;
            yield return new Vector2(min.x, max.y);
            yield return max;
            yield return new Vector2(max.x, min.y);
        }


        public static Bounds MultiplyMatrix(this Bounds bounds, Matrix4x4 matrix)
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            foreach (var p in bounds.ToPoints())
            {
                Vector3 p2 = matrix.MultiplyPoint(p);
                min = Vector3.Min(min, p2);
                max = Vector3.Max(max, p2);
            }

            Bounds b = new Bounds();
            b.SetMinMax(min, max);

            return b;
        }




        public static Vector3 RandomPoint(this Bounds bounds)
        {
            Vector3 pos = bounds.center;
            Vector3 size = bounds.size;
            pos.x += (Random.value - 0.5f) * size.x;
            pos.y += (Random.value - 0.5f) * size.y;
            pos.z += (Random.value - 0.5f) * size.z;
            return pos;
        }

        public static Vector3 RandomPoint(this Bounds bounds, float rate)
        {
            Vector3 pos = bounds.center;
            Vector3 size = bounds.size * rate;
            pos.x += (Random.value - 0.5f) * size.x;
            pos.y += (Random.value - 0.5f) * size.y;
            pos.z += (Random.value - 0.5f) * size.z;
            return pos;
        }


        #region IntersectRay


        public static bool IntersectRay(this Bounds bounds, Ray ray, float maxDistance)
        {
            float dist;
            if (bounds.IntersectRay(ray, out dist) && dist <= maxDistance)
                return true;
            return false;
        }
        public static bool IntersectRay(this Bounds bounds, Ray ray, out float distance, float maxDistance)
        {
            float dist;
            if (bounds.IntersectRay(ray, out dist) && dist <= maxDistance)
            {
                distance = dist;
                return true;
            }
            distance = 0;
            return false;
        }
        public static bool IntersectRay(this Bounds bounds, Ray ray, Transform t, out float dist)
        {
            Matrix4x4 mat;
            mat = t.worldToLocalMatrix;
            Ray ray2 = new Ray(mat.MultiplyPoint(ray.origin), mat.MultiplyVector(ray.direction));

            return bounds.IntersectRay(ray2, out dist);
        }

        public static bool IntersectRay(this Bounds bounds, Ray ray, Transform t, float maxDistance)
        {
            float dist;
            return IntersectRay(bounds, ray, t, out dist, maxDistance);
        }
        public static bool IntersectRay(this Bounds bounds, Ray ray, Transform t, out float dist, float maxDistance)
        {
            Matrix4x4 mat;
            mat = t.worldToLocalMatrix;
            Ray ray2 = new Ray(mat.MultiplyPoint(ray.origin), mat.MultiplyVector(ray.direction));

            return bounds.IntersectRay(ray2, out dist) && dist <= maxDistance;
        }


        #endregion


        #region Intersect


        private static bool _Intersects(this Bounds bounds1, Transform transform1, Bounds bounds2, Transform transform2)
        {
            Vector3[] bounds1Points = transform2.InverseTransformPoints(bounds1.EnumeratePoints(), transform1).ToArray();
            Vector3[] bounds2Points = transform1.InverseTransformPoints(bounds2.EnumeratePoints(), transform2).ToArray();

            if (bounds1.ContainsAny(bounds2Points) || bounds2.ContainsAny(bounds1Points))
                return true;

            Func<Vector3, Vector3, Bounds, bool> edgeIntersectsBounds = (point1, point2, bounds) =>
            {
                Ray ray = new Ray(point1, point2 - point1);
                if (!bounds.IntersectRay(ray))
                    return false;
                return true;
                /*
                ray.origin = point2;
                ray.direction = -ray.direction;
                return bounds.IntersectRay(ray);*/
            };

            Func<Vector3[], Bounds, bool> pointsContainIntersectingEdge = (points, bounds) =>
            {
                for (int i = 0; i < points.Length - 1; i++)
                    if (points.Skip(i + 1).Any(p => edgeIntersectsBounds(points[i], p, bounds)))
                        return true;
                return false;
            };

            return pointsContainIntersectingEdge(bounds2Points, bounds1) || pointsContainIntersectingEdge(bounds1Points, bounds2);
        }

        public static bool Intersects(this Bounds bounds1, Transform transform1, Bounds bounds2, Transform transform2)
        {
            Vector3[] bounds1Points = transform2.InverseTransformPoints(bounds1.EnumeratePoints(), transform1).ToArray();
            Vector3[] bounds2Points = transform1.InverseTransformPoints(bounds2.EnumeratePoints(), transform2).ToArray();

            if (bounds1.ContainsAny(bounds2Points) || bounds2.ContainsAny(bounds1Points))
                return true;

            Func<Vector3, Vector3, Bounds, bool> edgeIntersectsBounds = (point1, point2, bounds) =>
            {
                Ray ray = new Ray(point1, point2 - point1);
                if (!bounds.IntersectRay(ray))
                    return false;
                // return true;

                ray.origin = point2;
                ray.direction = -ray.direction;
                return bounds.IntersectRay(ray);
            };

            Func<Vector3[], Bounds, bool> pointsContainIntersectingEdge = (points, bounds) =>
            {
                for (int i = 0; i < points.Length - 1; i++)
                    if (points.Skip(i + 1).Any(p => edgeIntersectsBounds(points[i], p, bounds)))
                        return true;

                return false;
            };

            return pointsContainIntersectingEdge(bounds2Points, bounds1) || pointsContainIntersectingEdge(bounds1Points, bounds2);
        }

        public static bool Intersects(this Bounds bounds1, Matrix4x4 localToWorld1, Bounds bounds2, Matrix4x4 localToWorld2)
        {
            return Intersects(bounds1, localToWorld1, Matrix4x4.Inverse(localToWorld1), bounds2, localToWorld2, Matrix4x4.Inverse(localToWorld2));
        }
        public static bool Intersects(this Bounds bounds1, Matrix4x4 localToWorld1, Matrix4x4 worldToLocal1, Bounds bounds2, Matrix4x4 localToWorld2, Matrix4x4 worldToLocal2)
        {
            //Matrix4x4 worldToLocal1 = Matrix4x4.Inverse(localToWorld1);
            //Matrix4x4 worldToLocal2 = Matrix4x4.Inverse(localToWorld2);
            Vector3[] bounds1Points = worldToLocal2.MultiplyPoints(localToWorld1.MultiplyPoints(bounds1.EnumeratePoints())).ToArray();
            Vector3[] bounds2Points = worldToLocal1.MultiplyPoints(localToWorld2.MultiplyPoints(bounds2.EnumeratePoints())).ToArray();
            // This is redundant, because this will return true in a subset of cases 
            // with edge intersections, but Unity's methods are not in C# and might be faster.
            // Requires profiling.
            if (bounds1.ContainsAny(bounds2Points) || bounds2.ContainsAny(bounds1Points))
                return true;

            Func<Vector3, Vector3, Bounds, bool> edgeIntersectsBounds = (point1, point2, bounds) =>
            {
                Ray ray = new Ray(point1, point2 - point1);
                if (!bounds.IntersectRay(ray))
                    return false;
                return true;
                /*
                ray.origin = point2;
                ray.direction = -ray.direction;
                return bounds.IntersectRay(ray);*/
            };

            Func<Vector3[], Bounds, bool> pointsContainIntersectingEdge = (points, bounds) =>
            {
                for (int i = 0; i < points.Length - 1; i++)
                    if (points.Skip(i + 1).Any(p => edgeIntersectsBounds(points[i], p, bounds)))
                        return true;

                return false;
            };

            return pointsContainIntersectingEdge(bounds2Points, bounds1) || pointsContainIntersectingEdge(bounds1Points, bounds2);
        }

        #endregion



    }
}
