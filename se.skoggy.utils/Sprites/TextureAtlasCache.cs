using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Sprites;

namespace se.skoggy.utils.Sprites
{
    public class TextureAtlasCache
    {
        private readonly string _atlasesPath;
        private readonly GraphicsDevice _device;
        private readonly ContentManager _content;
        private readonly List<DynamicTextureAtlasData> _atlases;
        private readonly List<DynamicTextureAtlasManager> _atlasManager; 

        public TextureAtlasCache(string atlasesPath, GraphicsDevice device, ContentManager content, List<DynamicTextureAtlasData> atlases)
        {
            if (atlasesPath == null) throw new ArgumentNullException("atlasesPath");
            if (device == null) throw new ArgumentNullException("device");
            if (content == null) throw new ArgumentNullException("content");
            if (atlases == null) throw new ArgumentNullException("atlases");
            _atlasesPath = atlasesPath;
            _device = device;
            _content = content;
            _atlases = atlases;
            _atlasManager = new List<DynamicTextureAtlasManager>();
        }

        public DynamicTextureAtlasManager GetAtlas(string name)
        {
            if (!AtlasExists(name))
                LoadAtlas(name);
            return FindAtlas(name);
        }

        private DynamicTextureAtlasManager FindAtlas(string name)
        {
            return _atlasManager.FirstOrDefault(x => x.ContainsAtlasWithName(name));
        }

        private void LoadAtlas(string name)
        {
            foreach (var atlasData in _atlases)
            {
                if (atlasData.Name == name)
                {
                    var atlas = new DynamicTextureAtlasManager(_content);
                    atlas.RegisterAsNonPreCompiledAsset(_device, string.Format("{0}/{1}", _atlasesPath, name));
                    _atlasManager.Add(atlas);
                    break;
                }
            }
        }

        private bool AtlasExists(string name)
        {
            return _atlasManager.Any(dynamicTextureAtlasManager => dynamicTextureAtlasManager.ContainsAtlasWithName(name));
        }
    }
}
