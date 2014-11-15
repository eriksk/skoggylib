using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using se.skoggy.utils.Cameras;
using se.skoggy.utils.GUI;
using se.skoggy.utils.Input;

namespace se.skoggy.utils.Screens
{
    public abstract class GuiBaseScreen : BaseScreen
    {
        private GuiContext _guiContext;
        private SpriteFont _guifont;
        private Camera _guiCam;

        protected GuiBaseScreen(IGameContext context, string name, int virtualWidth, int virtualHeight) 
            : base(context, name, virtualWidth, virtualHeight)
        {
        }

        public override void Load()
        {
            var pixel = new Texture2D(context.GraphicsDevice, 1, 1);
            pixel.SetData(new[] {Color.White});
            _guiContext = new GuiContext(pixel);

            _guifont = content.Load<SpriteFont>(FontName);
            _guiCam = new Camera(new Vector2());
            _guiCam = new Camera(new Vector2(VirtualResolution.Width / 2, VirtualResolution.Height / 2));
            base.Load();
        }

        public abstract string FontName { get; }

        public GuiContext Gui 
        {
            get { return _guiContext; }
        }

        /// <summary>
        /// Font for gui
        /// </summary>
        public SpriteFont Font
        {
            get { return _guifont; }
        }

        public override void Update(float dt)
        {
            _guiCam.Update(dt);
            base.Update(dt);
        }

        public override void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, _guiCam.View);
            _guiContext.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw();
        }
    }
}
