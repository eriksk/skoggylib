using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Cameras;
using se.skoggy.utils.Lighting.Lights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Lighting
{
    public class Lighting
    {
        private string shaderDirectoryName;

        protected Effect lighting;
        protected Vector4 ambientColor;
        protected float globalLightIntensity;

        protected RenderTarget2D lightMap;

        protected List<Light> lights;
        protected Texture2D lightmapTexture;

        public Lighting(string shaderDirectoryName)
        {
            this.shaderDirectoryName = shaderDirectoryName;
        }

        public void Load(ContentManager content, GraphicsDevice graphicsDevice, int width, int height)
        {
            lights = new List<Light>();
            lightMap = new RenderTarget2D(graphicsDevice, width, height);
            lightmapTexture = content.Load<Texture2D>(string.Format(@"{0}/lightmaps", shaderDirectoryName));
            lighting = content.Load<Effect>(string.Format(@"{0}/lighting", shaderDirectoryName));
            ambientColor = new Vector4(0.3f, 0.3f, 0.3f, 0.7f);
            globalLightIntensity = 0.1f;
        }

        public void Clear() 
        {
            lights.Clear();
        }

        public Light CreateLight(LightType type) 
        {
            var light = new Light(lightmapTexture, type);
            lights.Add(light);
            return light;
        }

        public Effect Effect { get { return lighting; } }
        public Vector4 Ambient { get { return ambientColor; } set { ambientColor = value; } }
        public float GlobalLightIntensity { get { return globalLightIntensity; } set { globalLightIntensity = value; } }

        public void Update(float dt) 
        {
            foreach (var l in lights)
            {
                l.Update(dt);
            }
        }

        private void SetParameters() 
        {
            lighting.Parameters["ambientColor"].SetValue(Ambient);
            lighting.Parameters["globalLightIntensity"].SetValue(GlobalLightIntensity);
        }

        /// <summary>
        /// Creates lightmaps and stuff for the effect and readies it to be passed to a spritebatch
        /// 
        /// Just pass lighting.Effect in spritebatch after calling this method
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="graphicsDevice"></param>
        public void Prepare(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Camera cam) 
        {
            graphicsDevice.SetRenderTarget(lightMap);
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, cam.Projection);
            foreach (var l in lights)
            {
                l.Draw(spriteBatch);
            }
            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(Color.Black);
            graphicsDevice.Textures[1] = lightMap;
            SetParameters();
        }
    }
}
