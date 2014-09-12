using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Shaders
{
    public abstract class ShaderParameters
    {
        public abstract void ApplyTo(Effect effect);
    }
}
