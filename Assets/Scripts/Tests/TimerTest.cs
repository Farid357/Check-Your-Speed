using CheckYourSpeed.Model;
using CheckYourSpeed.Utils;
using NUnit.Framework;

namespace CheckYourSpeed.Tests
{
    public sealed class TimerTest
    {
        [Test]
        public void IsTimerFinishCountdown()
        {
            var timer = new Timer(1, new DummyTimerVisualization());
            timer.Update(1);
            Assert.That(timer.FinishedCountdown);
        }

        [Test]
        public void IsTimerAddsTimeCorrectly()
        {
            var timer = new Timer(2, new DummyTimerVisualization());
            timer.Add(2);
            Assert.That(timer.Time == 4);
        }

        [Test]
        public void IsTimerResetsTimeCorrectly()
        {
            var timer = new Timer(2, new DummyTimerVisualization());
            timer.Add(4);
            timer.Reset();
            Assert.That(timer.Time == 2);
        }

        [Test]
        public void IsTimerThrowLessOrEqualZeroException()
        {
            Assert.Throws<LessThanOrEqualsToZeroException>(() => new Timer(0, new DummyTimerVisualization()));
            Assert.Throws<LessThanOrEqualsToZeroException>(() => new Timer(-1, new DummyTimerVisualization()));
        }
    }
}
