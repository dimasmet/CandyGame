using System;
using UnityEngine;
using UnityEngine.UI;

public class Bootstrap : MonoBehaviour
{
    public static Action<int> OnChangeScoreValue;

    public static Action<int> OnChoiceLevel;
    public static Action OnStartGame;
    public static Action OnWinGame;
    public static Action OnEndTimeGame;
    public static Action OnStopGame;
    public static Action OnNextLevel;

    [SerializeField] private TimerMatch timerMatch;

    [Header("Result View")]
    [SerializeField] private ResultView _resultView;

    [Header("Score handler")]
    [SerializeField] private Text _textFieldScore;
    [SerializeField] private Text _textFieldTargetScore;
    private ScoreView _scoreView;
    private ScoreHandler _scoreHandler;

    private LevelInfo currrentLvl;
    private int numberLevel;

    private void Start()
    {
        _scoreView = new ScoreView(_textFieldScore, _textFieldTargetScore);
        _scoreHandler = new ScoreHandler(_scoreView);

        OnChangeScoreValue += _scoreHandler.AddScore;
        OnEndTimeGame += TimeGameEnd;
        OnStartGame += StartGame;
        OnChoiceLevel += ChoiceLevel;

        OnNextLevel += NextLevel;
        OnWinGame += WinLevel;
    }

    private void OnDisable()
    {
        OnChangeScoreValue -= _scoreHandler.AddScore;
        OnEndTimeGame -= TimeGameEnd;
        OnStartGame -= StartGame;
        OnChoiceLevel -= ChoiceLevel;

        OnWinGame -= WinLevel;

        OnNextLevel -= NextLevel;
    }

    private void ChoiceLevel(int num)
    {
        currrentLvl = LevelsData.Instance.GetLevelData(num);
        numberLevel = num;
    }

    private void StartGame()
    {
        _scoreHandler.SetTargetScore(currrentLvl.targetScoreLevel);
        _scoreHandler.ResetScore();
        timerMatch.SetTime(currrentLvl.timeOnLevel);

        ScreensViewHandler.UI.OpenPanel(ScreensViewHandler.NamePanel.Game);
    }

    private void NextLevel()
    {
        int numLvl;
        if (numberLevel < 9)
        {
            numLvl = numberLevel + 1;
        }
        else
        {
            numLvl = 0;
        }
        OnChoiceLevel?.Invoke(numLvl);
        OnStartGame?.Invoke();
    }

    private void WinLevel()
    {
        int score = _scoreHandler.GetScoreResult();
        float timeResult = timerMatch.GetResultTime();
        _resultView.ShowResultGame(ResultView.TypeResult.Win, timeResult, currrentLvl.timeRecord, currrentLvl.rewardLvl);
        BalanceUser.Instance.AddCoinBalance(currrentLvl.rewardLvl);

        LevelsData.Instance.LevelSuccess(numberLevel, timeResult);
    }

    private void TimeGameEnd()
    {
        int score = _scoreHandler.GetScoreResult();
        _resultView.ShowResultGame(ResultView.TypeResult.Lose, score, 0);
    }
}
