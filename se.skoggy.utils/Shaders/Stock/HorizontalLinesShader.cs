using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Shaders.Stock
{
    public class HorizontalLinesShader : Shader<HorizontalLinesShaderParameters>
    {
        public HorizontalLinesShader()
            :base("horizontal_lines", new HorizontalLinesShaderParameters())
        {
        }

        public override void Begin(GraphicsDevice device, SpriteBatch spriteBatch)
        {
            base.Begin(device, spriteBatch);
        }

        public override void End(GraphicsDevice device, SpriteBatch spriteBatch)
        {
            base.End(device, spriteBatch);
        }

    }
}
