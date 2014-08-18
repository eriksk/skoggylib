using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Particles
{
    public class ParticleSystemLoader
    {
        ContentManager content;

        public ParticleSystemLoader(ContentManager content)
        {
            this.content = content;
        }

        public ParticleSystemSettings Load(string path) 
        {
            // TODO:
            return new ParticleSystemSettings(new ParticleEmitterSettings[]{});
        }
    }
}
