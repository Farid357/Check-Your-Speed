namespace CheckYourSpeed.App
{
    public sealed class PauseButton : AppStateButton
    {
        protected override void OnClick(IPauseSwitch pauseSwitch)
        {
            pauseSwitch.Pause();
        }
    }
}
