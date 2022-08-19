using CheckYourSpeed.Model;
using System;
using Zenject;

namespace CheckYourSpeed.Factory
{
    public sealed class RandomPointFactory : IFactory
    {
        private readonly DiContainer _container;
        private ITimer _loseTimer;
        private IWaveCleaner _waveCleaner;

        public RandomPointFactory(ITimer loseTimer, IWaveCleaner waveCleaner, DiContainer container)
        {
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _waveCleaner = waveCleaner ?? throw new ArgumentNullException(nameof(waveCleaner));
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public IPoint Get()
        {
            var randomPoint = _container.Instantiate<RandomPoint>();
            randomPoint.Init(_loseTimer, _waveCleaner);
            return randomPoint;
        }
    }
}
