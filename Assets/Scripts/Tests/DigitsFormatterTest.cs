using CheckYourSpeed.GameLogic;
using NUnit.Framework;

namespace CheckYourSpeed.Tests
{
    public sealed class DigitsFormatterTest
    {
        [Test]
        public void DigitsFormatterFormatCorrect()
        {
            var formatter = new DigitsFormatter();
            var number = 4500000;
            var formattedNumber = formatter.TryFormat(number);
            UnityEngine.Debug.LogError(formattedNumber);
            Assert.That(formattedNumber.Equals("4,5m", System.StringComparison.InvariantCulture));
        }
    }
}
