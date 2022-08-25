﻿using UnityEngine;
using CheckYourSpeed.Utils;
using CheckYourSpeed.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointsInAreaSpawner : PointsSpawner
    {
        [SerializeField] private float _radius = 2f;
        private IPointInput _pointInput;
        private Queue<Collider2D> _positionPrimitiveColliders = new();

        public void Init(IPointInput pointInput)
        {
            _pointInput = pointInput ?? throw new ArgumentNullException(nameof(pointInput));
            _pointInput.OnInputed += TranslateTo;
        }

        private void OnDisable() => _pointInput.OnInputed -= TranslateTo;

        private void TranslateTo(IPointView point) => transform.position = point.Position;

        protected override Vector2 GetSpawnPoint()
        {
            if (_positionPrimitiveColliders.Count <= 3)
            {
                var colliders = new List<Collider2D>();
                Physics2D.OverlapCircleNonAlloc(transform.position, _radius, colliders.ToArray());
                _positionPrimitiveColliders = colliders.Where(point => point.TryGetComponent(out PointPositionPrimitive pointPositionPrimitive)).ToList().ToQueue();

                if (_positionPrimitiveColliders.Count < 3)
                    throw new InvalidOperationException("Increase radius!");
            }
            var collider = _positionPrimitiveColliders.Peek();
            return collider.transform.position;

        }
    }
}
