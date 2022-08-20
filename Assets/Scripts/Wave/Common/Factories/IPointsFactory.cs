using CheckYourSpeed.Model;

namespace CheckYourSpeed.Factory
{
    public interface IPointsFactory
    {
        public IPoint CreateFrom(PointType type);
    }
}
