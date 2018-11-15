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
    }
}
