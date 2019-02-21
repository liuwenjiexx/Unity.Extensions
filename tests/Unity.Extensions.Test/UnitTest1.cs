using System;
using UnityEngine.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;


namespace Unity.Extensions.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AdjustInRect()
        {
            // Debug.Log("Test AdjustInRect");
            Rect dst;
            dst = new Rect(0, 0, 100, 100);
            Action<Rect, Rect, Rect> test = (src, container, assert) =>
            {
                var adjust = src.AdjustInRect(container);
                Assert.IsTrue(adjust.Equals(assert), "src:{0},dst:{1}, adjust:{2}", src, container, adjust);
            };
            test(new Rect(10, 10, 10, 10), dst, new Rect(10, 10, 10, 10));
            test(new Rect(-10, -10, 10, 10), dst, new Rect(0, 0, 10, 10));
            test(new Rect(10, 10, 110, 110), dst, new Rect(0, 0, 110, 110));

            // Debug.Log("-width,-height");

            test(new Rect(10, 10, -10, -10), dst, new Rect(10, 10, -10, -10));
            test(new Rect(0, 0, -10, -10), dst, new Rect(10, 10, -10, -10));
            test(new Rect(10, 10, -110, -110), dst, new Rect(100, 100, -110, -110));


            dst = new Rect(0, 0, -100, -100);

            test(new Rect(0, 0, 10, 10), dst, new Rect(-10, -10, 10, 10));
            test(new Rect(10, 10, 10, 10), dst, new Rect(-10, -10, 10, 10));
            test(new Rect(0, 0, -10, -10), dst, new Rect(0, 0, -10, -10));
            test(new Rect(-10, -10, -10, -10), dst, new Rect(-10, -10, -10, -10));
            test(new Rect(-10, -10, -110, -110), dst, new Rect(0, 0, -110, -110));


            // Debug.Log("dst:" + dst);


        }


        //void Test()
        //{
        //    return;
        //    Debug.Log("0 :" + Utils.ClampVelocity(0, 10));
        //    Debug.Log("1:" + Utils.ClampVelocity(1, 10));
        //    Debug.Log("0:" + Utils.ClampVelocity(-1, 10));
        //    Debug.Log("0:" + Utils.ClampVelocity(0, -10));
        //    Debug.Log("0:" + Utils.ClampVelocity(1, -10));
        //    Debug.Log("-1:" + Utils.ClampVelocity(-1, -10));

        //    Debug.Log("5:" + Utils.AccelerationVelocity(5, 0, 10));
        //    Debug.Log("6:" + Utils.AccelerationVelocity(5, 1, 10));
        //    Debug.Log("10:" + Utils.AccelerationVelocity(15, 0, 10));
        //    Debug.Log("4:" + Utils.AccelerationVelocity(5, -1, 10));
        //    Debug.Log("-5:" + Utils.AccelerationVelocity(5, 0, -10));
        //    Debug.Log("-4:" + Utils.AccelerationVelocity(5, 1, -10));
        //    Debug.Log("-6:" + Utils.AccelerationVelocity(5, -1, -10));
        //    Debug.Log("-15:" + Utils.AccelerationVelocity(-15, 0, -10));
        //    Debug.Log("35:" + Utils.AccelerationVelocity(5, 40, 20));

        //}
        [TestMethod]
        public void TestCalculateVelocity()
        {

        }
        public float CalculateVelocity(float velocity, float accel, float decel, float maxVelocity)
        {
            float offsetAccel = accel - decel;
            float max = maxVelocity;
            if (offsetAccel < 0)
            {
                max = 0f;
            }

            float offset = maxVelocity - velocity;
            float absAcc = Mathf.Abs(offsetAccel);
            if (Mathf.Abs(offset) < absAcc)
                return maxVelocity;
            velocity += (Mathf.Sign(offset) * absAcc);


            return velocity;
        }
    }
}
