using UnityEngine;
using UnityEngine.UI;

public class ScreensViewHandler : MonoBehaviour
{
    public static ScreensViewHandler UI;

    [SerializeField] private ScreenPanel _menu;
    [SerializeField] private ScreenPanel _levels;
    [SerializeField] private ScreenPanel _game;
    [SerializeField] private ScreenPanel _settings;
    [SerializeField] private ScreenPanel _store;

    [SerializeField] private Text _recordScoreText;

    private ScreenPanel _currentPanel;

    public enum NamePanel
    {
        Menu,
        Game,
        Store,
        Levels
    }

    private void Awake()
    {
        if (UI == null)
            UI = this;
    }

    private void Start()
    {
        OpenPanel(NamePanel.Menu);
    }

    public void OpenPanel(NamePanel name)
    {
        if (_currentPanel != null) _currentPanel.ClosePanel();

        switch (name)
        {
            case NamePanel.Menu:
                _currentPanel = _menu;

                _recordScoreText.text = "BEST SCORE: " + PlayerPrefs.GetInt("RecordPlayer");
                break;
            case NamePanel.Game:
                _currentPanel = _game;
                break;
            case NamePanel.Store:
                _currentPanel = _store;
                break;
            case NamePanel.Levels:
                _currentPanel = _levels;
                break;
        }

        _currentPanel.OpenPanel();
    }
}
