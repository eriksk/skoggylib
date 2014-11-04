using Microsoft.Xna.Framework;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public Matrix Matrix
        {
            get
            {
                return Matrix.CreateScale(Scale.X, Scale.Y, 1f) *
                       Matrix.CreateRotationZ(Rotation) *
                       Matrix.CreateTranslation(Position.X, Position.Y, 0f);
            }
        }

        public Transform Clone()
        {
            return new Transform()
            {
                Position = Position,
                Scale = Scale,
                Rotation = Rotation
            };
        }
    }
}
