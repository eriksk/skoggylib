using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Input;

namespace se.skoggy.utils.GUI.Components
{
    public class VerticalList : GuiComponent
    {

        private int _selectedItem;
        private float prevY;
        private float padding = 24;

        public VerticalList(GuiContext context)
            : base(context)
        {
        }

        protected override void OnChildAdded(GuiComponent child)
        {
            child.position.Y = prevY;
            prevY += child.Height + padding;
            base.OnChildAdded(child);
        }

        public override float Height
        {
            get { return Children.Sum(x => x.Height); }
        }

        public override void OnFocused()
        {
            FocusSelected();
            base.OnFocused();
        }

        public override void Update(float dt, InputState input)
        {
            if (input.UpClicked())
            {
                Previous();
            }
            else if (input.DownClicked())
            {
                Next();
            }

            base.Update(dt, input);
        }

        private void Next()
        {
            _selectedItem++;
            if (_selectedItem > Children.Count - 1)
                _selectedItem = 0;
            FocusSelected();
        }

        private void Previous()
        {
            _selectedItem--;
            if (_selectedItem < 0)
                _selectedItem = Children.Count - 1;
            FocusSelected();
        }

        private void FocusSelected()
        {
            Context.SetFocus(Children[_selectedItem]);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var child in Children)
            {
                child.Draw(spriteBatch);
            }
        }
    }
}
