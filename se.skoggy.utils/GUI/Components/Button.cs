using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using se.skoggy.utils.Graphics;
using se.skoggy.utils.Input;
using se.skoggy.utils.UI;

namespace se.skoggy.utils.GUI.Components
{
    public class Button : GuiComponent
    {
        public delegate void ButtonClicked();
        public event ButtonClicked OnButtonClicked;

        public Button(GuiContext context)
            : base(context)
        {
        }

        private void InvokeClickEvent()
        {
            if (OnButtonClicked != null) 
                OnButtonClicked();
        }

        public override float Height
        {
            get { return Font.MeasureString(Text).Y*scale.Y; }
        }

        public override void Update(float dt, IInputState input)
        {
            if (HasFocus && input.ButtonClicked(Buttons.A))
            {
                InvokeClickEvent();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var t = new Text(Text, TextAlign.Center)
            {
                rotation = rotation,
                position = position,
                color = color,
                scale = scale,
                Content = Text
            };

            Vector2 textSize = Font.MeasureString(Text);
            const int padding = 8;

            new DrawableRectangle(
                Context.Pixel, 
                new Rectangle(
                    (int)(position.X - textSize.X / 2f) - padding,
                    (int)(position.Y - textSize.Y / 2f) - padding,
                    (int)textSize.X + padding,
                    (int)textSize.Y + padding), 
                    HasFocus ? SelectedColor : BackgroundColor, 
                    Color.Transparent)
                .Draw(spriteBatch);

            t.Draw(spriteBatch, Font);
        }
    }
}
