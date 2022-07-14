using System;

namespace CheckYourSpeed.Model
{
    public interface IPointsSpawner
    {
        public void Spawn(Factory.PointType[] pointTypes);

        public event Action<IPointView> OnSpawned;

    }
}
