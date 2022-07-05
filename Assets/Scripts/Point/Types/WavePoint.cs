using System;

namespace CheckYourSpeed.Model
{
    public sealed class WavePoint : Point
    {
        private readonly IWaveCleaner _cleaner;

        public WavePoint(ILoseTimer timer, IWaveCleaner cleaner) : base(timer)
        {
            _cleaner = cleaner ?? throw new ArgumentNullException(nameof(cleaner));
        }


        protected override void PlayApplyFeedback()
        {
            _cleaner.CleanWave();
        }
    }
}