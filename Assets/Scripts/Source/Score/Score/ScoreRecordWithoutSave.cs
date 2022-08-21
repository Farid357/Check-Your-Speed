namespace CheckYourSpeed.Model
{
    public sealed class ScoreRecordWithoutSave : IScoreRecord
    {
        private readonly ITextView _textView;

        public ScoreRecordWithoutSave(ITextView textView)
        {
            _textView = textView ?? throw new System.ArgumentNullException(nameof(textView));
        }
        public int Count { get; private set; }

        public void TryIncrease(int scoreCount)
        {
            if(Count < scoreCount)
            {
                Count = scoreCount;
                _textView.Visualize(Count);
            }
        }
    }
}
