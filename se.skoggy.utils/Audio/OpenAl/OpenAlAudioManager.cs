using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace se.skoggy.utils.Audio.OpenAl
{
    public class OpenAlAudioManager : IAudio
    {
        private readonly ContentManager _content;
        private readonly string _audioDirectoryName;
        private readonly Dictionary<string, Sound> _sounds;
        private readonly Dictionary<string, AudioEffect> _effects;
        private readonly AudioContext _context;
        private readonly EffectsExtension _efx;

        public OpenAlAudioManager(ContentManager content, string audioDirectoryName)
        {
            if (content == null) throw new ArgumentNullException("content");
            if (audioDirectoryName == null) throw new ArgumentNullException("audioDirectoryName");
            
            _content = content;
            _audioDirectoryName = audioDirectoryName;

            // destroy previous context, specifically because monogame creates one
            var context = Alc.GetCurrentContext();
            Alc.DestroyContext(context);
            
            _context = new AudioContext();
            _context.MakeCurrent();
            
            _efx = new EffectsExtension();
            _sounds = new Dictionary<string, Sound>();
            _effects = new Dictionary<string, AudioEffect>();
            if(_efx.IsInitialized)
                _effects.Add("lowpassfilter", new LowPassFilterAudioEffect(_efx));
        }
        
        public void Dispose()
        {
            foreach (var sound in _sounds)
                sound.Value.Dispose();
            foreach (var audioEffect in _effects)
            {
                audioEffect.Value.Dispose();
            }
            _context.Dispose();
        }

        public void SetEffect(string name, float value)
        {
            GetEffect(name).SetValue(value);
        }

        public AudioEffect GetEffect(string name)
        {
            return _effects[name];
        }

        public void Register(string name)
        {
            var sound = new Sound(new WaveFileLoader(string.Format("{0}/{1}/{2}.wav", _content.RootDirectory, _audioDirectoryName, name)));
            _sounds.Add(name, sound);
        }

        public void Load()
        {
            foreach (var sound in _sounds)
                sound.Value.Load();
        }

        public Sound this[string name]
        {
            get { return _sounds[name]; }
        }

        public void Update()
        {
            foreach (var audioEffect in _effects)
            {
                if (!audioEffect.Value.Dirty) continue;
                foreach (var sound in _sounds)
                    audioEffect.Value.BindTo(sound.Value);
            }
        }

        public float SfxVolume { get; set; }
        public float MusicVolume { get; set; }

        public void Play(string name)
        {
            _sounds[name].Looping = false;
            _sounds[name].Play();
        }

        public void Play(string name, float pitch)
        {
            _sounds[name].Looping = false;
            _sounds[name].Play();
        }

        public void PlayLoopedSound(string name)
        {
            _sounds[name].Looping = true;
            if(!_sounds[name].IsPlaying)
                _sounds[name].Play();
        }

        public void StopLoopedSound(string name)
        {
            _sounds[name].Stop();
        }

        public void Play(string name, float pan, float volume)
        {
            _sounds[name].Looping = false;
            _sounds[name].Play();
        }

        public void PlaySong(string name)
        {
            _sounds[name].Looping = true;
            _sounds[name].Play();
        }

        public void StopSong(string name)
        {
            _sounds[name].Looping = false;
            _sounds[name].Stop();
        }

        public void StopAllSongs()
        {
            foreach (var sound in _sounds)
            {
                StopSong(sound.Key);
            }
        }
    }
}
