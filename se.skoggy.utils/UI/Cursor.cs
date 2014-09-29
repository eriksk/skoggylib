using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using se.skoggy.utils.GameObjects;

namespace se.skoggy.utils.UI
{
    public sealed class Cursor : GameObject
    {
        private MouseState _state, _oldState;
        private readonly int _virtualWidth;
        private readonly int _virtualHeight;
        private readonly int _actualWidth;
        private readonly int _actualHeight;

        public Cursor(Texture2D texture, int virtualWidth, int virtualHeight, int actualWidth, int actualHeight)
            : base(texture)
        {
            _virtualWidth = virtualWidth;
            _virtualHeight = virtualHeight;
            _actualWidth = actualWidth;
            _actualHeight = actualHeight;
            SetOrigin(0f, 0f);
            SetScale(0.6f);
        }

        public bool LeftDown
        {
            get { return _state.LeftButton == ButtonState.Pressed; }
        }

        public bool RightDown
        {
            get { return _state.RightButton == ButtonState.Pressed; }
        }

        public bool LeftClicked
        {
            get { return _state.LeftButton == ButtonState.Pressed && _oldState.LeftButton == ButtonState.Released; }
        }

        public bool RightClicked
        {
            get { return _state.RightButton == ButtonState.Pressed && _oldState.RightButton == ButtonState.Released; }
        }

        public bool MiddleClicked
        {
            get { return _state.MiddleButton == ButtonState.Pressed && _oldState.MiddleButton == ButtonState.Released; }
        }

        public float ScrollDiff
        {
            get { return _state.ScrollWheelValue - _oldState.ScrollWheelValue; }
        }

        public override void Update(float dt)
        {
            _oldState = _state;
            _state = Mouse.GetState();
            position.X = ((float) _state.X/(float) _actualWidth)*(float) _virtualWidth;
            position.Y = ((float) _state.Y/(float) _actualHeight)*(float) _virtualHeight;

            base.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            const float shadowOffset = 6f;
            Color oldColor = color;
            color = new Color(0, 0, 0, 50);
            position.X -= shadowOffset;
            position.Y += shadowOffset;

            base.Draw(spriteBatch);

            position.X += shadowOffset;
            position.Y -= shadowOffset;

            color = oldColor;
            base.Draw(spriteBatch);
        }
    }
}