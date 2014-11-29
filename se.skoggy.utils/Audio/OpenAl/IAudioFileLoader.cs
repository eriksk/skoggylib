using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Audio.OpenAl
{
    public interface IAudioFileLoader
    {
        byte[] LoadWave(out int channels, out int bits, out int rate);
    }

    //public class OggVorbisFileLoader : IAudioFileLoader
    //{
    //    private readonly string _fileName;

    //    public OggVorbisFileLoader(string fileName)
    //    {
    //        if (fileName == null) throw new ArgumentNullException("fileName");
    //        _fileName = fileName;
    //    }

    //    public short[] LoadWave(out int channels, out int bits, out int rate)
    //    {
    //        NVorbis.

    //        using (var reader = new VorbisReader(_fileName))
    //        {
    //            channels = reader.Channels;
    //            rate = reader.SampleRate;
    //            bits = reader.NominalBitrate;

    //            long samples = reader.TotalSamples/reader.SampleRate;
    //            var buffer = new float[16384 * samples];
    //            var readSamples = reader.ReadSamples(buffer, 0, buffer.Length);

    //            short[] outBuffer = new short[buffer.Length];
    //            CastBuffer(buffer, outBuffer, readSamples);
    //            return outBuffer;
    //        }
    //    }

    //    private void CastBuffer(float[] inBuffer, short[] outBuffer, int length)
    //    {
    //        for (int i = 0; i < length; i++)
    //        {
    //            var temp = (int)(32767f * inBuffer[i]);
    //            if (temp > short.MaxValue) temp = short.MaxValue;
    //            else if (temp < short.MinValue) temp = short.MinValue;
    //            outBuffer[i] = (short)temp;
    //        }
    //    }

    //}

    public class WaveFileLoader : IAudioFileLoader
    {
        private readonly string _fileName;

        public WaveFileLoader(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            _fileName = fileName;
        }

        public byte[] LoadWave(out int channels, out int bits, out int rate)
        {
            using (var reader = new BinaryReader(new FileStream(_fileName, FileMode.Open)))
            {
                // RIFF header
                var signature = new string(reader.ReadChars(4));
                if (signature != "RIFF")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                int riffChunckSize = reader.ReadInt32();

                var format = new string(reader.ReadChars(4));
                if (format != "WAVE")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                // WAVE header
                var formatSignature = new string(reader.ReadChars(4));
                if (formatSignature != "fmt ")
                    throw new NotSupportedException("Specified wave file is not supported.");

                var formatChunkSize = reader.ReadInt32();
                var audioFormat = reader.ReadInt16();
                var numChannels = reader.ReadInt16();
                var sampleRate = reader.ReadInt32();
                var byteRate = reader.ReadInt32();
                var blockAlign = reader.ReadInt16();
                var bitsPerSample = reader.ReadInt16();

                var dataSignature = new string(reader.ReadChars(4));
                if (dataSignature != "data")
                    throw new NotSupportedException("Specified wave file is not supported.");

                int dataChunkSize = reader.ReadInt32();

                channels = numChannels;
                bits = bitsPerSample;
                rate = sampleRate;

                return reader.ReadBytes((int)reader.BaseStream.Length);
            }
        }
    }
}
