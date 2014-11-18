using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using se.skoggy.utils.Interpolations;

namespace se.skoggy.utils.Tweening.Stock
{
    public class AlphaTween: Tween
    {
        float start;
        float end;

        public AlphaTween(ITweenable subject, Interpolation interpolation, float duration, float start, float end)
            :base(subject, interpolation, duration)
        {
            this.start = start;
            this.end = end;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            subject.SetAlpha(interpolation.Apply(start, end, Progress));
        }
    }
}
