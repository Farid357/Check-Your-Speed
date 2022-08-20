using UnityEngine;
using CheckYourSpeed.Utils;
using System;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointsRandomPositionsSpawner : PointsSpawner
    {
        private Vector2[] _positions;

        public void Init(Vector2[] positions)
        {
            _positions = positions ?? throw new ArgumentNullException(nameof(positions));
        }

        protected override Vector2 GetSpawnPoint()
        {
            return new Vector2().GetRandomPointFrom(_positions);
        }
    }
}
