namespace CheckYourSpeed.App
{
    public sealed class PauseButton : AppStateButton
    {
        protected override void OnClick(GameState gameState)
        {
            gameState.Pause();
        }
    }
}
