using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Audio
{
    public class Audio
    {
        protected ContentManager content;
        protected string audioFilesDirectoryName;
        protected Dictionary<string, SoundEffectInstance> sounds;

        public Audio(ContentManager content, string audioFilesDirectoryName)
        {
            this.content = content;
            this.audioFilesDirectoryName = audioFilesDirectoryName;
            sounds = new Dictionary<string, SoundEffectInstance>();
        }

        public void Register(string name) 
        {
            sounds.Add(name, content.Load<SoundEffect>(string.Format("{0}/{1}", audioFilesDirectoryName, name)).CreateInstance());
        }

        public void Play(string name) 
        {
            sounds[name].Stop();
            sounds[name].Play();
        }
    }
}
