namespace CheckYourSpeed.App
{
    public sealed class ContinueButton : AppStateButton
    {
        protected override void OnClick(IPauseSwitch pauseSwitch)
        {
            pauseSwitch.UnPause();
        }
    }
}
