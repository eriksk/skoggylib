using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace se.skoggy.utils.ECSL.Rendering.Implementations.Monogame
{
    public class MonoGameRenderContext : IRenderContext
    {
        private readonly SpriteBatch _spriteBatch;

        public MonoGameRenderContext(SpriteBatch spriteBatch)
        {
            if (spriteBatch == null) throw new ArgumentNullException("spriteBatch");
            _spriteBatch = spriteBatch;
        }

        public void RenderSprite(ITexture texture, float x, float y, float originX, float originY, float scaleX, float scaleY,
            float rotation, bool flipX, bool flipY, byte r, byte g, byte b, byte a)
        {
            Texture2D tex = (texture as MonoGameTexture).Texture;

            SpriteEffects flip = SpriteEffects.None;
            if(flipX)
                flip = SpriteEffects.FlipHorizontally;
            if(flipY)
                flip &= SpriteEffects.FlipVertically;

            originX = tex.Width * originX;
            originY = tex.Height * originY;

            _spriteBatch.Draw(tex, new Vector2(x, y), null, new Color(r, g, b, a), rotation, new Vector2(originX, originY), new Vector2(scaleX, scaleY), flip, 0f);
        }

        public void RenderSprite(ITexture texture, float x, float y, float originX, float originY, float scaleX, float scaleY,
            float rotation, int sourceX, int sourceY, int sourceWidth, int sourceHeight, bool flipX, bool flipY, byte r, byte g, byte b, byte a)
        {
            Texture2D tex = (texture as MonoGameTexture).Texture;

            SpriteEffects flip = SpriteEffects.None;
            if (flipX)
                flip = SpriteEffects.FlipHorizontally;
            if (flipY)
                flip &= SpriteEffects.FlipVertically;


            originX = sourceWidth * originX;
            originY = sourceHeight * originY;

            _spriteBatch.Draw(tex, new Vector2(x, y), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), new Color(r, g, b, a), rotation, new Vector2(originX, originY), new Vector2(scaleX, scaleY), flip, 0f);
        }
    }
}
