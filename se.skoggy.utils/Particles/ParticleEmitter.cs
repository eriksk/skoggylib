﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Cameras;
using se.skoggy.utils.GameObjects;
using se.skoggy.utils.Management;
using se.skoggy.utils.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Particles
{
    public class ParticleEmitter
    {
        public ParticleEmitterSettings settings;
        public TimerTrig spawnTimer;
        public Pool<Particle> particles;
        private int spawned;
        private bool running;

        public ParticleEmitter(ParticleEmitterSettings settings)
        {
            this.settings = settings;
            spawnTimer = new TimerTrig(settings.frequency.Random());
            particles = new Pool<Particle>(settings.capacity);
            Reset();
        }

        public void Reset()
        {
            spawned = 0;
            running = false;
            Done = false;
            particles.Clear();
        }

        public void Stop() 
        {
            running = false;
        }

        public void Play() 
        {
            running = true;
            Done = false;
        }

        public void ResetAndRecreate()
        {
            Reset();
            particles = new Pool<Particle>(settings.capacity);
        }

        private void Spawn()
        {
            // TODO: parenting of particles to enable transforms for children, let them be like unity so world or local space
            for (int i = 0; i < settings.spawnCount; i++)
            {
                if (particles.Count >= settings.capacity)
                    return;

                Particle p = particles.Pop();
                p.position.X = settings.position.RandomX();
                p.position.Y = settings.position.RandomY();
                p.velocity.X = settings.velocity.RandomX();
                p.velocity.Y = settings.velocity.RandomY();
                Vector2 startScale = settings.startScale.Random();
                p.startScale.X = startScale.X;
                p.startScale.Y = startScale.Y;
                Vector2 endScale = settings.endScale.Random();
                p.endScale.X = endScale.X;
                p.endScale.Y = endScale.Y;
                p.scale.X = p.startScale.X;
                p.scale.Y = p.startScale.Y;
                p.source = settings.RandomSource();
                p.rotation = settings.rotation.Random();
                p.current = 0f;
                p.duration = settings.lifeTime.Random();
                p.startColor = settings.startColor.Random();
                p.endColor = settings.endColor.Random();
                p.color = p.startColor;

                spawned++;
            }

            spawnTimer.Set(settings.frequency.Random());
        }

        public bool Done { get; private set; }

        public void Update(float dt) 
        {
            if (running && spawnTimer.IsTrigged(dt)) 
            {
                if (settings.loop)
                {
                    Spawn();
                }
                else 
                {
                    if (spawned >= settings.capacity)
                    {
                        if(particles.Count == 0)
                            Done = true;
                    }
                    else
                    {
                        Spawn();
                    }
                }
            }

            for (int i = 0; i < particles.Count; i++)
            {
                Particle p = particles[i];
                p.current += dt;
                if (p.current > p.duration)
                {
                    particles.Push(i--);
                }
                else 
                {
                    float progress = p.current / p.duration;

                    p.velocity.X += settings.gravity.force.X * dt;
                    p.velocity.Y += settings.gravity.force.Y * dt;

                    p.position.X += p.velocity.X * dt;
                    p.position.Y += p.velocity.Y * dt;

                    p.scale.X = MathHelper.Lerp(p.startScale.X, p.endScale.X, progress);
                    p.scale.Y = MathHelper.Lerp(p.startScale.Y, p.endScale.Y, progress);

                    // TODO: start and end of all properties
                    p.color.R = (byte)MathHelper.Lerp(p.startColor.R, p.endColor.R, progress);
                    p.color.G = (byte)MathHelper.Lerp(p.startColor.G, p.endColor.G, progress);
                    p.color.B = (byte)MathHelper.Lerp(p.startColor.B, p.endColor.B, progress);
                    p.color.A = (byte)MathHelper.Lerp(p.startColor.A, p.endColor.A, progress);
                }
            }
        }
        public void Draw(Camera cam, SpriteBatch spriteBatch, ParticleSystem parent, Rectangle[] sources, GameObject template)
        {
            Draw(cam, spriteBatch, parent, sources, template, null);
        }

        public void Draw(Camera cam, SpriteBatch spriteBatch, ParticleSystem parent, Rectangle[] sources, GameObject template, Effect effect)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, settings.blendState, null, null, null, effect, cam.Projection);
            for (int i = 0; i < particles.Count; i++)
            {
                Particle p = particles[i];

                Vector2 rotatedPosition = Vector2.Transform(p.position, Matrix.CreateRotationZ(parent.rotation));

                template.position.X = parent.position.X + rotatedPosition.X * parent.scale;
                template.position.Y = parent.position.Y + rotatedPosition.Y * parent.scale;
                template.rotation = parent.rotation + p.rotation;
                Rectangle source = sources[p.source];
                template.SetSource(source.X, source.Y, source.Width, source.Height);
                template.scale.X = p.scale.X * parent.scale;
                template.scale.Y = p.scale.Y * parent.scale;
                template.color.R = p.color.R;
                template.color.G = p.color.G;
                template.color.B = p.color.B;
                template.color.A = p.color.A;
                template.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

    }
}
