using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Resolutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Screens
{
    public interface IGameContext
    {
        GraphicsDevice GraphicsDevice { get; }
        IServiceProvider ServiceProvider { get; }
        string ContentRoot { get; }
        Resolution Resolution{ get; }

        void ChangeScreen(IScreen screen);
        void Exit();
    }
}
