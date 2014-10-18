using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.ECSL.Interfaces;
using se.skoggy.utils.ECSL.Rendering;

namespace se.skoggy.utils.ECSL.Implementations.Components
{
    public class SpriteRenderer : GameObjectComponent, IRenderer
    {
        public Texture2D Texture { get; set; }
        public Vector2 Origin = new Vector2(0.5f, 0.5f);

        public void Render(IRenderContext renderContext)
        {
            throw new NotImplementedException();
        }
    }
}
