using CheckYourSpeed.Factory;
using CheckYourSpeed.Model;
using System;

public sealed class WavePointFactory : IFactory
{
    private readonly IPointsSwitch _pointsSwitch;
    private readonly ITimer _loseTimer;

    public WavePointFactory(ITimer loseTimer, IPointsSwitch pointsSwitch)
    {
        _pointsSwitch = pointsSwitch ?? throw new ArgumentNullException(nameof(pointsSwitch));
        _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
    }

    public IPoint Create()
    {
        return new WavePoint(_pointsSwitch, new TimerPoint(_loseTimer));
    }
}
