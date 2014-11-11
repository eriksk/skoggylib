using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly List<string> _atlasNames; 

        public DynamicTextureAtlasManager(ContentManager content)
        {
            if (content == null) throw new ArgumentNullException("content");

            _content = content;
            _textures = new Dictionary<string, DynamicTexture>();
            _loader = new DynamicTextureAtlasLoader();
            _atlasNames = new List<string>();
        }

        public bool ContainsAtlasWithName(string name)
        {
            return _atlasNames.Contains(name);
        }

        public void RegisterAsNonPreCompiledAsset(GraphicsDevice device, string fullPath)
        {
            var atlasName = Path.GetFileNameWithoutExtension(fullPath);
            _atlasNames.Add(atlasName);
            var atlasData = _loader.Load(string.Format("{0}.json", fullPath));
            var texture = Texture2D.FromStream(device, new FileStream(string.Format("{0}.png", fullPath), FileMode.Open));
            foreach (var frame in atlasData.frames)
            {
                var assetName = frame.filename.Replace(".png", "");

                if (_textures.ContainsKey(assetName))
                    throw new ArgumentException(string.Format("'{0}' is already defined, loading dynamic atlas '{1}'", assetName, atlasName));

                _textures.Add(assetName, new DynamicTexture(assetName, texture, frame.frame));
            }
        }

        public void Register(string atlasName)
        {
            _atlasNames.Add(atlasName);
            var atlasData = _loader.Load(_content.RootDirectory + "/" + atlasName + ".json");
            var texture = _content.Load<Texture2D>(atlasName);
            foreach (var frame in atlasData.frames)
            {
                var assetName = frame.filename.Replace(".png", "");

                if (_textures.ContainsKey(assetName))
                    throw new ArgumentException(string.Format("'{0}' is already defined, loading dynamic atlas '{1}'", assetName, atlasName));

                _textures.Add(assetName, new DynamicTexture(assetName, texture, frame.frame));
            }
        }

        public DynamicTexture GetTexture(string name)
        {
            if (name == null)
                return null;

            if (!_textures.ContainsKey(name))
                return null;

            return _textures[name];
        }
    }
}
