using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace se.skoggy.utils.Input
{
    public struct InputState
    {
        public GamePadState State, OldState;
        private const float DeadZone = 0.5f;

        public InputState(GamePadState state, GamePadState oldState)
        {
            State = state;
            OldState = oldState;
        }

        public bool ButtonClicked(Buttons button)
        {
            return State.IsButtonDown(button) && OldState.IsButtonUp(button);
        }

        public bool LeftClicked()
        {
            return (State.ThumbSticks.Left.X < -DeadZone && OldState.ThumbSticks.Left.X > -DeadZone) || ButtonClicked(Buttons.DPadLeft);
        }

        public bool RightClicked()
        {
            return (State.ThumbSticks.Left.X > DeadZone && OldState.ThumbSticks.Left.X < DeadZone) || ButtonClicked(Buttons.DPadRight);
        }

        public bool UpClicked()
        {
            return (State.ThumbSticks.Left.Y > DeadZone && OldState.ThumbSticks.Left.Y < DeadZone) || ButtonClicked(Buttons.DPadUp);
        }

        public bool DownClicked()
        {
            return (State.ThumbSticks.Left.Y < -DeadZone && OldState.ThumbSticks.Left.Y > -DeadZone) || ButtonClicked(Buttons.DPadDown);
        }

        public bool ButtonDown(Buttons button)
        {
            return State.IsButtonDown(button);
        }
    }
}
