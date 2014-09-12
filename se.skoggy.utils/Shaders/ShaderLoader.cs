using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Shaders
{
    public class ShaderLoader
    {
        private ContentManager content;
        private string shaderDirectoryName;

        public ShaderLoader(ContentManager content, string shaderDirectoryName)
        {
            this.content = content;
            this.shaderDirectoryName = shaderDirectoryName;
        }

        public Effect LoadEffect(string name) 
        {
            return content.Load<Effect>(string.Format("{0}/{1}", shaderDirectoryName, name));
        }
    }
}
