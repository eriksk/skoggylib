using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Shaders
{
    public abstract class Shader<T> where T : ShaderParameters
    {
        protected Effect effect;
        protected T parameters;
        protected string name;

        public Shader(string name, T parameters)
        {
            this.name = name;
            this.parameters = parameters;
        }

        public Effect Effect { get { return effect; } }
        public T Parameters 
        {
            get { return parameters; }
            set { parameters = value; }
        }

        public virtual void Load(ShaderLoader shaderLoader) 
        {
            effect = shaderLoader.LoadEffect(name);
        }

        public virtual void Update(float dt) 
        {
            parameters.ApplyTo(effect);
        }

        public virtual void Begin(GraphicsDevice device, SpriteBatch spriteBatch){}
        public virtual void End(GraphicsDevice device, SpriteBatch spriteBatch){}
    }
}
