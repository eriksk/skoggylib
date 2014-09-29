using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace se.skoggy.utils.Sprites
{
    public class TextureAtlasPart
    {
        [JsonIgnore]
        private TextureAtlas _atlas;
        private Rectangle _source;
        private string _name;

        public TextureAtlasPart()
        {
        }

        public TextureAtlasPart(TextureAtlas atlas, Rectangle source, string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            _atlas = atlas;
            _source = source;
            Name = name;
        }

        public Rectangle Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [JsonIgnore]
        public TextureAtlas Atlas
        {
            get { return _atlas; }
            set { _atlas = value; }
        }
    }
}