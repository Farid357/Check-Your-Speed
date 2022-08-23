using CheckYourSpeed.Factory;
using CheckYourSpeed.Model;
using System;

public sealed class WavePointFactory : IFactory
{
    private readonly IPointsSwitch _pointsSwitch;
    private readonly ITimer _timer;

    public WavePointFactory(ITimer timer, IPointsSwitch pointsSwitch)
    {
        _pointsSwitch = pointsSwitch ?? throw new ArgumentNullException(nameof(pointsSwitch));
        _timer = timer ?? throw new ArgumentNullException(nameof(timer));
    }

    public IPoint Create()
    {
        return new WavePoint(_pointsSwitch, new TimerPoint(_timer));
    }
}
