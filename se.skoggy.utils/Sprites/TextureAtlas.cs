using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace se.skoggy.utils.Sprites
{
    public class TextureAtlas
    {
        private string _name;
        [JsonIgnore]
        private Texture2D _texture;
        private string _textureName;
        private List<TextureAtlasPart> _parts;

        public TextureAtlas()
        {
        }

        public TextureAtlas(string name, Texture2D texture, string textureName, params TextureAtlasPart[] parts)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (parts == null) throw new ArgumentNullException("parts");

            _name = name;
            _texture = texture;
            _textureName = textureName;
            _parts = new List<TextureAtlasPart>();
            _parts.AddRange(parts);
        }

        [JsonIgnore]
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public List<TextureAtlasPart> Parts
        {
            get { return _parts; }
            set { _parts = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string TextureName
        {
            get { return _textureName; }
            set { _textureName = value; }
        }
    }
}
