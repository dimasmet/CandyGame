using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseView : MonoBehaviour
{
    [SerializeField] private GameObject _panelPause;
    [Header("Buttons")]
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _closeButton;

    private void Awake()
    {
        _homeButton.onClick.AddListener(() =>
        {
            _panelPause.SetActive(false);
            Time.timeScale = 1;

            ScreensViewHandler.UI.OpenPanel(ScreensViewHandler.NamePanel.Levels);
            Bootstrap.OnStopGame?.Invoke();

            Tile.GlobalActive = true;
        });

        _restartButton.onClick.AddListener(() =>
        {
            _panelPause.SetActive(false);

            Time.timeScale = 1;

            Bootstrap.OnStopGame?.Invoke();

            ScreensViewHandler.UI.OpenPanel(ScreensViewHandler.NamePanel.Game);
            BoardManager.instance.RespawnField(true);

            Tile.GlobalActive = true;
        });

        _closeButton.onClick.AddListener(() =>
        {
            _panelPause.SetActive(false);

            Tile.GlobalActive = true;
            Time.timeScale = 1;
        });
    }

    public void ShowPause()
    {
        _panelPause.SetActive(true);
        Tile.GlobalActive = false;
        Time.timeScale = 0;
    }
}
