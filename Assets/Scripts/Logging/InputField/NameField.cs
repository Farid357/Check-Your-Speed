namespace CheckYourSpeed.Loging
{
    public sealed class NameField : InputField, INameField
    {

    }

    public interface INameField
    {
        public string Text { get; }
    }
}