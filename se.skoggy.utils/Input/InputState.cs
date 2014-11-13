using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace se.skoggy.utils.Input
{
    public struct InputState
    {
        public GamePadState State, OldState;

        public InputState(GamePadState state, GamePadState oldState)
        {
            State = state;
            OldState = oldState;
        }

        // TODO: helpers


        internal bool ButtonClicked(Buttons buttons)
        {
            throw new NotImplementedException();
        }
    }
}
