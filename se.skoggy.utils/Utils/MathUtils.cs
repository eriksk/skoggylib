using Microsoft.Xna.Framework;
using se.skoggy.utils.Interpolations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Utils
{
    public class MathUtils
    {
        public static float AngleLerp(float nowrap, float wraps, float lerp)
        {
            return AngleLerp(Interpolation.Linear, nowrap, wraps, lerp);
        }

        public static float AngleLerp(Interpolation interpolation, float nowrap, float wraps, float lerp)
        {
            float c, d;

            if (wraps < nowrap)
            {
                c = wraps + (float)(Math.PI * 2);
                //c > nowrap > wraps
                d = c - nowrap > nowrap - wraps
                    ? interpolation.Apply(nowrap, wraps, lerp)
                    : interpolation.Apply(nowrap, c, lerp);

            }
            else if (wraps > nowrap)
            {
                c = wraps - (float)(Math.PI * 2);
                //wraps > nowrap > c
                d = wraps - nowrap > nowrap - c
                    ? interpolation.Apply(nowrap, c, lerp)
                    : interpolation.Apply(nowrap, wraps, lerp);

            }
            else { return nowrap; } //Same angle already

            // wrap
            while (MathHelper.ToDegrees(d) < 0)
            {
                d += MathHelper.ToRadians(360f);
            }
            while (MathHelper.ToDegrees(d) > 360f)
            {
                d -= MathHelper.ToRadians(360f);
            }

            return d;
        }

        public static Vector2 RandomPointFromCircle(Vector2 point, int radius)
        {
            float angle = Rand.Next() * 10f;
            return new Vector2(
                point.X + (float)Math.Cos(angle) * radius, 
                point.Y + (float)Math.Sin(angle) * radius);
        }
    }
}
