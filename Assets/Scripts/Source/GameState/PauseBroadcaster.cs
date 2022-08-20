namespace CheckYourSpeed.App
{
    public sealed class PauseBroadcaster : IPauseBroadcaster, IPauseSwitch
    {
        public bool IsPaused { get; private set; }

        public void Pause() => IsPaused = true;

        public void UnPause() => IsPaused = false;
    }
}