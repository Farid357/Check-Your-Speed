using NUnit.Framework;

namespace CheckYourSpeed.Tests
{
    public sealed class SystemTest
    {
        [Test]
        public void SystemCreateUserCorrectly()
        {
            var system = new Loging.System(new FakeNameField(), new FakePasswordField(), new DummyStorage());
            var startCount = system.EnteredUsers.Count;
            system.CreateNewUser();
            var newUsers = system.EnteredUsers;
            Assert.That(startCount < newUsers.Count);
        }

        [Test]
        public void SystemThrowsUserEnteredEvent()
        {
            var system = new Loging.System(new FakeNameField(), new FakePasswordField(), new DummyStorage());
            var count = 0;
            system.OnUserEntered += (_) => count++;
            system.InviteUser();
            Assert.That(count == 1);
        }

    }
}