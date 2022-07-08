using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace CheckYourSpeed.GameLogic
{
    public interface IWaveSpawner
    {
        public event Action OnSpawningNextWave;

        public event Action<Wave, IEnumerable<IPointView>> OnSpawnedNextWave;

        public event Action OnWaiting;

        public UniTaskVoid SpawnWithDelay();

    }
}
