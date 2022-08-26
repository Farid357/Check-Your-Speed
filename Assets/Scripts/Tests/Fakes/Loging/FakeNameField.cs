using CheckYourSpeed.Loging;

namespace CheckYourSpeed.Tests
{
    public sealed class FakeNameField : INameField
    {
        public bool TextInvalid => false;
        public bool TextNotEmpty => true;
        public string Text => "TestUser////////";
    }
}