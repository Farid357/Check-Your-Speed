namespace CheckYourSpeed.Loging
{
    public sealed class PasswordField : InputField, IPasswordField
    {

    }

    public interface IPasswordField : IInputField
    {
        public string Text { get; }
    }
}