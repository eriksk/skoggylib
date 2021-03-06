﻿using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Bootstrap;
using se.skoggy.utils.Resolutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Screens
{
    public interface IGameContext : IGameBootstrapper
    {
        string ContentRoot { get; }
        GraphicsDevice GraphicsDevice { get; }
        IServiceProvider ServiceProvider { get; }
        Resolution Resolution{ get; }
        DisplayModeCollection DisplayModes { get; }
        void ChangeDisplayMode(DisplayMode displayMode);
        bool IsFullScreen { get; }
        void SetFullScreen(bool fullScreen);
        
        void ChangeScreen(IScreen screen);
        void Exit();

    }
}
