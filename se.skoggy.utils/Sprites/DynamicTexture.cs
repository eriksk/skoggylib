using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace se.skoggy.utils.Sprites
{
    public class DynamicTexture
    {
        public readonly Texture2D Texture;
        public readonly Rectangle Source;
        public readonly string Name;

        public DynamicTexture(string name, Texture2D texture, Rectangle source)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (texture == null) throw new ArgumentNullException("texture");
        
            Texture = texture;
            Source = source;
            Name = name;
        }
    }
}
