using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using se.skoggy.utils.Graphics;
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

        public List<KeyFrame> KeyFrames
        {
            get { return _keyFrames; }
            set { _keyFrames = value; }
        }

        [JsonIgnore]
        public KeyFrame CurrentFrame
        {
            get { return _currentFrame > _keyFrames.Count - 1 ? null : _keyFrames[_currentFrame]; }
        }

        public void Update(float dt)
        {
            Current += dt;

            if (_keyFrames.Count == 0)
                return;

            float durationPerFrame = Duration / _keyFrames.Count;

            if (Current >= durationPerFrame)
            {
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
