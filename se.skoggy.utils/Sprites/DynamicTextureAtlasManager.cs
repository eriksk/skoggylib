using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace se.skoggy.utils.Sprites
{
    public class DynamicTextureAtlasManager
    {
        private readonly ContentManager _content;
        private readonly Dictionary<string, DynamicTexture> _textures;
        private readonly DynamicTextureAtlasLoader _loader;

        public DynamicTextureAtlasManager(ContentManager content)
        {
            if (content == null) throw new ArgumentNullException("content");

            _content = content;
            _textures = new Dictionary<string, DynamicTexture>();
            _loader = new DynamicTextureAtlasLoader();
        }

        public void Register(string atlasName)
        {
            var atlasData = _loader.Load(_content.RootDirectory + "/" + atlasName + ".json");
            var texture = _content.Load<Texture2D>(atlasName);
            foreach (var frame in atlasData.frames)
            {
                var assetName = frame.filename.Replace(".png", "");

                if (_textures.ContainsKey(assetName))
                    throw new ArgumentException(assetName + " is already defined, loading dynamic atlas " + atlasName);

                _textures.Add(assetName, new DynamicTexture(assetName, texture, frame.frame));
            }
        }

        public DynamicTexture GetTexture(string name)
        {
            return _textures[name];
        }
    }
}
