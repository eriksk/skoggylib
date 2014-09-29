using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Cameras;

namespace se.skoggy.utils.Graphics
{
    public class DrawableGrid
    {
        private readonly Texture2D _texture;
        private int _cellSize;
        private readonly Color _color;

        public DrawableGrid(Texture2D texture, int cellSize, Color color)
        {
            _texture = texture;
            _cellSize = cellSize;
            _color = color;
        }

        public void Draw(SpriteBatch spriteBatch, Camera cam)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearWrap, null, null);
            spriteBatch.Draw(
                _texture,
                new Rectangle(0, 0, (int)cam.center.X * 2, (int)cam.center.Y * 2),
                new Rectangle((int)-cam.position.X, (int)-cam.position.Y, (int)cam.center.X * 2, (int)cam.center.Y * 2), 
                _color);
            spriteBatch.End();
        }
    }
}
