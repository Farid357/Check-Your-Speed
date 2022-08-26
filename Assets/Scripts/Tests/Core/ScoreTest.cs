using CheckYourSpeed.Loging;
using CheckYourSpeed.Model;
using NUnit.Framework;

namespace CheckYourSpeed.Tests
{
    public sealed class ScoreTest
    {
        [Test]
        public void ScoreIncreaseCountCorrectly()
        {
            var realScoreCount = MakeScoreCount25(new MockScoreView());
            Assert.AreEqual(realScoreCount, 25);
        }

        [Test]
        public void ScoreRecordIncreaseCountCorrectly()
        {
            var record = new ScoreRecord(new DummyCountVisualization(), new FakeUserCounterStorage());
            record.TryIncrease(50);
            Assert.AreEqual(record.Count, 50);
        }

        [Test]
        public void ScoreVisualizeToViewCountCorrectly()
        {
            var view = new MockScoreView();
            MakeScoreCount25(view);
            Assert.AreEqual(view.Count, 25);
        }

        private int MakeScoreCount25(IScoreView scoreView)
        {
            var score = new Score(scoreView, new DummyScoreRecord());
            var timerPoint = new TimerPoint(new DummyTimer());
            score.Subscribe(timerPoint);
            timerPoint.Apply();
            return score.Count;
        }
    }
}
