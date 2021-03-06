﻿using System;
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

        public void Draw(SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, Color color, bool flipped = false)
        {
            foreach (var part in Parts)
            {
                part.Draw(spriteBatch, atlas, color, flipped);
            }
        }

        public void DrawInterpolated(Interpolation interpolation, float progress, Frame next, SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, Frame frame, Color color, bool flipped)
        {
            for (int i = 0; i < Parts.Count; i++)
            {
                var part = Parts[i];
                var targetPart = next.Parts[i];

                part.DrawAt(
                    new Vector2(
                        interpolation.Apply(part.Position.X, targetPart.Position.X, progress),
                        interpolation.Apply(part.Position.Y, targetPart.Position.Y, progress)), 
                        Utils.MathUtils.AngleLerp(interpolation, part.Rotation, targetPart.Rotation, progress), 
                        spriteBatch,
                        atlas, 
                        color, 
                        flipped);
            }
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
}
