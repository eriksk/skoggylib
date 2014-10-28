using se.skoggy.utils.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Animations
{
    public class FrameStepAnimation
    {
        private readonly int[] frames;
        private readonly TimerTrig trig;
        private int currentFrame;

        public delegate void AnimationEnd();
        public event AnimationEnd OnAnimationEnd;

        public FrameStepAnimation(int[] frames, float interval, string name = "")
        {
            this.frames = frames;
            trig = new TimerTrig(interval);
            Name = name;
        }

        public string Name { get; set; }

        public virtual int Frame
        {
            get { return frames[currentFrame]; }
        }

        public void Reset()
        {
            currentFrame = 0;
            trig.Reset();
        }

        public virtual void Update(float dt)
        {
            if (!trig.IsTrigged(dt))
                return;

            currentFrame++;
            if (currentFrame >= frames.Length)
            {
                currentFrame = 0;
                if (OnAnimationEnd != null)
                    OnAnimationEnd();
            }
        }
    }
}
