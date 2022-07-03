using UnityEngine;

namespace CheckYourSpeed.Utils
{
    public static class Vector2Extensions
    {
        private static Vector2 _previousPoint;
        private static Vector2 _point;

        private static bool HavePreviousPoint => _previousPoint != default;

        private static bool PointsIsSame => _point == _previousPoint;

        public static Vector2 GetRandomPointFrom(this Vector2 vector, Vector2[] points)
        {
            if (points is null)
            {
                throw new System.ArgumentNullException(nameof(points));
            }

            _point = GetRandomPoint(points);

            while (HavePreviousPoint && PointsIsSame)
            {
                _point = GetRandomPoint(points);
            }
            return _point;
        }

        private static Vector2 GetRandomPoint(Vector2[] points)
        {
            var randomIndex = Random.Range(0, points.Length);
            return points[randomIndex];
        }
    }
}
