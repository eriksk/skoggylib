using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Cameras;

namespace se.skoggy.utils.Management
{
    public abstract class Manager
    {
        public abstract void Load(ContentManager content);
        public abstract void Update(float dt);
        public abstract void Draw(SpriteBatch spriteBatch, Camera cam);
    }
}
