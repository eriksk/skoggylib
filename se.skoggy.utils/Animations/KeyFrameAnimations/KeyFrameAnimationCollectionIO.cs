using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace se.skoggy.utils.Animations.KeyFrameAnimations
{
    public class KeyFrameAnimationCollectionIO
    {
        public void Save(string path, KeyFrameAnimationCollection animations)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(animations, Formatting.Indented));
        }

        public KeyFrameAnimationCollection Load(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<KeyFrameAnimationCollection>(json);
        }
    }
}
