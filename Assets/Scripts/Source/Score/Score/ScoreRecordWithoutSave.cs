namespace CheckYourSpeed.Model
{
    public sealed class ScoreRecordWithoutSave : IScoreRecord
    {
        private readonly ITextView _textView;
        private int _count;

        public ScoreRecordWithoutSave(ITextView textView)
        {
            _textView = textView ?? throw new System.ArgumentNullException(nameof(textView));
        }

        public void TryIncrease(int scoreCount)
        {
            if(_count < scoreCount)
            {
                _count = scoreCount;
                _textView.Visualize(_count);
            }
        }
    }
}
