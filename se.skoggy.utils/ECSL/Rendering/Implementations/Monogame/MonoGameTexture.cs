using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace se.skoggy.utils.ECSL.Rendering.Implementations.Monogame
{
    public class MonoGameTexture : ITexture
    {
        public readonly Texture2D Texture;

        public MonoGameTexture(Texture2D texture)
        {
            if (texture == null) throw new ArgumentNullException("texture");
            this.Texture = texture;
        }

    }
}
