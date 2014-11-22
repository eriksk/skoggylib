using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using se.skoggy.utils.Graphics;
using se.skoggy.utils.Interpolations;
using se.skoggy.utils.MathUtils;
using se.skoggy.utils.Sprites;

namespace se.skoggy.utils.Animations.KeyFrameAnimations
{
    public class KeyFrameAnimation
    {
        private List<KeyFrame> _keyFrames;
        public delegate void AnimationEnded();
        public event AnimationEnded OnAnimationEnd;
        public delegate void FrameEventTriggered(FrameEvent @event);
        public event FrameEventTriggered OnFrameEvent;
        private int _currentFrame;

        public KeyFrameAnimation()
        {
            _keyFrames = new List<KeyFrame>();
            Transform = new Transform();
            AnimationInterpolationType = AnimationInterpolationType.Linear;
            Loop = true;
        }

        public void Reset()
        {
            Current = 0f;
            _currentFrame = 0;
        }

        public Transform Transform { get; set; }

        private Matrix CreateRenderMatrix()
        {
            return Transform.Matrix;
        }

        public string Name { get; set; }
        public float Duration { get; set; }
        [JsonIgnore]
        public float Current { get; set; }
        [JsonIgnore]
        public float FrameProgress
        {
            get
            {
                float durationPerFrame = Duration / _keyFrames.Count;
                return Current / durationPerFrame;
            }
        }

        public AnimationInterpolationType AnimationInterpolationType { get; set; }

        public List<KeyFrame> KeyFrames
        {
            get { return _keyFrames; }
            set { _keyFrames = value; }
        }
        public bool Loop { get; set; }

        [JsonIgnore]
        public KeyFrame CurrentFrame
        {
            get { return _currentFrame > _keyFrames.Count - 1 ? null : _keyFrames[_currentFrame]; }
        }
        [JsonIgnore]
        public KeyFrame NextFrame
        {
            get { return _keyFrames.Count == 0 ? null : (_currentFrame + 1 > _keyFrames.Count - 1 ? (Loop ? _keyFrames[0] : CurrentFrame) : _keyFrames[_currentFrame + 1]); }
        }

        public KeyFrame GetNextFrameRegardless()
        {
            return _currentFrame > _keyFrames.Count - 1 ? _keyFrames[0] : _keyFrames[_currentFrame];
        }

        public void Update(float dt, bool trigEvents = true)
        {
            Current += dt;

            if (_keyFrames.Count == 0)
                return;

            float durationPerFrame = Duration / _keyFrames.Count;

            if (Current >= durationPerFrame)
            {
                if (trigEvents)
                    TrigEvents();

                Current = 0f;
                _currentFrame++;
                if (_currentFrame > _keyFrames.Count - 1)
                {
                    if (OnAnimationEnd != null)
                        OnAnimationEnd();
                    Current = 0f;
                    _currentFrame = 0;
                }
            }
        }

        private void TrigEvents()
        {
            foreach (var @event in CurrentFrame.Events)
            {
                if (OnFrameEvent != null)
                    OnFrameEvent(@event);
            }
        }

        public override string ToString()
        {
            return Name ?? "";
        }

        public void Draw(SpriteBatch spriteBatch, Matrix view, DynamicTextureAtlasManager atlas, List<Frame> frames, Color color, bool flipped)
        {
            if (_keyFrames.Count > 0 && CurrentFrame != null)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, CreateRenderMatrix() * view);
                CurrentFrame.Draw(spriteBatch, atlas, frames, color, flipped);
                spriteBatch.End();
            }
        }

        public void DrawInterpolated(SpriteBatch spriteBatch, Matrix view, DynamicTextureAtlasManager atlas, List<Frame> frames, Color color, bool flipped)
        {
            if (_keyFrames.Count > 0 && CurrentFrame != null)
            {
                Interpolation interpolation = Interpolation.Linear;
                switch (AnimationInterpolationType)
                {
                    case AnimationInterpolationType.Linear:
                        break;
                    case AnimationInterpolationType.Smooth:
                        interpolation = Interpolation.Pow2;
                        break;
                }

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, null, null, null, CreateRenderMatrix() * view);
                CurrentFrame.DrawInterpolated(interpolation, FrameProgress, NextFrame, spriteBatch, atlas, frames, color, flipped);
                spriteBatch.End();
            }
        }

        public void Draw(SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, List<Frame> frames, Vector2 position, Vector2 scale, Color color, bool flipped)
        {
            if (_keyFrames.Count > 0 && CurrentFrame != null)
            {
                CurrentFrame.Draw(spriteBatch, atlas, frames, position, scale, color, flipped);
            }
        }

        public void DrawDebugPoints(List<Frame> frames, SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, DrawableRectangle point, Vector2 position, Vector2 scale)
        {
            if (_keyFrames.Count > 0 && CurrentFrame != null)
            {
                CurrentFrame.DrawDebugPoints(frames, spriteBatch, atlas, point, position, scale);
            }
        }
    }
}
