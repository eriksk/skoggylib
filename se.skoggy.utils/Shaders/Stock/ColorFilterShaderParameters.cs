using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Shaders.Stock
{
    public class ColorFilterShaderParameters : ShaderParameters
    {
        public float Burn { get; set; }
        public float Saturation { get; set; }
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float Brightness { get; set; }
        public float RefractionMagnitude { get; set; }
        
        public ColorFilterShaderParameters()
        {
            Burn = 0.4f;
            Saturation = 0.8f;
            R = 1.0f;
            G = 1.0f;
            B = 1.0f;
            Brightness = 0.1f;
            RefractionMagnitude = 0.3f;
        }

        public override void ApplyTo(Effect effect)
        {
            effect.Parameters["burn"].SetValue(Burn);
            effect.Parameters["saturation"].SetValue(Saturation);
            effect.Parameters["r"].SetValue(R);
            effect.Parameters["g"].SetValue(G);
            effect.Parameters["b"].SetValue(B);
            effect.Parameters["brightness"].SetValue(Brightness);
            effect.Parameters["refractionMagnitude"].SetValue(RefractionMagnitude);
        }
    }
}
