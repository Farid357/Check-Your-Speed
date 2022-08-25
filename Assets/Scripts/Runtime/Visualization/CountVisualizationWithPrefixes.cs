using CheckYourSpeed.Model;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    public sealed class CountVisualizationWithPrefixes : MonoBehaviour, IVisualization<int>
    {
        [SerializeField] private LineVisualization _lineVisualization;
        private readonly DigitsFormatter _digitsFormatter = new();

        public void Visualize(int count)
        {
            var text = _digitsFormatter.TryFormat(count);
           _lineVisualization.Visualize(text);
        }
    }
}