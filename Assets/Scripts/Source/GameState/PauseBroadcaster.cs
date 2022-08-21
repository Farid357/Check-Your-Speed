namespace CheckYourSpeed.App
{
    public sealed class PauseBroadcaster : IPauseBroadcaster, IPauseSwitch
    {
        public bool GameIsPaused { get; private set; }

        public void Pause() => GameIsPaused = true;

        public void UnPause() => GameIsPaused = false;
    }
}