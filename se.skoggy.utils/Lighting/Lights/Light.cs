using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Lighting.Lights
{
    public class Light : GameObject
    {
        protected LightType type;

        public Light(Texture2D lightTexture, LightType type)
            :base(lightTexture)
        {
            Type = LightType.Spotlight;
        }

        public LightType Type 
        {
            get 
            { 
                return type;
            }
            set
            {
                type = value;
                switch (type)
	            {
		            case LightType.Spotlight:
                        SetSource(0, 0, 128, 128);
                     break;
                    case LightType.Cone:
                        SetSource(128, 0, 128, 128);
                        origin.X = 0.2f;
                     break;
                    case LightType.Rectangle:
                        SetSource(256, 0, 128, 128);
                     break;
	            }
            }
        }
    }
}
