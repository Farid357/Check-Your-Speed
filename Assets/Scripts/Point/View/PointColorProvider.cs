using UnityEngine;
using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointColorProvider : MonoBehaviour
    {
        [SerializeField] private Color _wave, _score, _disable, _miltiple, _random;

        public Color Get(IPoint point)
        {
            if (point is WavePoint)
            {
                return _wave;
            }

            else if (point is ScorePoint)
            {
                return _score;
            }

            else if (point is DisablePoint)
            {
                return _disable;
            }

            else if (point is MultiplePoint)
            {
                return _miltiple;
            }

            else if (point is RandomPoint)
            {
                return _random;
            }

            throw new InvalidOperationException();
        }
    }
}
