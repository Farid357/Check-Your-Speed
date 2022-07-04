using System;

namespace CheckYourSpeed.GameLogic
{
    public interface IWaveSpawner
    {
        public event Action OnWaitingNextWave;

        public event Action OnSpawningNextWave;
    }
}
