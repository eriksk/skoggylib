using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Cameras;
using se.skoggy.utils.GameObjects;

namespace se.skoggy.utils.Particles
{
    public class PoolableParticleManager
    {
        private readonly List<ParticleSystem> _systemInstances;
        private Dictionary<string, ParticleSystemSettings> _systemSettings;

        private GameObject _template;
        private Rectangle[] _sources;

        public PoolableParticleManager()
        {
            _systemInstances = new List<ParticleSystem>();
            _systemSettings = new Dictionary<string, ParticleSystemSettings>();
        }

        public void Load(ContentManager content)
        {
            _template = new GameObject(content.Load<Texture2D>(@"gfx/particles"));

            const int cellSize = 128;
            int cols = _template.texture.Width / cellSize;
            int rows = _template.texture.Height / cellSize;

            _sources = new Rectangle[cols * rows];
            for (int i = 0; i < cols; i++)
			{
                for (int j = 0; j < rows; j++)
			    {
                    _sources[i + j * cols] = new Rectangle(i * cellSize, j * cellSize, cellSize, cellSize);			 
			    }
			}
        }

        public int ActiveSystems
        {
            get { return _systemInstances.Count; }
        }

        public void AddSystem(string name, ParticleSystemSettings systemSettings)
        {
            _systemSettings.Add(name, systemSettings);
        }

        public ParticleSystem StartSystem(string name, Vector2 position, float rotation = 0f, float scale = 1f)
        {
            // TODO: pool this
            var system = new ParticleSystem(_systemSettings[name]);
            system.position = position;
            system.rotation = rotation;
            system.scale = scale;
            system.Play();
            _systemInstances.Add(system);
            return system;
        }

        public void Update(float dt)
        {
            for (int i = 0; i < _systemInstances.Count; i++)
            {
                var s = _systemInstances[i];
                s.Update(dt);
                if (s.Done)
                    _systemInstances.RemoveAt(i--);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera cam)
        {
            foreach (var s in _systemInstances)
            {
                s.Draw(spriteBatch, cam, _sources, _template);
            }
        }
        public void Draw(SpriteBatch spriteBatch, Camera cam, Effect effect)
        {
            foreach (var s in _systemInstances)
            {
                s.Draw(spriteBatch, cam, _sources, _template, effect);
            }
        }
    }
}
