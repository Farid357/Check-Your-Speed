namespace CheckYourSpeed.App
{
    public sealed class PauseButton : AppStateButton
    {
        protected override void OnClick(PauseBroadcaster pauseBroadcaster)
        {
            pauseBroadcaster.Pause();
        }
    }
}
