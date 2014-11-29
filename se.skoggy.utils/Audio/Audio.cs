using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace se.skoggy.utils.Audio
{
    public class Audio : IAudio
    {
        protected ContentManager content;
        protected string audioFilesDirectoryName;
        protected Dictionary<string, SoundEffectInstance> sounds;
        protected Dictionary<string, SoundEffectInstance> songs;

        private float sfxVolume;
        private float musicVolume;

        public Audio(ContentManager content, string audioFilesDirectoryName)
        {
            this.content = content;
            this.audioFilesDirectoryName = audioFilesDirectoryName;
            sounds = new Dictionary<string, SoundEffectInstance>();
            songs = new Dictionary<string, SoundEffectInstance>();
            sfxVolume = 1f;
            musicVolume = 1f;
        }

        public void Update()
        {
        }

        public void SetEffect(string name, float value)
        {
            
        }

        public float SfxVolume { get { return sfxVolume; } set { sfxVolume = value; } }

        public float MusicVolume
        {
            get { return musicVolume; }
            set { musicVolume = value; }
        }

        public void Register(string name) 
        {
            sounds.Add(name, content.Load<SoundEffect>(string.Format("{0}/{1}", audioFilesDirectoryName, name)).CreateInstance());
        }

        public void RegisterSong(string name)
        {
            songs.Add(name, content.Load<SoundEffect>(string.Format("{0}/{1}", audioFilesDirectoryName, name)).CreateInstance());
        }

        public void Play(string name)
        {
            sounds[name].Stop();
            sounds[name].Pitch = 0f;
            sounds[name].Volume = sfxVolume;
            sounds[name].IsLooped = false;
            sounds[name].Play();
        }
        public void Play(string name, float pitch)
        {
            sounds[name].Stop();
            sounds[name].Pitch = pitch;
            sounds[name].Volume = sfxVolume;
            sounds[name].IsLooped = false;
            sounds[name].Play();
        }

        public void PlayLoopedSound(string name)
        {
            sounds[name].Volume = sfxVolume;
            sounds[name].IsLooped = true;
            sounds[name].Play();
        }

        public void StopLoopedSound(string name)
        {
            sounds[name].Stop();
        }

        public void Play(string name, float pan, float volume)
        {
            sounds[name].Stop();
            sounds[name].Pan = pan;
            sounds[name].Volume = sfxVolume * volume;
            sounds[name].Play();
        }

        public void PlaySong(string name)
        {
            var song = songs[name];
            song.Volume = MusicVolume;
            song.IsLooped = true;
            song.Play();
        }

        public void StopSong(string name)
        {
            var song = songs[name];
            song.Stop();
        }

        public void StopAllSongs()
        {
            foreach (var song in songs)
            {
                song.Value.Stop();
            }
        }

        public void Dispose()
        {
            
        }
    }
}
