using CheckYourSpeed.Loging;

namespace CheckYourSpeed.Tests
{
    public sealed class FakePasswordField : IPasswordField
    {
        public bool TextInvalid => false;
        public bool TextNotEmpty => true;
        public string Text => "TestUser////////";
    }
}