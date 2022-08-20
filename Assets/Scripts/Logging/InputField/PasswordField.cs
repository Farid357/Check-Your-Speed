namespace CheckYourSpeed.Loging
{
    public sealed class PasswordField : InputField, IPasswordField
    {

    }

    public interface IPasswordField
    {
        public string Text { get; }
    }
}