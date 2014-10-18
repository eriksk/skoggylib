using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace se.skoggy.utils.ECSL.Rendering
{
    public interface IRenderContext
    {
        void RenderSprite(ITexture texture, float x, float y, float originX, float originY, float scaleX, float scaleY, float rotation, bool flipX, bool flipY, byte r, byte g, byte b, byte a);
        void RenderSprite(ITexture texture, float x, float y, float originX, float originY, float scaleX, float scaleY, float rotation, int sourceX, int sourceY, int sourceWidth, int sourceHeight, bool flipX, bool flipY, byte r, byte g, byte b, byte a);
    }
}
