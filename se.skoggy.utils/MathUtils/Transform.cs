using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace se.skoggy.utils.MathUtils
{
    public class Transform
    {
        public Vector2 Position;
        public Vector2 Scale;
        public float Rotation;

        public Transform()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0;
        }

        public Matrix Matrix
        {
            get
            {
                return Matrix.CreateScale(Scale.X, Scale.Y, 1f) *
                       Matrix.CreateRotationZ(Rotation) *
                       Matrix.CreateTranslation(Position.X, Position.Y, 0f);
            }
        }
    }
}
