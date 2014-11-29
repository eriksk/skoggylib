using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Audio.OpenAL;

namespace se.skoggy.utils.Audio.OpenAl
{
    public class Sound : IDisposable
    {
        private readonly IAudioFileLoader _fileLoader;
        private int[] _buffers;
        private int _source;

        public Sound(IAudioFileLoader fileLoader)
        {
            if (fileLoader == null) throw new ArgumentNullException("fileLoader");
            _fileLoader = fileLoader;
        }

        public void Load()
        {
            int channels;
            int bits;
            int sampleRate;

            var bytes = _fileLoader.LoadWave(out channels, out bits, out sampleRate);
            var soundFormat = ALHelpers.GetSoundFormat(channels, bits);

            // generate buffer
            _buffers = AL.GenBuffers(1);
            ALHelpers.CheckError();

            // load data to buffer
            AL.BufferData(_buffers[0], soundFormat, bytes, bytes.Length, sampleRate);
            ALHelpers.CheckError();

            // generate source
            _source = AL.GenSource();
            ALHelpers.CheckError();

            // attach source to buffer
            AL.Source(_source, ALSourcei.Buffer, _buffers[0]);
            ALHelpers.CheckError();
        }

        public void Dispose()
        {
            AL.DeleteBuffers(_buffers);
            AL.DeleteSource(_source);
        }

        public bool Looping
        {
            get
            {
                bool val = false;
                AL.GetSource(_source, ALSourceb.Looping, out val);
                return val;
            }
            set
            {
                AL.Source(_source, ALSourceb.Looping, value);
            }
        }

        public void Play()
        {
            AL.SourcePlay(_source);
        }

        public void Pause()
        {
            AL.SourcePause(_source);
        }

        public void Resume()
        {
            AL.SourcePlay(_source);
        }

        public void Stop()
        {
            AL.SourceStop(_source);
        }

        public bool IsPlaying 
        {
            get { return AL.GetSourceState(_source) == ALSourceState.Playing; }
        }

        public int Source
        {
            get { return _source; }
        }

    }
}