using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Audio.OpenAL;

namespace se.skoggy.utils.Audio.OpenAl
{
    public abstract class AudioEffect
    {
        protected readonly EffectsExtension Efx;
        protected int FilterId;
        public bool Dirty { get; set; }

        protected AudioEffect(EffectsExtension efx, EfxFilterType type)
        {
            if (efx == null) throw new ArgumentNullException("efx");
            Efx = efx;
            FilterId = efx.GenFilter();
            efx.Filter(FilterId, EfxFilteri.FilterType, (int) type);
            ALHelpers.CheckError();
            Dirty = true;
        }

        public abstract void BindTo(Sound sound);

        public virtual void Dispose()
        {
            Efx.DeleteFilter(FilterId);
        }

        public virtual void SetValue(float value)
        {
        }
    }

    public class LowPassFilterAudioEffect : AudioEffect
    {
        private float _gain;

        public LowPassFilterAudioEffect(EffectsExtension efx)
            : base(efx, EfxFilterType.Lowpass)
        {
            _gain = 1f;
        }

        public float Gain
        {
            get { return _gain; }
            set
            {
                _gain = value;
                Efx.Filter(FilterId, EfxFilterf.LowpassGainHF, value);
                ALHelpers.CheckError();
            }
        }

        public override void SetValue(float value)
        {
            Gain = value;
        }

        public override void BindTo(Sound sound)
        {
            Efx.Filter(FilterId, EfxFilterf.LowpassGainHF, Gain);
            Efx.BindFilterToSource(sound.Source, FilterId);
            ALHelpers.CheckError();
        }
    }
}