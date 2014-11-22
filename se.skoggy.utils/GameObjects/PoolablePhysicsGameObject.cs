using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using se.skoggy.utils.Physics;

namespace se.skoggy.utils.GameObjects
{
    public class PoolablePhysicsGameObject : PoolableGameObject
    {
        public Body Body;

        public virtual void Disable()
        {
            Body.Enabled = false;
        }

        public virtual void Enable()
        {
            Body.Enabled = true;
            Body.LinearVelocity = Vector2.Zero;
            Body.AngularVelocity = 0f;
        }

        public virtual void Update(float dt)
        {
            position = Body.Position.ToDisplayUnits();
            rotation = Body.Rotation;
        }
    }
}
