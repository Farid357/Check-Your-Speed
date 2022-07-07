using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.Factory
{
    public sealed class PointFactory
    {
        private readonly IWaveCleaner _waveCleaner;
        private readonly ILoseTimer _loseTimer;
        private IFactory _factory;

        public PointFactory(ILoseTimer loseTimer, IWaveCleaner waveCleaner)
        {
            _waveCleaner = waveCleaner ?? throw new ArgumentNullException(nameof(waveCleaner));
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
        }

        public IPoint Get(PointType type)
        {
            if (type is PointType.Score)
            {
                _factory = new ScorePointFactory(_loseTimer);
                return _factory.Get();
            }

            else if (type is PointType.Wave)
            {
                _factory = new WavePointFactory(_loseTimer, _waveCleaner);
                return _factory.Get();
            }

            throw new InvalidOperationException();
        }
    }
}
