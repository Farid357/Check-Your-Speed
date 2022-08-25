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
            Assert.That(formattedNumber.Contains("4,5"));
        }
    }
}
