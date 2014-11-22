using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;

namespace se.skoggy.utils.Physics.Events
{
    public class PhysicsEvents
    {
        public delegate void BodyHitByExplosion(Body body, float force, float reach, float distance);
        public event BodyHitByExplosion OnBodyHitByExplosion;

        public void InvokeOnBodyHitByExplosion(Body body, float force, float reach, float distance)
        {
            if (OnBodyHitByExplosion != null) OnBodyHitByExplosion(body, force, reach, distance);
        }
    }
}
