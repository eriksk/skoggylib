using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Ranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Particles
{
    public class ParticleEmitterSettings
    {
        public BlendState blendState;
        public FloatRange frequency;
        public FloatRange lifeTime;
        public ColorRange startColor, endColor;
        public Vector2Range position;
        public Vector2Range velocity;
        public Vector2Range startScale, endScale;
        public FloatRange rotation;
        public int[] sources;
        public bool loop;
        public int capacity;

        public ParticleEmitterSettings()
        {
            loop = true;
            blendState = BlendState.AlphaBlend;
            frequency = new FloatRange();
            lifeTime = new FloatRange();
            position = new Vector2Range();
            velocity = new Vector2Range();
            startScale = new Vector2Range() { min = Vector2.One, max = Vector2.One };
            endScale = new Vector2Range() { min = Vector2.One, max = Vector2.One };
            rotation = new FloatRange();
            startColor = new ColorRange() { min = Color.White, max = Color.White };
            endColor = new ColorRange() { min = Color.White, max = Color.White };
            sources = new int[] { 0 };
            capacity = 64;
        }

        public int RandomSource()
        {
            return sources[Rand.Next(sources.Length)];
        }
    }
}
