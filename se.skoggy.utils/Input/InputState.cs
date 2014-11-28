using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace se.skoggy.utils.Input
{
    public interface IInputState
    {
        GamePadState State { get; }
        GamePadState OldState { get; }
        void Update();
        bool ButtonClicked(Buttons button);
        bool LeftClicked();
        bool RightClicked();
        bool UpClicked();
        bool DownClicked();
        bool ButtonDown(Buttons button);
    }

    public class InputState : IInputState
    {
        private GamePadState _state, _oldState;
        private const float DeadZone = 0.5f;

        public InputState()
        {
        }

        public void Update()
        {
            _oldState = _state;
            _state = GamePad.GetState(PlayerIndex.One);
        }

        public GamePadState State
        {
            get { return _state; }
        }

        public GamePadState OldState
        {
            get { return _oldState; }
        }

        public bool ButtonClicked(Buttons button)
        {
            return _state.IsButtonDown(button) && _oldState.IsButtonUp(button);
        }

        public bool LeftClicked()
        {
            return (_state.ThumbSticks.Left.X < -DeadZone && _oldState.ThumbSticks.Left.X > -DeadZone) || ButtonClicked(Buttons.DPadLeft);
        }

        public bool RightClicked()
        {
            return (_state.ThumbSticks.Left.X > DeadZone && _oldState.ThumbSticks.Left.X < DeadZone) || ButtonClicked(Buttons.DPadRight);
        }

        public bool UpClicked()
        {
            return (_state.ThumbSticks.Left.Y > DeadZone && _oldState.ThumbSticks.Left.Y < DeadZone) || ButtonClicked(Buttons.DPadUp);
        }

        public bool DownClicked()
        {
            return (_state.ThumbSticks.Left.Y < -DeadZone && _oldState.ThumbSticks.Left.Y > -DeadZone) || ButtonClicked(Buttons.DPadDown);
        }

        public bool ButtonDown(Buttons button)
        {
            return _state.IsButtonDown(button);
        }
    }
}
