using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using se.skoggy.utils.GUI;
using se.skoggy.utils.Input;

namespace se.skoggy.utils.Screens
{
    public abstract class GuiBaseScreen : BaseScreen
    {
        private GuiContext _guiContext;
        private SpriteFont _guifont;
        private GamePadState _oldPad, _pad;

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
            _oldPad = _pad;
            _pad = GamePad.GetState(PlayerIndex.One);
            _guiContext.Update(dt, new InputState(_pad, _oldPad));
            base.Update(dt);
        }

        public override void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.View);
            _guiContext.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw();
        }
    }
}
