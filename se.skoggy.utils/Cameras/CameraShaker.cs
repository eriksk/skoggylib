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
        private Vector2 _offset, _target;
        private float _rotation;
        private float targetWait;

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
            _offset = Vector2.Zero;
            _rotation = 0f;
            NewTarget();
        }

        private void NewTarget()
        {
            _target.X = Rand.Next(-1f, 1f) * mag * 0.01f;
            _target.Y = Rand.Next(-1f, 1f) * mag * 0.01f;
            targetWait = 150f;
        }

        public void Update(float dt, Camera cam)
        {
            current += dt;
            if (current < duration)
            {
                targetWait -= dt;
                if(targetWait < 0f)
                    NewTarget();

                _offset += _target * dt;
                _rotation += Rand.Next(-1f, 1f) * (mag * 0.01f) *0.001f*dt;

                cam.Offset = _offset * (1f - (current / duration));
                cam.OffsetRotation = _rotation * (1f - (current / duration));
            }
            else
            {
                cam.Offset = Vector2.Zero;
                cam.OffsetRotation = 0f;
            }
        }
    }
}
