namespace CheckYourSpeed.App
{
    public sealed class ContinueButton : AppStateButton
    {
        protected override void OnClick(PauseBroadcaster pauseBroadcaster)
        {
            pauseBroadcaster.UnPause();
        }
    }
}
