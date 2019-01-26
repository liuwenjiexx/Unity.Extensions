using System;
using Core.Unity;
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
    }
}
