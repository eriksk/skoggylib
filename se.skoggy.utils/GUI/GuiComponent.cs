using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.GameObjects;
using se.skoggy.utils.Input;

namespace se.skoggy.utils.GUI
{
    public abstract class GuiComponent : GameObject
    {
        private readonly GuiContext _context;
        private readonly List<GuiComponent> _children;

        protected GuiComponent(GuiContext context)
        {
            _context = context;
            _children = new List<GuiComponent>();
            BackgroundColor = Color.Transparent;
        }

        public void AddChild(GuiComponent component)
        {
            _children.Add(component);
        }

        protected GuiContext Context
        {
            get { return _context; }
        }
        public Color BackgroundColor { get; set; }
        public object Tag { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public SpriteFont Font { get; set; }

        public bool HasFocus 
        {
            get { return _context.HasFocus(this); }
        }
        public virtual void OnLostFocus()
        {
        }

        public virtual void OnFocused()
        {
        }

        public virtual void Update(float dt, InputState input)
        {
            foreach (var child in _children)
            {
                child.Update(dt, input);
            }
        }
    }
}
