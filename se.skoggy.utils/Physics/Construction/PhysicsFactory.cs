using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using se.skoggy.utils.MathUtils;
using se.skoggy.utils.Physics.Objects;

namespace se.skoggy.utils.Physics.Construction
{
    public class PhysicsFactory
    {
        public static Body CreateRect(IPhysicsBody owner, PhysicsWorld world, float width, float height, BodyType type = BodyType.Static)
        {
            var body = BodyFactory.CreateRectangle(world.World, ConvertUnits.ToSimUnits(width), ConvertUnits.ToSimUnits(height), 1f);
            body.BodyType = type;
            body.UserData = owner;
            if (owner != null)
                owner.Body = body;

            return body;
        }

        public static Body CreateCircle(IPhysicsBody owner, PhysicsWorld world, float radius, BodyType type = BodyType.Static)
        {
            var body = BodyFactory.CreateCircle(world.World, ConvertUnits.ToSimUnits(radius), 1f);
            body.BodyType = type;
            body.UserData = owner;
            if (owner != null)
                owner.Body = body;

            return body;
        }
    }
}
