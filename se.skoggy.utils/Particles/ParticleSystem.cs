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

        public ParticleSystem(ParticleSystemSettings settings)
        {
            this.settings = settings;
            Initialize();
        }

        private void Initialize()
        {
            emitters = new List<ParticleEmitter>();
            foreach (var emitterSettings in settings.emitters)
            {
                emitters.Add(new ParticleEmitter(emitterSettings));
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
                emitter.Draw(cam, spriteBatch, sources, template);
            }
        }
    }
}
