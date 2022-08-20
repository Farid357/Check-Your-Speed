using CheckYourSpeed.Factory;
using CheckYourSpeed.Model;
using System;

public sealed class WavePointFactory : IFactory
{
    private readonly IWaveCleaner _waveCleaner;
    private readonly ITimer _loseTimer;

    public WavePointFactory(ITimer loseTimer, IWaveCleaner waveCleaner)
    {
        _waveCleaner = waveCleaner ?? throw new ArgumentNullException(nameof(waveCleaner));
        _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
    }

    public IPoint Create()
    {
        return new WavePoint(_waveCleaner, new TimerPoint(_loseTimer));
    }
}
