using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Audio
{
    public interface IAudio : IDisposable
    {
        float SfxVolume { get; set; }
        float MusicVolume { get; set; }

        void Play(string name);
        void PlayLoopedSound(string name);
        void StopLoopedSound(string name);
        void Play(string name, float pan, float volume);
        void PlaySong(string name);
        void StopSong(string name);
        void StopAllSongs(string name);
    }
}
