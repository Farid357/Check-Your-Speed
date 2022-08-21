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
            var timer = new Timer(1);
            timer.Update(1);
            Assert.That(timer.FinishedCountdown);
        }

        [Test]
        public void IsTimerAddsTimeCorrectly()
        {
            var timer = new Timer(2);
            timer.Add(2);
            Assert.That(timer.Time == 4);
        }

        [Test]
        public void IsTimerResetsTimeCorrectly()
        {
            var timer = new Timer(2);
            timer.Add(4);
            timer.Reset();
            Assert.That(timer.Time == 2);
        }

        [Test]
        public void IsTimerThrowLessOrEqualZeroException()
        {
            Assert.Throws<LessOrEqualZeroException>(() => new Timer(0));
            Assert.Throws<LessOrEqualZeroException>(() => new Timer(-1));
        }
    }
}
