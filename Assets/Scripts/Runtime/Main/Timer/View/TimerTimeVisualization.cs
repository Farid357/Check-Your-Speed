using CheckYourSpeed.Model;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    public sealed class TimerTimeVisualization : MonoBehaviour, IVisualization<float>
    {
        [SerializeField] private LineVisualization _lineVisualization;
        private const int SecondsInMinute = 60;

        public void Visualize(float seconds)
        {
            var minutes = GetMinutes(seconds);
            int leftSeconds = (int)(seconds - minutes * SecondsInMinute);
            var text = minutes == 0 ? $"{leftSeconds}" : $"{minutes}:{leftSeconds}";
            
            if (minutes == 0 && leftSeconds == 0)
            {
                _lineVisualization.Visualize(string.Empty);
            }
            
            else
            {
                _lineVisualization.Visualize(text);

            }
        }

        private int GetMinutes(float seconds)
        {
            var count = 0;
            if (seconds > SecondsInMinute)
            {
                while (seconds > SecondsInMinute)
                {
                    count++;
                    seconds /= SecondsInMinute;
                }
            }

            return count;
        }
    }
}