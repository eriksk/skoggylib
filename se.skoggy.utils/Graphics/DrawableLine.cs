using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Graphics
{
    public class DrawableLine
    {
        public Vector2 start, end;

        public DrawableLine()
        {
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D pixel, Color color) 
        {
            int length = (int)Vector2.Distance(start, end) + 1;
            int x = (int)start.X;
            int y = (int)start.Y;
            float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);

            spriteBatch.Draw(pixel, new Rectangle(x, y, length, 1), null, color, angle, Vector2.Zero, SpriteEffects.None, 0f);
        }
    }
}
