using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.UI
{
    public class Text
    {
        public Vector2 position, origin, scale;
        public Color color;
        public float rotation;

        public SpriteEffects flip;
        private readonly TextAlign align;
        protected string text;
        private bool dirty;

        public Text(string text, TextAlign align = TextAlign.Left)
        {
            this.text = text;
            this.align = align;
            color = Color.White;
            position = new Vector2();
            origin = new Vector2(0.5f, 0.5f);
            scale = new Vector2(1f, 1f);
            flip = SpriteEffects.None;
            rotation = 0f;
            dirty = true;
        }

        public string Content { get { return text; } set { text = value; dirty = true; } }

        private void Clean(SpriteFont font)
        {
            var size = font.MeasureString(text);

            switch (align)
            {
                case TextAlign.Left:
                    origin.X = 0f * size.X;
                    break;
                case TextAlign.Center:
                    origin.X = 0.5f * size.X;
                    break;
                case TextAlign.Right:
                    origin.X = 1f * size.X;
                    break;
            }

            origin.Y = 0.5f * size.Y;

            dirty = false;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (dirty)
                Clean(font);

            spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, flip, 0f);
        }
    }
}
