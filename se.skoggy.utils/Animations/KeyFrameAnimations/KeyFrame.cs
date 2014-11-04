using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Graphics;
using se.skoggy.utils.Interpolations;
using se.skoggy.utils.Sprites;

namespace se.skoggy.utils.Animations.KeyFrameAnimations
{
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


        public void Draw(SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, List<Frame> frames,  Color color, bool flipped = false)
        {
            var frame = GetFrame(frames);
            if (frame == null)
                return;
            frame.Draw(spriteBatch, atlas, color, flipped);
        }

        public void DrawInterpolated(Interpolation interpolation, float progress, KeyFrame next, SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, List<Frame> frames, Color color, bool flipped)
        {
            var frame = GetFrame(frames);
            var nextFrame = next.GetFrame(frames);

            if (frame == null)
                return;
            if (nextFrame == null)
                return;
            if (frame.Parts.Count != nextFrame.Parts.Count)
                return;

            frame.DrawInterpolated(interpolation, progress, nextFrame, spriteBatch, atlas, frame, color, flipped);
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
}
