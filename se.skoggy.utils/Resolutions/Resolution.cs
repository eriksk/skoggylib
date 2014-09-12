using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Resolutions
{
    public class Resolution
    {
        public readonly int Width;
        public readonly int Height;

        public Resolution(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
