using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Ranges
{
    public class ColorRange
    {
        public Color min, max;

        public ColorRange() 
        {
        }

        public Color Random() 
        {
            Color c = new Color(
                Rand.Next(min.R, max.R),
                Rand.Next(min.G, max.G),
                Rand.Next(min.B, max.B),
                Rand.Next(min.A, max.A)
            );
            return c;
        }
    }
}
