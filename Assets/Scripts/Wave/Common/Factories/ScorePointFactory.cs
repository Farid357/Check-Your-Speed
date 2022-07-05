using CheckYourSpeed.Model;

namespace CheckYourSpeed.Factory
{
    public sealed class ScorePointFactory : IFactory
    {
        private readonly ILoseTimer _loseTimer;

        public ScorePointFactory(ILoseTimer loseTimer)
        {
            _loseTimer = loseTimer ?? throw new System.ArgumentNullException(nameof(loseTimer));
        }

        public IPoint Get()
        {
            return new ScorePoint(_loseTimer);
        }
    }
}
