using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace se.skoggy.utils.Cameras
{
    public class CameraShaker
    {
        private float current, duration;
        private float mag;

        public CameraShaker()
        {
            current = 0;
            duration = 0;

        }

        public void Start(float duration, float mag)
        {
            this.mag = mag;
            this.duration = duration;
            current = 0f;
        }

        public void Update(float dt, Camera cam)
        {
            current += dt;
            if (current < duration)
            {
                cam.Offset = new Vector2(Rand.Next(-1f, 1f), Rand.Next(-1f, 1f)) * mag *  (1f - (current / duration));
            }
            else
            {
                cam.Offset = Vector2.Zero;
            }
        }
    }
}
