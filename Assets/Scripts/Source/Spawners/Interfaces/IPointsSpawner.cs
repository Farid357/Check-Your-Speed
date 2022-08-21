using CheckYourSpeed.Factory;

namespace CheckYourSpeed.Model
{
    public interface IPointsSpawner
    {
        public void SpawnRandomFrom(PointType[] pointTypes);

    }
}
