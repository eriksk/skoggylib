using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace se.skoggy.utils.MathUtils
{
    public struct Rectanglef
    {
        public float X, Y, Width, Height;

        public Rectanglef(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public float CenterX
        {
            get { return X + Width / 2f; }
        }
        public float CenterY
        {
            get { return Y + Height / 2f; }
        }

        public float Bottom
        {
            get { return Y + Height; }
        }

        public float Top
        {
            get { return Y; }
        }

        public float Left 
        {
            get { return X; }
        }

        public float Right
        {
            get { return X + Width; }
        }

        public bool Intersects(Rectanglef other)
        {
            if (Left > other.Right) return false;
            if (Right < other.Left) return false;
            if (Top > other.Bottom) return false;
            if (Bottom < other.Top) return false;

            return true;
        }

        public bool Contains(float x, float y)
        {
            if (Left > x) return false;
            if (Right < x) return false;
            if (Top > y) return false;
            if (Bottom < y) return false;

            return true;
        }

        public Vector2 Min
        {
            get
            {
                return new Vector2(X, Y);
            }
        }

        public Vector2 Max
        {
            get
            {
                return new Vector2(X + Width, Y + Height);
            }
        }
    }
}
