using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Animations.KeyFrameAnimations
{
    public class KeyFrameAnimationCollection
    {
        private readonly List<KeyFrameAnimation> _keyFrameAnimations;
        private readonly List<Frame> _frames; 

        public KeyFrameAnimationCollection()
        {
            _keyFrameAnimations = new List<KeyFrameAnimation>();
            _frames = new List<Frame>();
        }

        public List<KeyFrameAnimation> Animations 
        {
            get { return _keyFrameAnimations; }
        }

        public List<Frame> Frames
        {
            get { return _frames; }
        }

        public int GetNewUniqueFrameId()
        {
            return _frames.Max(x => x.ID) + 1;
        }
    }
}
