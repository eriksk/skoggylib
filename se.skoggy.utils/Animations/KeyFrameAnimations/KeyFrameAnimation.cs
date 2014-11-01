using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using se.skoggy.utils.Graphics;
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

    public class KeyFrame
    {
        public string Name { get; set; }
        public int FrameId { get; set; }
        private List<FrameEvent> _events;

        public KeyFrame()
        {
            _events = new List<FrameEvent>();
        }

        public List<FrameEvent> Events
        {
            get { return _events; }
            set { _events = value; }
        }

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

        public void DrawDebugPoints(List<Frame> frames, SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, DrawableRectangle point, Vector2 position, Vector2 scale)
        {
            var frame = GetFrame(frames);
            if (frame == null)
                return;

            frame.DrawDebugPoints(spriteBatch, atlas, point, position, scale);
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

        public Vector2 Centroid()
        {
            Vector2 centroid = Vector2.Zero;

            foreach (var part in Parts)
            {
                centroid.X += part.Position.X;
                centroid.Y += part.Position.Y;
            }

            int points = Parts.Count;
            centroid.X = centroid.X / points;
            centroid.Y = centroid.Y / points;

            return centroid;
        }

        public void Draw(SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, Vector2 position, Vector2 scale, Color color, bool flipped = false)
        {
            foreach (var part in Parts)
            {
                part.Draw(spriteBatch, atlas, position, scale, color, flipped);
            }
        }

        public void DrawDebugPoints(SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, DrawableRectangle point, Vector2 position, Vector2 scale)
        {
            foreach (var part in Parts)
            {
                part.DrawDebugPoint(spriteBatch, atlas, point, position, scale);
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

        public Matrix GetLocalTransform(Rectangle source, bool flip)
        {
            // Transform = -Origin * Scale * Rotation * Translation
            return// Matrix.CreateTranslation(-source.Width * Origin.X, -source.Height * Origin.Y, 0f) *
                   Matrix.CreateScale(Scale.X, Scale.Y, 1f) *
                  //Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateTranslation(Position.X * (flip ? -1f: 1f), Position.Y, 0f);
        }
        
        public static void DecomposeMatrix(ref Matrix matrix, out Vector2 position, out float rotation, out Vector2 scale)
        {
            Vector3 position3, scale3;
            Quaternion rotationQ;
            matrix.Decompose(out scale3, out rotationQ, out position3);
            Vector2 direction = Vector2.Transform(Vector2.UnitX, rotationQ);
            rotation = (float)Math.Atan2(direction.Y, direction.X);
            position = new Vector2(position3.X, position3.Y);
            scale = new Vector2(scale3.X, scale3.Y);
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

            var parent = (Matrix.Identity * Matrix.CreateScale(scale.X, scale.Y, 1f) * Matrix.CreateTranslation(position.X, position.Y, 0f));

            Matrix globalTransform = GetLocalTransform(source, flipped) * parent;

            Vector2 lposition, lscale;
            float lrotation;
            DecomposeMatrix(ref globalTransform, out lposition, out lrotation, out lscale);

            float rot = Rotation;

            if (flipped)
            {
                //lposition.X *= -1f;
                rot *= -1f;
            }

            var originInPixels = new Vector2();
            originInPixels.X = source.Width * Origin.X;
            originInPixels.Y = source.Height * Origin.Y;

            var flip = this.Flipped ? (!flipped) : (flipped);


            // IT works properly now. We just have to "rotate" the scaling, so that it scales rotated properly

            spriteBatch.Draw(
                dynamicTexture.Texture,
                lposition, 
                source, 
                Color.White,
                rot,
                originInPixels,
                Scale * scale,
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 
                0.0f);

            /*
            
            Vector2 scaleFactor = Scale * scale;

            var originInPixels = new Vector2();
            originInPixels.X = source.Width * Origin.X;
            originInPixels.Y = source.Height * Origin.Y;

            var flip = this.Flipped ? (!flipped) : (flipped);

            Vector2 pos = Position ;

            float rot = Rotation;

            if (flipped)
            {
                pos.X *= -1f;
                rot *= -1f;
            }
            

            spriteBatch.Draw(
                dynamicTexture.Texture,
                position + ScaleTargetPosition(pos, Vector2.Zero, scaleFactor), 
                source,
                color,
                rot,
                originInPixels,
                scaleFactor,
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0f);
             * */
        }

        public void DrawDebugPoint(SpriteBatch spriteBatch,DynamicTextureAtlasManager atlas, DrawableRectangle point, Vector2 position, Vector2 scale)
        {

            var dynamicTexture = atlas.GetTexture(TextureName);
            if (dynamicTexture == null)
                return;

            var source = dynamicTexture.Source;

            var parent = (Matrix.Identity * Matrix.CreateScale(scale.X, scale.Y, 1f) * Matrix.CreateTranslation(position.X, position.Y, 0f));

            Matrix globalTransform = Matrix.CreateTranslation(Position.X, Position.Y, 0f) * parent;

            Vector2 lposition, lscale;
            float lrotation;
            DecomposeMatrix(ref globalTransform, out lposition, out lrotation, out lscale);
            

            point.X = (int)lposition.X - point.Width / 2;
            point.Y = (int)lposition.Y - point.Height / 2;
            
            point.Draw(spriteBatch);
        }

        private Vector2 ScaleTargetPosition(Vector2 target, Vector2 point, Vector2 scaleMag)
        {
            Vector2 scalePosition = Vector2.Transform(target - point, Matrix.CreateScale(scaleMag.X, scaleMag.Y, 1f));
            return scalePosition + point;
        }
    }
}
