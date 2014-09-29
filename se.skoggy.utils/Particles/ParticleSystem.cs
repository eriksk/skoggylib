using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Cameras;
using se.skoggy.utils.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Particles
{
    public class ParticleSystem
    {
        ParticleSystemSettings settings;
        List<ParticleEmitter> emitters;
        
        public Vector2 position;
        public float rotation;
        public float scale;

        public ParticleSystem(ParticleSystemSettings settings)
        {
            position = Vector2.Zero;
            rotation = 0f;
            scale = 1f;
            Initialize(settings);
        }

        public void Initialize(ParticleSystemSettings settings)
        {
            emitters = new List<ParticleEmitter>();
            foreach (var emitterSettings in settings.emitters)
            {
                emitters.Add(new ParticleEmitter(emitterSettings));
            }
        }

        public bool Done 
        {
            get
            {
                bool allDone = true;
                foreach (var emitter in emitters)
                {
                    if (emitter.settings.loop)
                        return false;
                    if (!emitter.Done)
                        allDone = false;
                }
                return allDone && AllEmittersAreDone;
            }
        }

        public bool AllEmittersAreDone 
        {
            get
            {
                for (int i = 0; i < emitters.Count; i++)
                {
                    if (!emitters[i].Done)
                        return false;
                }
                return true;
            }
        }

        public void Reset()
        {
            foreach (var emitter in emitters)
            {
                emitter.Reset();
            }
        }

        public void Stop()
        {
            foreach (var emitter in emitters)
            {
                emitter.Stop();
            }
        }

        public void Play()
        {
            foreach (var emitter in emitters)
            {
                emitter.Play();
            }
        }

        public void ResetAndRecreate()
        {
            foreach (var emitter in emitters)
            {
                emitter.ResetAndRecreate();
            }
        }
        
        public void Update(float dt)
        {
            foreach (var emitter in emitters)
            {
                emitter.Update(dt);                
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera cam, Rectangle[] sources, GameObject template)
        {
            foreach (var emitter in emitters)
            {
                emitter.Draw(cam, spriteBatch, this, sources, template);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera cam, Rectangle[] sources, GameObject template, Effect effect)
        {
            foreach (var emitter in emitters)
            {
                emitter.Draw(cam, spriteBatch, this, sources, template, effect);
            }
        }
    }
}
