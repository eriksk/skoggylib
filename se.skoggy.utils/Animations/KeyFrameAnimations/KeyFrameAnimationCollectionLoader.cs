using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;

namespace se.skoggy.utils.Animations.KeyFrameAnimations
{
    public class KeyFrameAnimationCollectionLoader
    {
        private readonly ContentManager _content;

        public KeyFrameAnimationCollectionLoader(ContentManager content)
        {
            if (content == null) throw new ArgumentNullException("content");
            _content = content;
        }

        public KeyFrameAnimationCollection Load(string name)
        {
            var json = File.ReadAllText(_content.RootDirectory + string.Format("/{0}.json", name));
            return JsonConvert.DeserializeObject<KeyFrameAnimationCollection>(json);
        }
    }
}
