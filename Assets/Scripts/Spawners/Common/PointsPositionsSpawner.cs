using System.Collections.Generic;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointsPositionsSpawner : MonoBehaviour
    {
        [SerializeField] private int _xCount;
        [SerializeField] private int _yCount;
        [SerializeField] private float _offset = 0.5f;
        [SerializeField] private PointPositionPrimitive _primitive;

        private readonly List<Vector2> _positions = new();

        public IEnumerable<Vector2> Positions => _positions;

        private Vector2 StartPosition => transform.position;

        public void Spawn()
        {
            for (int x = 0; x < _xCount; x++)
            {
                for (int y = 0; y < _yCount; y++)
                {
                    var position = new Vector2(StartPosition.x + (x * _offset), StartPosition.y + (y * _offset));
                    var point = Instantiate(_primitive, position, Quaternion.identity);
                    point.transform.position = position;
                    point.gameObject.SetActive(false);
                    _positions.Add(point.transform.position);
                }
            }
        }
    }
}
