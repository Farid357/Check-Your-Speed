using System;

namespace CheckYourSpeed.Model
{
    public sealed class WavePoint : IPoint
    {
        private readonly IWaveCleaner _cleaner;
        private readonly IPoint _point;

        public WavePoint(IWaveCleaner cleaner, IPoint point)
        {
            _cleaner = cleaner ?? throw new ArgumentNullException(nameof(cleaner));
            _point = point ?? throw new ArgumentNullException(nameof(point));
        }

        public event Action<IPoint> OnApplyed;

        public void Apply()
        {
            _point.Apply();
            _cleaner.CleanWave();
            OnApplyed?.Invoke(this);
        }
    }
}