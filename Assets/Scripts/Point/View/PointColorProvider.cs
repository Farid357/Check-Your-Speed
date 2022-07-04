using UnityEngine;
using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointColorProvider : MonoBehaviour
    {
        [SerializeField] private Color _wave;
        [SerializeField] private Color _score;

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

            throw new InvalidOperationException();
        }
    }
}
