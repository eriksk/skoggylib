using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using se.skoggy.utils.Audio.NVorbis;

namespace se.skoggy.utils.Audio
{
    public class Sound
    {
        public OggStream Stream;
        public SoundState State;

        public Sound(OggStream stream, SoundState state)
        {
            Stream = stream;
            State = state;
        }
    }

    public enum SoundState
    {
        Playing,
        Stopped
    }

    public class NVorbisAudioManager : IAudio
    {
        private readonly ContentManager _content;
        private readonly string _soundsDirectoryName;

        public float SfxVolume { get; set; }
        public float MusicVolume { get; set; }

        private readonly Dictionary<string, Sound> _sounds;
        private readonly AudioContext _context;
        private readonly OggStreamer _streamer;

        public NVorbisAudioManager(ContentManager content, string soundsDirectoryName)
        {
            if (content == null) throw new ArgumentNullException("content");
            if (soundsDirectoryName == null) throw new ArgumentNullException("soundsDirectoryName");
            _content = content;
            _soundsDirectoryName = soundsDirectoryName;
            _sounds = new Dictionary<string, Sound>();
            var current = AudioContext.AvailableDevices;
            _context = new AudioContext();
            _streamer = new OggStreamer();
        }

        public void Dispose()
        {
            foreach (var sound in _sounds)
            {
                sound.Value.Stream.Stop();
                sound.Value.Stream.Dispose();
            }
            _context.Dispose();
            _streamer.Dispose();
        }

        public void Update()
        {
        }

        public void SetEffect(string name, float value)
        {
            
        }

        public void Prepare()
        {
            foreach (var sound in _sounds)
            {
                sound.Value.Stream.Prepare();
            }
        }

        public void Register(string name)
        {
            var fileName = string.Format("{0}/{1}/{2}.ogg", _content.RootDirectory, _soundsDirectoryName, name);
            var stream = new OggStream(fileName);
            _sounds.Add(name, new Sound(stream, SoundState.Stopped));
        }

        public void Play(string name)
        {
            var sound = _sounds[name];
            //sound.Stream.IsLooped = false;
            if(sound.State == SoundState.Playing)
                sound.Stream.Stop();
            sound.Stream.Play();
            sound.State = SoundState.Playing;
            //sound.State = SoundState.Playing;
        }

        public void Play(string name, float pitch)
        {
            
        }

        public void PlayLoopedSound(string name)
        {
            var sound = _sounds[name];
            sound.Stream.IsLooped = true;
            sound.Stream.Play();
            sound.State = SoundState.Playing;
        }

        public void StopLoopedSound(string name)
        {
            var sound = _sounds[name];
            if (sound.State == SoundState.Playing)
            {
                sound.Stream.Stop();
                sound.State = SoundState.Stopped;
            }
        }

        public void Play(string name, float pan, float volume)
        {
            throw new NotImplementedException();
        }

        public void PlaySong(string name)
        {
            throw new NotImplementedException();
        }

        public void StopSong(string name)
        {
            throw new NotImplementedException();
        }

        public void StopAllSongs()
        {
            throw new NotImplementedException();
        }
    }
}
