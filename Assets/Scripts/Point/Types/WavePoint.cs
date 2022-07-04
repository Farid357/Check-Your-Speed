using System;

namespace CheckYourSpeed.Model
{
    [Serializable]
    public sealed class WavePoint : Point
    {
        private readonly IWaveCleaner _cleaner;

        public WavePoint() { }

        public WavePoint(IWaveCleaner cleaner)
        {
            _cleaner = cleaner ?? throw new ArgumentNullException(nameof(cleaner));
        }

        protected override void PlayApplyFeedback()
        {
            _cleaner.CleanWave();
        }
    }
}