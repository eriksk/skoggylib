using Microsoft.Xna.Framework.Graphics;

namespace se.skoggy.utils.Shaders.Stock
{
    public class BloomShader : Shader<BloomShaderParameters>
    {
        public BloomShader()
            : base("bloom", new BloomShaderParameters())
        {
        }
    }

    public class BloomShaderParameters : ShaderParameters
    {
        public float Alpha { get; set; }
        public float Blur { get; set; }

        public BloomShaderParameters()
        {
            Alpha = 0.6f;
            Blur = 0.01f;
        }

        public override void ApplyTo(Effect effect)
        {
            effect.Parameters["blur"].SetValue(Blur);
            effect.Parameters["alpha"].SetValue(Alpha);
        }

    }
}


