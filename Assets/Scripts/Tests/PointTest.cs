using CheckYourSpeed.Model;
using NUnit.Framework;
using UnityEngine;

namespace CheckYourSpeed.Tests
{
    public sealed class PointTest
    {
        [Test]
        public void PointCatchEventPasses()
        {
            var timerPoint = new TimerPoint(new DummyTimer());
            var point = new WavePoint(new DummyPointsSwitch(), timerPoint);
            point.Apply();
            var count = 0;
            point.OnApplied += (p) => count++;
            timerPoint.OnApplied += (p) => count++;
            Assert.That(count == 0);
        }
    }
}