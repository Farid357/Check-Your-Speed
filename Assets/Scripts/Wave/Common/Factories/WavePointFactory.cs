using CheckYourSpeed.Factory;
using CheckYourSpeed.Model;
using System;

public sealed class WavePointFactory : IFactory
{
    private readonly IWaveCleaner _waveCleaner;
    private readonly ILoseTimer _loseTimer;

    public WavePointFactory(ILoseTimer loseTimer, IWaveCleaner waveCleaner)
    {
        _waveCleaner = waveCleaner ?? throw new ArgumentNullException(nameof(waveCleaner));
        _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
    }

    public IPoint Get()
    {
        return new WavePoint(_loseTimer, _waveCleaner);
    }
}
