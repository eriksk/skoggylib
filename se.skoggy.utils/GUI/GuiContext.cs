using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Input;
using se.skoggy.utils.Services.Locators;

namespace se.skoggy.utils.GUI
{
    public class GuiContext
    {
        private readonly Texture2D _pixel;
        private readonly List<GuiComponent> _components;
        private GuiComponent _focused;

        public GuiContext(Texture2D pixel)
        {
            if (pixel == null) throw new ArgumentNullException("pixel");
            _pixel = pixel;
            _components = new List<GuiComponent>();
        }

        public void Add(GuiComponent component)
        {
            _components.Add(component);
        }

        public Texture2D Pixel 
        {
            get { return _pixel; }
        }

        public void FocusFirst()
        {
            if(_components.Count > 0)
                SetFocus(_components.First());
        }

        public void SetFocus(GuiComponent component)
        {
            if (component == null)
                return;

            if (_focused != null)
                _focused.OnLostFocus();
            _focused = component;
            component.OnFocused();
        }

        public bool HasFocus(GuiComponent component)
        {
            return _focused == component;
        }

        public void Update(float dt)
        {
            var input = ServiceLocator.Context.Locate<IInputState>();
            foreach (var component in _components)
                component.Update(dt, input);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
                component.Draw(spriteBatch);
        }
    }
}
