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

        private float sfxVolume;
        private float musicVolume;

        public Audio(ContentManager content, string audioFilesDirectoryName)
        {
            this.content = content;
            this.audioFilesDirectoryName = audioFilesDirectoryName;
            sounds = new Dictionary<string, SoundEffectInstance>();
            sfxVolume = 1f;
            musicVolume = 1f;
        }

        public float SfxVolume { get { return sfxVolume; } set { sfxVolume = value; } }

        public void Register(string name) 
        {
            sounds.Add(name, content.Load<SoundEffect>(string.Format("{0}/{1}", audioFilesDirectoryName, name)).CreateInstance());
        }

        public void Play(string name) 
        {
            sounds[name].Stop();
            sounds[name].Volume = sfxVolume;
            sounds[name].Play();
        }

        public void Play(string name, float pan, float volume)
        {
            sounds[name].Stop();
            sounds[name].Pan = pan;
            sounds[name].Volume = sfxVolume * volume;
            sounds[name].Play();
        }
    }
}
