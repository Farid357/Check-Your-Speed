namespace CheckYourSpeed.Loging
{
    public sealed class NameField : InputField, INameField
    {

    }

    public interface INameField : IInputField
    {
        public string Text { get; }
    }
}