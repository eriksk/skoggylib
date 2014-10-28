using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using se.skoggy.utils.Sprites;

namespace se.skoggy.utils.Animations.KeyFrameAnimations
{
    public class KeyFrameAnimation
    {
        private List<KeyFrame> _keyFrames;
        private List<FrameEvent> _events;

        public delegate void AnimationEnded();

        public event AnimationEnded OnAnimationEnd;

        public delegate void FrameEventTriggered(FrameEvent @event);

        public event FrameEventTriggered OnFrameEvent;

        private int _currentFrame;

        public KeyFrameAnimation()
        {
            _keyFrames = new List<KeyFrame>();
            _events = new List<FrameEvent>();
        }

        public void Reset()
        {
            Current = 0f;
            _currentFrame = 0;
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

        public List<FrameEvent> Events
        {
            get { return _events; }
            set { _events = value; }
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

            CheckForEvents();

            if (Current >= Duration)
            {
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

        private void CheckForEvents()
        {
            foreach (var @event in _events)
            {
                if (@event.Frame == _currentFrame)
                {
                    if (OnFrameEvent != null)
                        OnFrameEvent(@event);
                }
            }
        }

        public override string ToString()
        {
            return Name ?? "";
        }

        public void Draw(SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, List<Frame> frames, Vector2 position, Vector2 scale, Color color, bool flipped)
        {
            if (_keyFrames.Count > 0 && CurrentFrame != null)
            {
                CurrentFrame.Draw(spriteBatch, atlas, frames, position, scale, color, flipped);
            }
        }
    }

    public class FrameEvent
    {
        public string Name { get; set; }
        public int Frame { get; set; }
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public float FloatValue { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class KeyFrame
    {
        public string Name { get; set; }
        public int FrameId { get; set; }

        public override string ToString()
        {
            return Name;
        }

        private Frame GetFrame(List<Frame> frames)
        {
            foreach (var frame in frames)
            {
                if (frame.ID == FrameId)
                    return frame;
            }
            return null;
        }

        public void Draw(SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, List<Frame> frames, Vector2 position, Vector2 scale, Color color, bool flipped = false)
        {
            var frame = GetFrame(frames);
            if (frame == null)
                return;
            frame.Draw(spriteBatch, atlas, position, scale, color, flipped);
        }
    }

    public class Frame
    {
        public string Name { get; set; }
        public int ID { get; set; }
        private readonly List<FramePart> _parts;

        public Frame()
        {
            _parts = new List<FramePart>();
        }

        public List<FramePart> Parts
        {
            get { return _parts; }
        }


        public override string ToString()
        {
            return Name;
        }

        public void Draw(SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, Vector2 position, Vector2 scale, Color color, bool flipped = false)
        {
            foreach (var part in Parts)
            {
                part.Draw(spriteBatch, atlas, position, scale, color, flipped);
            }
        }

        public Frame Clone(int newId)
        {
            var clone = new Frame()
            {
               ID = newId,
               Name = Name + " copy",
            };
            foreach (var part in Parts)
            {
                clone.Parts.Add(part.Clone());
            }
            return clone;
        }
    }

    public class FramePart
    {
        public Vector2 Position;
        public Vector2 Scale;
        public Vector2 Origin;
        public float Rotation;
        public bool Flipped { get; set; }
        public string TextureName { get; set; }

        public FramePart()
        {
            Scale = Vector2.Zero;
            Origin = new Vector2(0.5f, 0.5f);
            Scale = Vector2.One;
        }

        public FramePart Clone()
        {
            return new FramePart
            {
                Position = Position,
                Scale = Scale,
                Origin = Origin,
                Rotation = Rotation,
                Flipped = Flipped,
                TextureName = TextureName
            };
        }

        public void Draw(SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, Vector2 position, Vector2 scale, Color color, bool flipped = false)
        {
            var dynamicTexture = atlas.GetTexture(TextureName);
            if (dynamicTexture == null)
                return;

            var source = dynamicTexture.Source;

            var originInPixels = new Vector2();

            originInPixels.X = source.Width * Origin.X;
            originInPixels.Y = source.Height * Origin.Y;

            var flip = this.Flipped ? (!flipped) : (flipped);

            Vector2 pos = Position;

            pos = pos * (Scale * scale); //Vector2.Transform(pos * Scale *scale, Matrix.CreateScale(Scale.X * scale.X, Scale.Y * scale.Y, 0f));
            // NO idea what is going on here anymore, but it doesn't work when scale x & y aren't the same

            float rot = Rotation;

            if (flipped)
            {
                pos.X *= -1f;
                rot *= -1f;
            }

            spriteBatch.Draw(
                dynamicTexture.Texture,
                position + pos,
                source,
                color,
                rot,
                originInPixels,
                Scale * scale,
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0f);
        }
    }
}
