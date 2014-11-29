using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Audio.OpenAL;

namespace se.skoggy.utils.Audio.OpenAl
{
    public class ALHelpers
    {
        public static ALFormat GetSoundFormat(int channels, int bitsPerSample)
        {
            return channels == 1 && bitsPerSample == 8 ? ALFormat.Mono8 :
            channels == 1 && bitsPerSample == 16 ? ALFormat.Mono16 :
            channels == 2 && bitsPerSample == 8 ? ALFormat.Stereo8 :
            channels == 2 && bitsPerSample == 16 ? ALFormat.Stereo16 : 0; // unknown
        }

        public static void CheckError()
        {
            var error = AL.GetError();
            if (error != ALError.NoError)
            {
                throw new Exception("OpenAL Error: " + AL.GetErrorString(error));
            }
        }
    }
}
