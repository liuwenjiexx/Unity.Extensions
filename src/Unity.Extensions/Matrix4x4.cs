using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LWJ.Unity
{
    public static partial class Extensions
    {



        public static IEnumerable<Vector3> MultiplyPoints(this Matrix4x4 source, IEnumerable<Vector3> points)
        {
            return points.Select(p => source.MultiplyPoint(p));
        }

        public static IEnumerable<Vector3> MultiplyPoint3x4s(this Matrix4x4 source, IEnumerable<Vector3> points)
        {
            return points.Select(p => source.MultiplyPoint3x4(p));
        }

        public static IEnumerable<Vector3> MultiplyVectors(this Matrix4x4 source, IEnumerable<Vector3> points)
        {
            return points.Select(p => source.MultiplyVector(p));
        }

        public static Matrix4x4 Lerp(this Matrix4x4 from, Matrix4x4 to, float time)
        {
            Matrix4x4 ret = new Matrix4x4();
            for (int i = 0; i < 16; i++)
                ret[i] = Mathf.Lerp(from[i], to[i], time);
            return ret;
        }
        public static Matrix4x4 LerpUnclamped(this Matrix4x4 from, Matrix4x4 to, float time)
        {
            Matrix4x4 ret = new Matrix4x4();
            for (int i = 0; i < 16; i++)
                ret[i] = Mathf.LerpUnclamped(from[i], to[i], time);
            return ret;
        }
        public static Quaternion ToQuaternion(this Matrix4x4 mat)
        {
            return Quaternion.LookRotation(mat.GetColumn(2), mat.GetColumn(1));
        }

        //可能错误
        private static Quaternion ToQuaternion2(this Matrix4x4 m)
        {
            // Adapted from: http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/index.htm
            Quaternion q = new Quaternion();
            q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2;
            q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2;
            q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2;
            q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;
            q.x *= Mathf.Sign(q.x * (m[2, 1] - m[1, 2]));
            q.y *= Mathf.Sign(q.y * (m[0, 2] - m[2, 0]));
            q.z *= Mathf.Sign(q.z * (m[1, 0] - m[0, 1]));
            return q;
        }

    }
}
