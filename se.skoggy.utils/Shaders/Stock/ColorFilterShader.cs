using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Shaders.Stock
{
    public class ColorFilterShader : Shader<ColorFilterShaderParameters>
    {
        public ColorFilterShader()
            :base("color_filter", new ColorFilterShaderParameters())
        {
        }
    }
}
