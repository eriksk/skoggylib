﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Graphics
{
    public class DrawableRectangle
    {
        protected Rectangle rectangle;
        public Color fillColor;
        public Color outlineColor;
        protected Texture2D pixel;

        public DrawableRectangle(Texture2D pixel)
            : this(pixel, 0, 0, 0, 0, Color.White, Color.White)
        {
        }

        public DrawableRectangle(Texture2D pixel, int x, int y, int width, int height, Color fillColor, Color outlineColor)
        {
            this.pixel = pixel;
            rectangle = new Rectangle(x, y, width, height);
            this.fillColor = fillColor;
            this.outlineColor = outlineColor;
        }
        public DrawableRectangle(Texture2D pixel, Rectangle area, Color fillColor, Color outlineColor)
        {
            this.pixel = pixel;
            rectangle = area;
            this.fillColor = fillColor;
            this.outlineColor = outlineColor;
        }

        public int X { get { return rectangle.X; } set { rectangle.X = value; } }
        public int Y { get { return rectangle.Y; } set { rectangle.Y = value; } }
        public int Width { get { return rectangle.Width; } set { rectangle.Width = value; } }
        public int Height { get { return rectangle.Height; } set { rectangle.Height = value; } }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixel, rectangle, fillColor);
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Top, 1, rectangle.Height), outlineColor); // left
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Right, rectangle.Top, 1, rectangle.Height), outlineColor); // right
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, 1), outlineColor); // top
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width + 1, 1), outlineColor); // bottom
        }

        public void DrawGrid(SpriteBatch spriteBatch,  int cellSize)
        {
            int cols = Width/cellSize;
            int rows = Height/cellSize;

            for (int row = 0; row < rows; row++)
            {
                spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Top + row * cellSize, rectangle.Width, 1), outlineColor); // top
                for (int col = 0; col < cols; col++)
                {
                    spriteBatch.Draw(pixel, new Rectangle(rectangle.Left + col * cellSize, rectangle.Top, 1, rectangle.Height), outlineColor); // left                    
                }
            }

            spriteBatch.Draw(pixel, new Rectangle(rectangle.Right, rectangle.Top, 1, rectangle.Height), outlineColor); // right
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, 1), outlineColor); // bottom 
        }
    }
}
