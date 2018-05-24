using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace LWJ.Unity
{
    public partial class Extensions
    {

        public static T FindComponentByName<T>(this GameObject go, string name)
            where T : Component
        {
            Transform t = FindByName(go.transform, name);
            if (t)
            {
                T result = t.GetComponent<T>();
                return result;
            }
            return null;
        }


        public static T EnsureComponent<T>(this GameObject go)
            where T : Component
        {
            T result = go.GetComponent<T>();

            if (result == null)
                result = go.AddComponent<T>();

            return result;
        }



        #region GetRendererBounds

        public static Bounds GetRendererBounds(this GameObject go)
        {
            Bounds bounds;
            GetRendererBounds(go, out bounds);
            return bounds;
        }

        public static bool GetRendererBounds(this GameObject go, out Bounds bounds)
        {
            bounds = new Bounds(go.transform.position, Vector3.zero);

            Renderer[] renderers = go.GetComponentsInChildren<Renderer>();

            if (renderers.Length == 0)
                return false;

            bool hasBounds = false;
            Bounds tmp;

            foreach (Renderer r in renderers)
            {
                if (!r.enabled)
                    continue;
                tmp = r.bounds;

                if (tmp.size == Vector3.zero)
                    continue;

                if (hasBounds)
                {
                    bounds.Encapsulate(tmp);
                }
                else
                {
                    bounds = tmp;
                    hasBounds = true;
                }

            }

            return hasBounds;
        }

        public static Bounds GetRendererBoundsLocal(this GameObject go)
        {
            Bounds bounds;
            GetRendererBoundsLocal(go, out bounds);
            return bounds;
        }

        public static bool GetRendererBoundsLocal(this GameObject go, out Bounds bounds)
        {
            Transform t = go.transform;
            Matrix4x4 rootMat = t.localToWorldMatrix;
            bool hasBounds = false;

            Vector3[] points = new Vector3[8];
            int i;
            MeshFilter mf;
            Matrix4x4 mat;
            bounds = new Bounds();
            Bounds tmp;
            foreach (Renderer r in go.GetComponentsInChildren<Renderer>())
            {
                if (!r.enabled)
                    continue;
                mf = r.GetComponent<MeshFilter>();
                tmp = mf.sharedMesh.bounds;
                if (tmp.size == Vector3.zero)
                    continue;
                mat = rootMat.inverse * r.transform.localToWorldMatrix;
                i = 0;
                foreach (Vector3 point in tmp.EnumeratePoints())
                {
                    points[i++] = mat.MultiplyPoint(point);
                }
                if (hasBounds)
                {
                    bounds.Encapsulate(points.GetBounds());
                }
                else
                {
                    bounds = points.GetBounds();
                    hasBounds = true;
                }
            }

            return hasBounds;
        }

        //  public static Bounds GetRendererBoundsLocal(this GameObject go)
        //  {
        //      Transform t = go.transform;
        //      Quaternion originRot = t.rotation;
        //      Vector3 originScale = t.localScale;

        //      Transform parent = t.parent;
        //      t.parent = null;
        //      Bounds bounds;

        //      t.rotation = Quaternion.identity;
        //      bounds = go.GetRendererBounds();


        //      /*Matrix4x4 mat = t.worldToLocalMatrix;            
        //  bounds.center = mat.MultiplyPoint(bounds.center);
        //  bounds.size = mat.MultiplyVector(t.rotation * bounds.size);
        //*/
        //      bounds.center = t.InverseTransformPoint(bounds.center);
        //      Vector3 s = t.lossyScale;
        //      Vector3 scale = new Vector3((s.x == 0 ? 0 : 1 / s.x), (s.y == 0 ? 0 : 1 / s.y), (s.z == 0 ? 0 : 1 / s.z));
        //      bounds.size = Vector3.Scale(bounds.size, scale);

        //      t.parent = parent;
        //      t.rotation = originRot;
        //      t.localScale = originScale;

        //      return bounds;
        //  }

        public static Bounds GetRendererBounds(this IEnumerable<GameObject> gos)
        {
            if (gos == null)
                return new Bounds();
            bool first = true;
            Bounds bounds = new Bounds();

            Bounds tmp;
            foreach (GameObject go in gos)
            {
                if (!go || !go.activeInHierarchy)
                    continue;

                tmp = go.GetRendererBounds();

                if (tmp.size == Vector3.zero)
                    continue;

                if (first)
                {
                    bounds = tmp;
                    first = !first;
                }
                else
                {
                    bounds.Encapsulate(tmp);
                }

            }

            return bounds;
        }

        #endregion

        public static GameObject AddEventTrigger(this GameObject go, EventTriggerType triggerType, UnityAction<BaseEventData> callback)
        {
            var click = new EventTrigger.TriggerEvent();
            click.AddListener(callback);

            var trigger = go.GetComponent<EventTrigger>();
            if (!trigger)
                trigger = go.AddComponent<EventTrigger>();
            trigger.triggers
                    .Add(new EventTrigger.Entry() { eventID = triggerType, callback = click });
            return go;
        }
        public static GameObject RemoveEventTrigger(this GameObject go, EventTriggerType triggerType, UnityAction<BaseEventData> callback)
        {
            var click = new EventTrigger.TriggerEvent();
            click.AddListener(callback);

            var trigger = go.GetComponent<EventTrigger>();
            if (!trigger)
                trigger = go.AddComponent<EventTrigger>();

            var triggers = trigger.triggers;
            for (int i = 0, len = triggers.Count; i < len; i++)
            {
                if (triggers[i].eventID == triggerType)
                {
                    triggers[i].callback.RemoveListener(callback);
                    break;
                }
            }
            return go;
        }
        //public static void AddEventTrigger(this GameObject go, EventTriggerType eventType, UnityAction<BaseEventData> callback)
        //{
        //    EventTrigger trigger;
        //    trigger = go.EnsureComponent<EventTrigger>();
        //    var entry = trigger.triggers.Where(o => o.eventID == eventType).FirstOrDefault();
        //    if (entry == null)
        //    {
        //        entry = new EventTrigger.Entry() { eventID = EventTriggerType.PointerClick, callback = new EventTrigger.TriggerEvent() };
        //        trigger.triggers.Add(entry);
        //    }
        //    entry.callback.AddListener(callback);
        //}
        public static RectTransform GetRectTransform(this GameObject go)
        {
            return go.transform as RectTransform;
        }
    }
}
