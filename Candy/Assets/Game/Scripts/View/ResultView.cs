using UnityEngine;
using UnityEngine.UI;

public class ResultView : MonoBehaviour
{
    public enum TypeResult
    {
        Win,
        Lose
    }

    [SerializeField] private GameObject _panelResult;

    [Header("View panels")]
    [SerializeField] private GameObject _winBlock;
    [SerializeField] private GameObject _loseBlock;

    [Header("Buttons")]
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _nextLevelButton;

    [Header("TextFields")]
    [SerializeField] private Text _timeResultText;
    [SerializeField] private Text _rewardCoinText;
    [SerializeField] private Text _bestTimeText;

    private void Awake()
    {
        InitButtons();
    }

    private void InitButtons()
    {
        _restartButton.onClick.AddListener(() =>
        {
            ScreensViewHandler.UI.OpenPanel(ScreensViewHandler.NamePanel.Game);
            BoardManager.instance.RespawnField(true);
            _panelResult.SetActive(false);
        });

        _homeButton.onClick.AddListener(() =>
        {
            ScreensViewHandler.UI.OpenPanel(ScreensViewHandler.NamePanel.Menu);
            _panelResult.SetActive(false);
        });

        _nextLevelButton.onClick.AddListener(() =>
        {
            ScreensViewHandler.UI.OpenPanel(ScreensViewHandler.NamePanel.Game);
            _panelResult.SetActive(false);

            Bootstrap.OnNextLevel?.Invoke();
        });
    }

    public void ShowResultGame(TypeResult type, float timeValue = 0, float bestRecordLvl = 0, int rewardValue = 0)
    {
        _panelResult.SetActive(true);

        Tile.GlobalActive = false;

        switch (type)
        {
            case TypeResult.Win:
                _winBlock.SetActive(true);
                _loseBlock.SetActive(false);

                _timeResultText.text = "TIME: " + string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timeValue / 60), Mathf.FloorToInt(timeValue % 60));
                _rewardCoinText.text = "+" + rewardValue;
                _bestTimeText.text = "BEST TIME: " + string.Format("{0:00}:{1:00}", Mathf.FloorToInt(bestRecordLvl / 60), Mathf.FloorToInt(bestRecordLvl % 60));
                break;
            case TypeResult.Lose:
                _winBlock.SetActive(false);
                _loseBlock.SetActive(true);
                break;
        }
    }
}
