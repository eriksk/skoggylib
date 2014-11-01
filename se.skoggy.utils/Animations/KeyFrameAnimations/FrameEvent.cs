using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Animations.KeyFrameAnimations
{
    public class FrameEvent
    {
        public string Name { get; set; }
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public float FloatValue { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
