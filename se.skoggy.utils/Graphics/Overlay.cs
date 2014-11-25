using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Resolutions;
using se.skoggy.utils.Tweening;

namespace se.skoggy.utils.Graphics
{
    public class Overlay : ITweenable
    {
        private readonly Texture2D _pixel;
        private readonly Resolution _resolution;
        private  Color _color;

        public Overlay(Texture2D pixel, Resolution resolution,  Color color)
        {
            if (pixel == null) throw new ArgumentNullException("pixel");
            if (resolution == null) throw new ArgumentNullException("resolution");
            _pixel = pixel;
            _resolution = resolution;
            _color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            spriteBatch.Draw(_pixel, new Rectangle(0, 0, _resolution.Width, _resolution.Height), _color);
            spriteBatch.End();
        }

        public void SetPosition(float x, float y)
        {
        }

        public void SetPositionX(float x)
        {
        }

        public void SetPositionY(float y)
        {
        }

        public void AddRotation(float rotationToAdd)
        {
        }

        public void SetRotation(float rotation)
        {
        }

        public void SetScale(float scalar)
        {
        }

        public void SetScale(float x, float y)
        {
        }

        public void SetAlpha(float a)
        {
            _color.A = (byte)(a * 255f);
        }

        public void SetColor(float r, float g, float b, float a)
        {
            _color.R = (byte)(r * 255f);
            _color.G = (byte)(g * 255f);
            _color.B = (byte)(b * 255f);
            _color.A = (byte)(a * 255f);
        }
    }
}
