namespace CheckYourSpeed.App
{
    public sealed class ContinueButton : AppStateButton
    {
        protected override void OnClick(GameState gameState)
        {
            gameState.UnPause();
        }
    }
}
