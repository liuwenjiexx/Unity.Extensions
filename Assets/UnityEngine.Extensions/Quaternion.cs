using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine.Extensions
{
    public static partial class UnityExtensions
    {
        public static Quaternion RotateTowards(this Quaternion current, Quaternion target, float maxAngularDelta)
        {
            if (maxAngularDelta <= 0f)
                return current;
            float angle = Quaternion.Angle(current, target);
            Quaternion rot;
            if (angle > maxAngularDelta)
            {
                float t = maxAngularDelta / angle;
                rot = Quaternion.Lerp(current, target, t);
            }
            else
            {
                rot = target;
            }
            return rot;
        }
        public static Quaternion ClampDirection(this Quaternion from, Quaternion to, float maxAngle)
        {
            float angle = Quaternion.Angle(from, to);
            if (angle <= maxAngle)
                return to;
            return Quaternion.Lerp(from, to, Mathf.InverseLerp(0, angle, maxAngle));
        }
    }

}
