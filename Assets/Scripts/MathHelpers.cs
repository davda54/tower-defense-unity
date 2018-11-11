using System;
using UnityEngine;


namespace Assets.Scripts
{
    class MathHelpers
    {
        static public float Angle(Vector2 a, Vector2 b)
        {
            var an = a.normalized;
            var bn = b.normalized;
            var x = an.x * bn.x + an.y * bn.y;
            var y = an.y * bn.x - an.x * bn.y;
            return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        }
    }
}
