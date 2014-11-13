using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics;
using Microsoft.Xna.Framework;

namespace se.skoggy.utils.Physics
{
    public static class PhysicsExtensions
    {
        public static float ToSimUnits(this float val)
        {
            return ConvertUnits.ToSimUnits(val);
        }

        public static Vector2 ToSimUnits(this Vector2 val)
        {
            return ConvertUnits.ToSimUnits(val);
        }

        public static float ToSimUnits(this int val)
        {
            return ConvertUnits.ToSimUnits(val);
        }
    }
}
