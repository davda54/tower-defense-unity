using System;
using UnityEngine;


namespace Assets.Scripts
{
    static class MathHelpers
    {
        static public float Angle(Vector2 a, Vector2 b)
        {
            var an = a.normalized;
            var bn = b.normalized;
            var x = an.x * bn.x + an.y * bn.y;
            var y = an.y * bn.x - an.x * bn.y;
            return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        }

        public static Texture2D textureFromSprite(Sprite sprite)
        {
            if (sprite.rect.width != sprite.texture.width)
            {
                Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
                Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                             (int)sprite.textureRect.y,
                                                             (int)sprite.textureRect.width,
                                                             (int)sprite.textureRect.height);
                newText.SetPixels(newColors);
                newText.Apply();
                return newText;
            }
            else
                return sprite.texture;
        }

        // https://stackoverflow.com/questions/5817490/implementing-box-mueller-random-number-generator-in-c-sharp
        public static float NextGaussianDouble()
        {
            float u, v, S;

            do
            {
                u = 2.0f * UnityEngine.Random.value - 1.0f;
                v = 2.0f * UnityEngine.Random.value - 1.0f;
                S = u * u + v * v;
            }
            while (S >= 1.0);

            float fac = Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
            return u * fac;
        }

        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }
    }
}
