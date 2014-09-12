using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Shaders.Stock
{
    public class HorizontalLinesShaderParameters : ShaderParameters
    {
        public float Magnitude { get; set; }
        public float Lines { get; set; }

        public HorizontalLinesShaderParameters()
        {
            Magnitude = 0.1f;
            Lines = 1000f;
        }

        public override void ApplyTo(Effect effect)
        {
            effect.Parameters["Magnitude"].SetValue(Magnitude);
            effect.Parameters["Lines"].SetValue(Lines);
        }
    }
}
