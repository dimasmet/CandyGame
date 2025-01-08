using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : ScreenPanel
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private PauseView pauseView;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            //Bootstrap.OnStopGame?.Invoke();
            //ScreensViewHandler.UI.OpenPanel(ScreensViewHandler.NamePanel.Levels);

            pauseView.ShowPause();
        });
    }
}
