using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Cameras;
using se.skoggy.utils.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Particles
{
    public class ParticleManager
    {
        List<ParticleSystem> systems;
        GameObject template;
        Rectangle[] sources;

        public ParticleManager()
        {
            systems = new List<ParticleSystem>();
        }

        public void Load(ContentManager content)
        {
            template = new GameObject(content.Load<Texture2D>(@"particles"));
            sources = new Rectangle[]
            {
                new Rectangle(0, 0, 128, 128)
            };
        }

        public void AddSystem(ParticleSystem particleSystem)
        {
            systems.Add(particleSystem);
        }

        public void Update(float dt)
        {
            foreach (var s in systems)
            {
                s.Update(dt);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera cam)
        {
            foreach (var s in systems)
            {
                s.Draw(spriteBatch, cam, sources, template);
            }
        }
    }
}
