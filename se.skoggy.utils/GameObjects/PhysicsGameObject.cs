using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Physics.Objects;

namespace se.skoggy.utils.GameObjects
{
    public class PhysicsGameObject : GameObject, IPhysicsBody
    {
        public PhysicsGameObject()
            : base()
        {
        }

        public PhysicsGameObject(Texture2D texture)
            : base(texture)
        {
        }

        public override GameObject SetPosition(Vector2 position)
        {
            this.position = ConvertUnits.ToSimUnits(position);
            Body.Position = position;
            return this;
        }

        public override GameObject SetPosition(float x, float y)
        {
            this.position.X = ConvertUnits.ToSimUnits(x);
            this.position.Y = ConvertUnits.ToSimUnits(y);
            Body.Position = position;
            return this;
        }

        public override GameObject SetRotation(float rotation)
        {
            this.rotation = rotation;
            Body.Rotation = rotation;
            return this;
        }

        public Vector2 Position
        {
            get { return ConvertUnits.ToDisplayUnits(position); }
            set { SetPosition(value); }
        }

        public float Rotation
        {
            get { return rotation; }
            set { SetRotation(value); }
        }

        public Body Body { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            originInPixels.X = source.Width * origin.X;
            originInPixels.Y = source.Height * origin.Y;

            spriteBatch.Draw(texture, ConvertUnits.ToDisplayUnits(Body.Position), source, color, Body.Rotation, originInPixels, scale, flip, 0f);
        }
    }
}
