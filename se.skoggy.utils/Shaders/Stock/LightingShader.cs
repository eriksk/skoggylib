using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace se.skoggy.utils.Shaders.Stock
{
    public class LightingShader : Shader<LightingShaderParameters>
    {
        public LightingShader()
            : base("lighting", new LightingShaderParameters())
        {
        }
    }

    public class LightingShaderParameters : ShaderParameters
    {
        public Vector4 Ambient { get; set; }
        public float GlobalLightIntensity { get; set; }

        public LightingShaderParameters()
        {
            Ambient = new Vector4(1f, 1f, 1f, 0.8f);
            GlobalLightIntensity = 0.3f;
        }

        public override void ApplyTo(Effect effect)
        {
            effect.Parameters["ambientColor"].SetValue(Ambient);
            effect.Parameters["globalLightIntensity"].SetValue(GlobalLightIntensity);
        }
    }
}
