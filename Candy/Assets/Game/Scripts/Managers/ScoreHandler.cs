
public class ScoreHandler
{
    private int _score;
    private int _targetScore;
    private ScoreView _scoreView;

    public ScoreHandler(ScoreView scoreView)
    {
        _scoreView = scoreView;
    }

    public void SetTargetScore(int scoreTarget)
    {
        _targetScore = scoreTarget;
    }

    public void ResetScore()
    {
        _score = 0;
        _scoreView.UpdateTextView(_score, _targetScore);
    }

    public void AddScore(int value)
    {
        _score += value;
        _scoreView.UpdateTextView(_score, _targetScore);

        if (_score >= _targetScore)
        {
            Bootstrap.OnWinGame?.Invoke();
        }
    }

    public int GetScoreResult()
    {
        return _score;
    }
}
