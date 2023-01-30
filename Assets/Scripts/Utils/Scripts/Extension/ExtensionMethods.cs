using System;
using System.Collections.Generic;
using UnityEngine;

namespace NewUtils
{
    public static class ExtensionMethods
    {
        public static void RemovePoint(this LineRenderer lineRenderer, int index)
        {
            Vector3[] dum = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(dum);
            for (int i = index; i < dum.Length - 1; i++)
            {
                dum[i] = dum[i + 1];
            }
            Array.Resize(ref dum, dum.Length - 1);
            lineRenderer.SetPositions(dum);
        }

        public static void Shuffle<T>(this IList<T> ts)
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }

        public static float Remap(this float from, float fromMin, float fromMax, float toMin, float toMax)
        {
            var fromAbs = from - fromMin;
            var fromMaxAbs = fromMax - fromMin;

            var normal = fromAbs / fromMaxAbs;

            var toMaxAbs = toMax - toMin;
            var toAbs = toMaxAbs * normal;

            var to = toAbs + toMin;

            return to;
        }
    }
}