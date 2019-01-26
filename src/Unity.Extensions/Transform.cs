using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Unity
{
    public static partial class Extensions
    {

        public static void SetX(this Transform source, float x)
        {
            Vector3 pos = source.position;
            pos.x = x;
            source.position = pos;
        }

        public static void SetY(this Transform source, float y)
        {
            Vector3 pos = source.position;
            pos.y = y;
            source.position = pos;
        }

        public static void SetZ(this Transform source, float z)
        {
            Vector3 pos = source.position;
            pos.z = z;
            source.position = pos;
        }

        public static void SetXLocal(this Transform source, float x)
        {
            Vector3 pos = source.localPosition;
            pos.x = x;
            source.localPosition = pos;
        }

        public static void SetYLocal(this Transform source, float y)
        {
            Vector3 pos = source.localPosition;
            pos.y = y;
            source.localPosition = pos;
        }

        public static void SetZLocal(this Transform source, float z)
        {
            Vector3 pos = source.localPosition;
            pos.z = z;
            source.localPosition = pos;
        }

        public static Transform GetAncestorNode(this Transform[] source)
        {
            if (source == null || source.Length == 0)
                return null;
            if (source.Length == 1)
                return source[0].parent;

            Transform commonParent = source[0].parent;
            Transform parent;
            bool hasFind;
            while (commonParent)
            {
                hasFind = true;
                for (int i = 1, len = source.Length; i < len; i++)
                {
                    parent = source[i].parent;
                    while (parent)
                    {
                        if (commonParent == parent)
                            break;
                        parent = parent.parent;
                    }
                    if (!parent)
                    {
                        hasFind = false;
                        break;
                    }
                }
                if (hasFind)
                    break;
                commonParent = commonParent.parent;
            }
            return commonParent;
        }

        public static void DestroyChildren(this Transform source)
        {
            if (source == null) throw new NullReferenceException();
            Transform child;
            for (int i = source.childCount - 1; i >= 0; i--)
            {
                child = source.GetChild(i);
                GameObject.Destroy(child.gameObject);
            }
        }

        public static void DestroyChildrenImmediate(this Transform source)
        {
            if (source == null) throw new NullReferenceException();
            Transform child;
            for (int i = source.childCount - 1; i >= 0; i--)
            {
                child = source.GetChild(i);
                GameObject.DestroyImmediate(child.gameObject);
            }
        }

        /// <summary>
        /// normalize local
        /// </summary>
        /// <param name="source"></param>
        public static void NormalizeLocal(this Transform source)
        {
            if (source == null) throw new NullReferenceException();

            source.localPosition = Vector3.zero;
            source.localEulerAngles = Vector3.zero;
            source.localScale = Vector3.one;
        }

        /// <summary>
        /// normalize world
        /// </summary> 
        public static void NormalizeWorld(this Transform source)
        {
            if (source == null) throw new NullReferenceException();

            source.position = Vector3.zero;
            source.eulerAngles = Vector3.zero;
            Transform p = source.parent;
            if (p != null)
            {
                Vector3 s = p.lossyScale;
                source.localScale = new Vector3((s.x == 0 ? 0 : 1 / s.x), (s.y == 0 ? 0 : 1 / s.y), (s.z == 0 ? 0 : 1 / s.z));
            }
            else
            {
                source.localScale = Vector3.one;
            }
        }


        public static Vector3 TransformScale(this Transform source, Vector3 localScale)
        {
            return Vector3.Scale(localScale, source.lossyScale);
        }

        public static Vector3 InverseTransformScale(this Transform source, Vector3 worldScale)
        {
            if (source == null) throw new NullReferenceException();

            Vector3 scale = source.lossyScale;
            Vector3 result = Vector3.zero;

            if (scale.x != 0)
                result.x = worldScale.x / scale.x;

            if (scale.y != 0)
                result.y = worldScale.y / scale.y;

            if (scale.z != 0)
                result.z = worldScale.z / scale.z;

            return result;
        }



        /// <summary>
        /// from local to world space
        /// </summary>
        public static IEnumerable<Vector3> TransformPoints(this Transform source, IEnumerable<Vector3> localPoints)
        {
            if (source == null) throw new NullReferenceException();
            return localPoints.Select(p => source.TransformPoint(p));
        }

        /// <summary>
        /// from world to local space
        /// </summary>
        public static IEnumerable<Vector3> InverseTransformPoints(this Transform source, IEnumerable<Vector3> worldPoints)
        {
            if (source == null) throw new NullReferenceException();
            return worldPoints.Select(p => source.InverseTransformPoint(p));
        }


        /// <summary>
        /// to local space
        /// </summary>
        public static IEnumerable<Vector3> InverseTransformPoints(this Transform source, IEnumerable<Vector3> localPoints, Transform otherTransform)
        {
            return source.InverseTransformPoints(otherTransform.TransformPoints(localPoints));
        }


        /// <summary>
        /// contains self
        /// </summary> 
        public static IEnumerable<Transform> EnumerateParents(this Transform source, bool includeInactive = false)
        {
            if (source == null)
                yield break;

            if (!includeInactive && !source.gameObject.activeInHierarchy)
                yield break;
            Transform t = source;

            while (t != null)
            {
                yield return t;
                t = t.parent;
            }

        }




        #region  IEnumerable<Transform> 


        public static bool Raycast(this IEnumerable<Transform> items, Ray ray, out RaycastHit hit, out float dist, float maxDistance)
        {

            float minDist = float.MaxValue;
            RaycastHit near = new RaycastHit();
            bool has = false;

            foreach (RaycastHit hit1 in Physics.RaycastAll(ray, maxDistance))
            {
                Transform t = hit1.transform;

                if (!items.Contains(t))
                    continue;

                dist = hit1.distance;
                if (dist < minDist)
                {
                    minDist = dist;
                    near = hit1;
                    has = true;
                }
            }
            hit = near;
            if (!has)
            {
                dist = 0;

                return false;
            }

            dist = minDist;

            return true;
        }



        public static bool RaycastNearPosition(this IEnumerable<Transform> items, Ray ray, out RaycastHit hit, out float dist, float maxDistance)
        {

            float minDist = float.MaxValue;
            RaycastHit near = new RaycastHit();
            bool has = false;
            Vector3 lineStart = ray.origin;
            Vector3 lineEnd = lineStart + ray.direction;
            foreach (RaycastHit hit1 in Physics.RaycastAll(ray, maxDistance))
            {
                Transform t = hit1.transform;

                if (!items.Contains(t))
                    continue;

                dist = t.position.GetLineDistance(lineStart, lineEnd);
                if (dist < minDist)
                {
                    minDist = dist;
                    near = hit1;
                    has = true;
                }
            }
            hit = near;
            if (!has)
            {
                dist = 0;

                return false;
            }

            dist = minDist;

            return true;
        }



        #endregion


        #region Find Child

        /// <summary>
        /// deep first find
        /// </summary>
        /// <param name="t"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Transform FindByName(this Transform t, string name)
        {
            Transform result = null;
            foreach (Transform child in t)
            {
                if (child.name == name)
                {
                    result = child;
                    break;
                }
                result = FindByName(child, name);
                if (result != null)
                    break;
            }

            return result;
        }

        #endregion


        #region EnumerateAllChild

        public static IEnumerable<Transform> EnumerateChildren(this Transform parent)
        {
            return (IEnumerable<Transform>)parent;
            //    foreach (Transform child in parent)
            //        yield return child;
        }

        public static IEnumerable<Transform> EnumerateAllChildren(this Transform parent, bool active)
        {
            if (active)
                return EnumerateAllChildrenActive(parent);
            return EnumerateAllChildren(parent);
        }

        /// <summary>
        /// not contains self
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static IEnumerable<Transform> EnumerateAllChildren(this Transform parent)
        {
            foreach (Transform child in parent)
                yield return child;

            foreach (Transform child in parent)
                foreach (Transform c2 in EnumerateAllChildren(child))
                    yield return c2;
        }

        private static IEnumerable<Transform> EnumerateAllChildrenActive(this Transform parent)
        {


            foreach (Transform t in parent)
                if (t.gameObject.activeSelf)
                    yield return t;

            foreach (Transform t in parent)
            {
                if (t.gameObject.activeSelf)
                {
                    foreach (var c in EnumerateAllChildrenActive(t))
                        yield return c;
                }
            }
        }

        #endregion

        public static string GetPath(this Transform parent, Transform child)
        {
            string path = null;
            if (parent)
            {
                Transform p = child;
                while (p != parent)
                {
                    if (path != null)
                        path = "/" + path;
                    path = p.name + path;
                    p = p.parent;
                    if (p == null)
                        throw new Exception(parent.name + " not contains child " + child.name);
                }
            }

            if (path == null)
                return "";
            return path;
        }

        public static void Translate(this IEnumerable<Transform> targets, Vector3 offset)
        {
            if (targets == null)
                return;
            foreach (Transform t in targets)
            {
                if (!t) continue;
                t.Translate(offset);
                //t.position += offset;
            }
        }

        public static void RotateAround(this IEnumerable<Transform> targets, Vector3 center, Vector3 axis, float angle)
        {
            if (targets == null)
                return;
            foreach (Transform t in targets)
            {
                if (!t) continue;
                t.RotateAround(center, axis, angle);
            }

        }

        public static void ResetChildCount(this Transform transform, int count, GameObject itemPrefab, Action<GameObject> newCallback = null)
        {
            while (transform.childCount > count)
            {
                GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
            }
            while (transform.childCount < count)
            {
                var go = GameObject.Instantiate(itemPrefab, transform);
                go.SetActive(true);
                if (newCallback != null)
                    newCallback(go);
            }
        }

    }
}
