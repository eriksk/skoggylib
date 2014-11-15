using System.Collections.Generic;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Physics;
using se.skoggy.utils.Physics.Objects;

namespace se.skoggy.utils.GameObjects
{
    public class GameObjectAttachment
    {
        public GameObject GameObject;
        public bool Render, Update, Additive;

        public GameObjectAttachment(GameObject gameObject, bool render, bool update, bool additive = false)
        {
            GameObject = gameObject;
            Render = render;
            Update = update;
            Additive = additive;
        }
    }

    public class PhysicsGameObject : GameObject, IPhysicsBody
    {
        private readonly List<GameObjectAttachment> _attachments; 

        public PhysicsGameObject()
            : base()
        {
            _attachments = new List<GameObjectAttachment>();
        }

        public PhysicsGameObject(Texture2D texture)
            : base(texture)
        {
            _attachments = new List<GameObjectAttachment>();
        }

        public override GameObject SetPosition(Vector2 position)
        {
            Body.Position = position.ToSimUnits();
            this.position = position;
            return this;
        }

        public override GameObject SetPosition(float x, float y)
        {
            this.position.X = x;
            this.position.Y = y;
            Body.Position = position.ToSimUnits();
            return this;
        }

        public override GameObject SetRotation(float rotation)
        {
            this.rotation = rotation;
            Body.Rotation = rotation;
            return this;
        }

        public virtual PhysicsGameObject AddAttachment(GameObjectAttachment attachment)
        {
            _attachments.Add(attachment);
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

        public override void Update(float dt)
        {
            position = ConvertUnits.ToDisplayUnits(Body.Position);
            rotation = Body.Rotation;

            foreach (var attachment in _attachments)
            {
                if(!attachment.Update) continue;
                attachment.GameObject.position = position;
                attachment.GameObject.rotation = rotation;
            }
            base.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            originInPixels.X = source.Width * origin.X;
            originInPixels.Y = source.Height * origin.Y;

            spriteBatch.Draw(texture, ConvertUnits.ToDisplayUnits(Body.Position), source, color, Body.Rotation, originInPixels, scale, flip, 0f);
        }

        public void DrawAttachments(SpriteBatch spriteBatch, bool additive)
        {
            foreach (var attachment in _attachments)
            {
                if (!attachment.Render) continue;
                if(additive == attachment.Additive)
                    attachment.GameObject.Draw(spriteBatch);
            }
        }
    }
}
